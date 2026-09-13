using DLand.backend.models;
using DLand.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DLand
{
    /// <summary>
    /// Логика взаимодействия для CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window
    {
        Hero hero; User user;
        public CharacterWindow(Hero hero, User user)
        {
            InitializeComponent();

            this.hero = hero; this.user = user;
            HeroCard heroCard = new HeroCard(hero, user, HeroImage, Power1, Power2, Magic1, Magic2, Durability1, Durability2, Dexterity1, Dexterity2, Wisdom1, Wisdom2, Luck1, Luck2, Weight1, Weight2, Stress1, Stress2, Speed1, Speed2, HP1, HP2, MP1, MP2, ID, OP1, OP2, Name1, Name2, Race1, Race2, Class1, Class2, Profession1, Profession2, StatEdit, StatConf, CharEdit, CharConf, CharDel, SpellAdd, EquAdd, ImageEdit, AbilitieWP, EquWP);
            heroCard.LoadHeroCard();
        }
    }
}
