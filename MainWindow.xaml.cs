using Dapper;
using Npgsql;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using DLand.Utilities;
using DLand.backend.models;
using System.Threading.Tasks;


namespace DLand
{
    public partial class MainWindow : Window
    {
        User user = new User(); List<Hero> user_heroes = new List<Hero>();
        Load load; Log_in_Out log_in; private CancellationTokenSource _search;

        public MainWindow()
        {
            InitializeComponent();
            load = new Load(user,user_heroes,Tab1, Tab2, IconExit, SnackbarPresenter);
            log_in = new Log_in_Out(load);
            load.Preload(this);
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            await log_in.Login(BoxLogin, BoxPassword, UserHeroSP, HeroesAllSP, SearchBox, user);
        }

        private void Exit(object sender, MouseButtonEventArgs e)
        {
            load.Exit();
        }

        private async void Tab2_Button(object sender, MouseButtonEventArgs e)
        {
            await load.LoadUserHeroes(UserHeroSP, user);
            await load.LoadHeroes(HeroesAllSP, SearchBox);
        }

        private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                _search?.Cancel();
                _search = new CancellationTokenSource();
                await Task.Delay(TimeSpan.FromSeconds(2), _search.Token);
                await load.LoadHeroes(HeroesAllSP, SearchBox);
            }
            catch (Exception ex){}
        }
    }
}