using DLand.backend.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using static MaterialDesignThemes.Wpf.Theme;
using ComboBox = System.Windows.Controls.ComboBox;
using TextBox = System.Windows.Controls.TextBox;

namespace DLand.Utilities
{
    class HeroCard:Database
    {
        Convertation convertation = new Convertation();
        Hero hero; User user; Image image; TextBlock power1; TextBox power2; TextBlock magic1; TextBox magic2; TextBlock durability1; TextBox durability2; TextBlock dexterity1; TextBox dexterity2; TextBlock wisdom1; TextBox wisdom2; TextBlock luck1; TextBox luck2; TextBlock weight1; TextBox weight2; TextBlock stress1; TextBox stress2; TextBlock speed1; TextBox speed2; TextBlock hp1; TextBox hp2; TextBlock mp1; TextBox mp2; TextBlock ID; TextBlock op1; TextBox op2; TextBlock name1; TextBox name2; TextBlock race1; TextBox race2; TextBlock class1; TextBox class2; TextBlock profession1; TextBox profession2; Border StatEdit; Border StatConf; Border CharEdit; Border CharConf; Border CharDel; Border SpellAdd; Border EquAdd; Border ImageEdit; WrapPanel AbilitieWP; WrapPanel EquWP;

        public HeroCard(Hero hero, User user, Image image, TextBlock power1, TextBox power2, TextBlock magic1, TextBox magic2, TextBlock durability1, TextBox durability2, TextBlock dexterity1, TextBox dexterity2, TextBlock wisdom1, TextBox wisdom2, TextBlock luck1, TextBox luck2, TextBlock weight1, TextBox weight2, TextBlock stress1, TextBox stress2, TextBlock speed1, TextBox speed2, TextBlock hp1, TextBox hp2, TextBlock mp1, TextBox mp2, TextBlock iD, TextBlock op1, TextBox op2, TextBlock name1, TextBox name2, TextBlock race1, TextBox race2, TextBlock class1, TextBox class2, TextBlock profession1, TextBox profession2, Border statEdit, Border statConf, Border charEdit, Border charConf, Border charDel, Border spellAdd, Border equAdd, Border imageEdit, WrapPanel AbilitieWP, WrapPanel EquWP)
        {
            this.hero = hero;
            this.user = user;
            this.image = image;
            this.power1 = power1;
            this.power2 = power2;
            this.magic1 = magic1;
            this.magic2 = magic2;
            this.durability1 = durability1;
            this.durability2 = durability2;
            this.dexterity1 = dexterity1;
            this.dexterity2 = dexterity2;
            this.wisdom1 = wisdom1;
            this.wisdom2 = wisdom2;
            this.luck1 = luck1;
            this.luck2 = luck2;
            this.weight1 = weight1;
            this.weight2 = weight2;
            this.stress1 = stress1;
            this.stress2 = stress2;
            this.speed1 = speed1;
            this.speed2 = speed2;
            this.hp1 = hp1;
            this.hp2 = hp2;
            this.mp1 = mp1;
            this.mp2 = mp2;
            ID = iD;
            this.op1 = op1;
            this.op2 = op2;
            this.name1 = name1;
            this.name2 = name2;
            this.race1 = race1;
            this.race2 = race2;
            this.class1 = class1;
            this.class2 = class2;
            this.profession1 = profession1;
            this.profession2 = profession2;
            StatEdit = statEdit;
            StatConf = statConf;
            CharEdit = charEdit;
            CharConf = charConf;
            CharDel = charDel;
            SpellAdd = spellAdd;
            EquAdd = equAdd;
            ImageEdit = imageEdit;
            this.AbilitieWP = AbilitieWP;
            this.EquWP = EquWP;

        }
        private void SetProperty(TextBox a, TextBlock b, string property) {
            if (property == null || property == "") { property = "0"; }
            a.Text = property; b.Text = a.Text;
        }
        private void SetVisibility(FrameworkElement element)
        {
            if (element.Visibility == Visibility.Visible) {
                element.Visibility = Visibility.Collapsed;
            }
            else
            {
                element.Visibility = Visibility.Visible;
            }
        }
        private async Task LoadHeroAbilities()
        {
            if (AbilitieWP.Children.Count != 0) { AbilitieWP.Children.Clear(); }
            string xamlContent = File.ReadAllText(Path.Combine("ViewModel", "SpellCard.xaml"));
            if(hero.abilities.Count != 0)
            {
                foreach (var HeroAbilities in hero.abilities)
                {
                    var content = XamlReader.Parse(xamlContent) as FrameworkElement;
                    var card = content.FindName("Card") as Border;
                    var id = content.FindName("Id") as TextBlock;
                    var name1 = content.FindName("Name1") as TextBlock;
                    var name2 = content.FindName("Name2") as TextBox;
                    var type1 = content.FindName("Type1") as TextBlock;
                    var type2 = content.FindName("Type2") as ComboBox;
                    var text1 = content.FindName("Text1") as TextBlock;
                    var text2 = content.FindName("Text2") as TextBox;
                    var edit = content.FindName("Edit") as Border;
                    var che = content.FindName("Che") as Border;
                    var del = content.FindName("Del") as Border;

                    SetVisibility(edit); SetVisibility(che); SetVisibility(del);
                    SetVisibility(name2); SetVisibility(type2); SetVisibility(text2);
                    if (user.Id == hero.User_Id || user.Login == "galdnext")
                    {
                        SetVisibility(edit);
                    }
                    
                    ComboBoxItem item1 = type2.Items[0] as ComboBoxItem;
                    ComboBoxItem item2 = type2.Items[1] as ComboBoxItem;
                    id.Text = HeroAbilities.Id.ToString();
                    SetProperty(name2, name1, HeroAbilities.PersonAbilitie[0].ToString());
                    SetProperty(text2, text1, HeroAbilities.PersonAbilitie[1].ToString());
                    if(HeroAbilities.TypeAbilitie == 1) { type1.Text = item1.Content.ToString(); type2.SelectedIndex = 0; }
                    else { type1.Text = item2.Content.ToString(); type2.SelectedIndex = 1; }
                    AbilitieWP.Children.Add(content);
                }
            }
        }
        private async Task LoadHeroItems()
        {
            if (EquWP.Children.Count != 0) { EquWP.Children.Clear(); }
            string xamlContent = File.ReadAllText(Path.Combine("ViewModel", "InventoryCard.xaml"));
            if (hero.items.Count != 0)
            {
                foreach (var HeroItem in hero.items)
                {
                    var content = XamlReader.Parse(xamlContent) as FrameworkElement;
                    var card = content.FindName("Card") as Border;
                    var id = content.FindName("Id") as TextBlock;
                    var name1 = content.FindName("Name1") as TextBlock;
                    var name2 = content.FindName("Name2") as TextBox;
                    var wei1 = content.FindName("Wei1") as TextBlock;
                    var wei2 = content.FindName("Wei2") as TextBox;
                    var text1 = content.FindName("Text1") as TextBlock;
                    var text2 = content.FindName("Text2") as TextBox;
                    var edit = content.FindName("Edit") as Border;
                    var che = content.FindName("Che") as Border;
                    var del = content.FindName("Del") as Border;

                    SetVisibility(edit); SetVisibility(che); SetVisibility(del);
                    SetVisibility(name2); SetVisibility(wei2); SetVisibility(text2);
                    if (user.Id == hero.User_Id || user.Login == "galdnext")
                    {
                        SetVisibility(edit);
                    }

                    id.Text = HeroItem.Id.ToString();
                    SetProperty(name2, name1, HeroItem.Item[0].ToString());
                    SetProperty(text2, text1, HeroItem.Item[1].ToString());
                    SetProperty(wei2, wei1, HeroItem.ItemWeight.ToString());
                    EquWP.Children.Add(content);
                }
            }
        }
        public void LoadHeroCard()
        {
            SetVisibility(StatEdit); SetVisibility(StatConf); SetVisibility(CharEdit); SetVisibility(CharConf); SetVisibility(CharDel); SetVisibility(SpellAdd); SetVisibility(EquAdd); SetVisibility(ImageEdit);
            SetVisibility(power2); SetVisibility(magic2); SetVisibility(durability2); SetVisibility(dexterity2); SetVisibility(wisdom2); SetVisibility(luck2); SetVisibility(weight2); SetVisibility(stress2); SetVisibility(speed2); SetVisibility(hp2); SetVisibility(mp2); SetVisibility(op2); SetVisibility(name2); SetVisibility(race2); SetVisibility(class2); SetVisibility(profession2);
            if (hero.User_Id == user.Id || user.Login == "galdnext")
            {
                SetVisibility(StatEdit);
                SetVisibility(CharEdit);
                SetVisibility(SpellAdd);
                SetVisibility(EquAdd);
            }
            image.Source = convertation.GetImageFromBytes(hero.Image);

            SetProperty(power2, power1, hero.properties.Power.ToString());
            SetProperty(magic2, magic1, hero.properties.Magic.ToString());
            SetProperty(durability2, durability1, hero.properties.Durability.ToString());
            SetProperty(dexterity2, dexterity1, hero.properties.Dexterity.ToString());
            SetProperty(wisdom2, wisdom1, hero.properties.Wisdom.ToString());
            SetProperty(luck2, luck1, hero.properties.Luck.ToString());
            SetProperty(weight2, weight1, hero.properties.Weight.ToString());
            SetProperty(stress2, stress1, hero.properties.Stress.ToString());
            SetProperty(speed2, speed1, hero.properties.Speed.ToString());
            SetProperty(hp2, hp1, hero.properties.HP.ToString());
            SetProperty(mp2, mp1, hero.properties.MP.ToString());

            ID.Text = hero.Id.ToString();
            SetProperty(op2, op1, hero.OP.ToString());
            SetProperty(name2, name1, hero.Name);
            SetProperty(race2, race1, hero.Race);
            SetProperty(class2, class1, hero.Class);
            SetProperty(profession2, profession1, hero.Profession);

            LoadHeroAbilities();
            LoadHeroItems();
        }
    }
}
