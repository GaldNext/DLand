using DLand.backend.models;
using MaterialDesignThemes.Wpf;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using static MaterialDesignThemes.Wpf.Theme;
using TextBox = System.Windows.Controls.TextBox;

namespace DLand.Utilities
{
    public class Load:Database
    {
        User user; List<Hero> user_heroes; Convertation convertation = new Convertation(); 

        public TabItem a, b;
        public Border icon;
        public Snackbar snackbar;
        public Load(User user, List<Hero> user_heroes,TabItem a, TabItem b, Border icon, Snackbar snackbar) { this.user = user; this.user_heroes = user_heroes; this.a = a; this.b = b; this.icon = icon; this.snackbar = snackbar; }
        public void CheckUser(User user)
        {
            this.user = user;
        }
        public void ChangeTAB(TabItem i)
        {
            try
            {
                if (i.Visibility == System.Windows.Visibility.Visible)
                {
                    i.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    i.Visibility = System.Windows.Visibility.Visible;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Snack($"Error: {ex.Message}", null, null, TimeSpan.FromSeconds(4));
            }
        }
        public void Preload(FrameworkElement f)
        {
            try
            {
                f.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
                f.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
                icon.Visibility = System.Windows.Visibility.Hidden;
                ChangeTAB(b);
            }
            catch (IndexOutOfRangeException ex)
            {
                Snack($"Error: {ex.Message}", null, null, TimeSpan.FromSeconds(4));
            }
        }
        public void Exit()
        {
            try
            {
                icon.Visibility = System.Windows.Visibility.Hidden;
                ChangeTAB(a);
                ChangeTAB(b);
                a.IsSelected = true;
                Snack($"Вы вышли из учётной записи", null, null, TimeSpan.FromSeconds(6));
            }
            catch (IndexOutOfRangeException ex)
            {
                Snack($"Error: {ex.Message}", null, null, TimeSpan.FromSeconds(6));
            }
        }
        public void Snack(object message, object? button, Action<object?>? a, TimeSpan time)
        {
            snackbar.MessageQueue?.Enqueue(message, button, a, null, false, true, time);
        }
        public async Task LoadUserHeroes(StackPanel sp, User Thisuser)
        {
            if (user_heroes.Count != 0)
            {
                user_heroes.Clear();
            }
            if (sp.Children.Count != 0)
            {
                sp.Children.Clear();
            }

            var heroes = await HeroesGetByUserId(user.Id);
            foreach(var hero in heroes)
            {
                user_heroes.Add(hero);
                sp.Children.Add(new TextBlock() { Text = hero.Name, TextWrapping=TextWrapping.Wrap });
            }
        }
        private void HeroCardClick(object sender, MouseButtonEventArgs e, Hero hero, User user)
        {
            try
            {
                var card = sender as MaterialDesignThemes.Wpf.Card;
                CharacterWindow characterWindow = new CharacterWindow(hero, user);
                characterWindow.Show();
            }
            catch (Exception ex) {
                Snack($"Error: {ex.Message}", null, null, TimeSpan.FromSeconds(6));
            }
        }
        public async Task LoadHeroes(WrapPanel wp, System.Windows.Controls.TextBox tb)
        {
            try
            {
                if (wp.Children.Count != 0)
                {
                    wp.Children.Clear();
                }

                string xamlContent = File.ReadAllText(Path.Combine("ViewModel", "HeroCard.xaml"));
                var heroes = await HeroesGetAll();
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    foreach (var hero in heroes)
                    {
                        var content = XamlReader.Parse(xamlContent) as FrameworkElement;
                        var card = content.FindName("HeroCard") as MaterialDesignThemes.Wpf.Card;
                        var image = content.FindName("HeroImage") as Image;
                        var nameText = content.FindName("HeroName") as TextBlock;
                        if (hero.User_Id == 5 && user.Login == "galdnext")
                        {
                            if (card != null && image != null && nameText != null)
                            {
                                nameText.Text = hero.Name;
                                image.Source = convertation.GetImageFromBytes(hero.Image);
                                card.MouseLeftButtonDown += (sendler, e) => HeroCardClick(sendler, e, hero, user);
                                wp.Children.Add(content);
                            }
                        }
                        else if (hero.User_Id != 5)
                        {
                            if (card != null && image != null && nameText != null)
                            {
                                nameText.Text = hero.Name;
                                image.Source = convertation.GetImageFromBytes(hero.Image);
                                card.MouseLeftButtonDown += (sendler, e) => HeroCardClick(sendler, e, hero, user);
                                wp.Children.Add(content);
                            }
                        }

                        
                    }
                }
                else
                {
                    foreach (var hero in heroes)
                    {
                        var content = XamlReader.Parse(xamlContent) as FrameworkElement;
                        var card = content.FindName("HeroCard") as MaterialDesignThemes.Wpf.Card;
                        var image = content.FindName("HeroImage") as Image;
                        var nameText = content.FindName("HeroName") as TextBlock;
                        if (hero.Name.Trim().ToLower().Contains(tb.Text) && hero.User_Id == 5 && user.Login == "galdnext")
                        {
                            nameText.Text = hero.Name;
                            image.Source = convertation.GetImageFromBytes(hero.Image);
                            card.MouseLeftButtonDown += (sendler, e) => HeroCardClick(sendler, e, hero, user);
                            wp.Children.Add(content);
                        }
                        else if(hero.Name.Trim().ToLower().Contains(tb.Text) && hero.User_Id != 5)
                        {
                            nameText.Text = hero.Name;
                            image.Source = convertation.GetImageFromBytes(hero.Image);
                            card.MouseLeftButtonDown += (sendler, e) => HeroCardClick(sendler, e, hero, user);
                            wp.Children.Add(content);
                        }
                    }
                }
                if (wp.Children.Count == 0)
                {
                    Snack("По запросу не найдено записей", null, null, TimeSpan.FromSeconds(3));
                }

            }
            catch (Exception ex)
            {
                Snack($"Error: {ex.Message}", null, null, TimeSpan.FromSeconds(6));
            }
        }
    }
}
