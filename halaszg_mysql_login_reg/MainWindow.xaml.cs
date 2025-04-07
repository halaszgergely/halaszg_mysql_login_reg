using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace halaszg_mysql_login_reg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //kapcsolati string
        MySqlConnection connect = new MySqlConnection("server = server.fh2.hu;database = v2labgwj_11a;uid = v2labgwj_11a;password = VGFR2GJjqudMt8Q4SA5j;");
        public MainWindow()
        {
            InitializeComponent();
            lbKiir();
        }

        private void lbKiir()
        {
            //listbox feltöltése a felhasználókkal
            connect.Open();
            var sql = "SELECT * FROM halaszg_user";
            MySqlCommand cmd = new MySqlCommand(sql, connect);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //listbox feltöltése
                lb.Items.Add(reader["nev"].ToString());
            }
            reader.Close();
            connect.Close();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            //kapcsolat megnyitása
            connect.Open();
            //parancs létrehozása
            var sql = $"SELECT * FROM halaszg_user WHERE nev = '{txtUser.Text}' AND jelszo = '{txtPass.Password}'";
            lbDebug.Content = sql;
            MySqlCommand cmd = new MySqlCommand(sql, connect);
            //parancs végrehajtása
            var reader = cmd.ExecuteReader();
            //ha van ilyen felhasználó
            if (reader.Read())
            {
                MessageBox.Show("Sikeres bejelentkezés");
            }
            else
            {
                MessageBox.Show("Sikertelen bejelentkezés");
            }
            reader.Close();
            connect.Close();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            //1. Ha a két jelszó nem egyezik nem enged tovább
            if (reg_pass.Password != reg_passAgain.Password)
            {
                MessageBox.Show("A két jelszó nem egyezik");
                return;
            }
            connect.Open();
            //2. Ha a felhasználónév már létezik nem enged tovább
            var reader = new MySqlCommand($"SELECT * FROM halaszg_user WHERE nev = '{reg_user.Text}'", connect).ExecuteReader();
            if (reader.Read())
            {
                MessageBox.Show("Ez a felhasználónév már létezik");
            }
            else
            {
                //3. Ha a felhasználónév és a jelszó is megvan, akkor regisztrál
                reader.Close();
                var sql = $"INSERT INTO halaszg_user (nev, jelszo) VALUES ('{reg_user.Text}', '{reg_pass.Password}')";
                lbDebug.Content = sql;
                new MySqlCommand(sql, connect).ExecuteNonQuery();
                MessageBox.Show("Sikeres regisztráció");
            }
            connect.Close();
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            //kiválasztott felhasználó jelszavának megváltoztatása
            if (lb.SelectedItem != null)
            {
                //jelszó ellenőrzése
                connect.Open();
                var sql = $"SELECT * FROM halaszg_user WHERE nev = '{lb.SelectedItem}' AND jelszo = '{changePassCurrent.Password}'";
                lbDebug.Content = sql;
                MySqlCommand cmd = new MySqlCommand(sql, connect);
                //parancs végrehajtása
                var reader = cmd.ExecuteReader();
                //ha van ilyen felhasználó
                if (reader.Read())
                {
                    if(changePass.Password == changePassAgain.Password)
                    {
                        reader.Close();
                        //jelszó megváltoztatása
                        sql = $"UPDATE halaszg_user SET jelszo = '{changePass.Password}' WHERE nev = '{lb.SelectedItem}';";
                        lbDebug.Content = sql;
                        new MySqlCommand(sql, connect).ExecuteNonQuery();
                        MessageBox.Show("Sikeres jelszóváltoztatás");
                    }
                    else
                    {
                        MessageBox.Show("A két jelszó nem egyezik");
                    }
                }
                else
                {
                    MessageBox.Show("Rossz a jelenlegi jelszó.");
                }
                connect.Close();
                reader.Close();
            }
            else
            {
                MessageBox.Show("Nincs kiválasztott felhasználó");
            }
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (deleteSure.IsChecked == true)
            {

            }
        }
    }
}
