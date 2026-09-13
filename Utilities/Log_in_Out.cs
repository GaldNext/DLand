using DLand.backend.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DLand.Utilities
{
    public class Log_in_Out:Database
    {
        Load l;
        public Log_in_Out(Load l) { this.l = l; }
        public async Task Login(TextBox login, TextBox password, StackPanel sp, WrapPanel wp, TextBox tb, User ThisUser)
        {
            try
            {
                var user = await UserGetByNameAndPassword(login.Text.ToLower().Trim(), password.Text.Trim());
                if (user != null)
                {
                    ThisUser = user;
                    l.CheckUser(user);

                    if (user.Login == "galdnext")
                    {
                        MessageBox.Show("Галд");
                    }
                    await l.LoadUserHeroes(sp, user);
                    await l.LoadHeroes(wp, tb);

                    l.icon.Visibility = Visibility.Visible;
                    l.ChangeTAB(l.a);
                    l.ChangeTAB(l.b);
                    l.b.IsSelected = true;
                    l.Snack($"Успешный вход", null, null, TimeSpan.FromSeconds(4));
                }
                else
                {
                    l.Snack("Пользователь не найден", null, null, TimeSpan.FromSeconds(4));
                    var item = await HeroItemsGetById(1);
                    MessageBox.Show($"{item[0].Id}, {item[0].Item[0]}, {item[0].Item[1]}, {item[0].ItemWeight}");
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                l.Snack($"Error: {ex.Message}", null, null, TimeSpan.FromSeconds(4));
            }
        }
    }
}
