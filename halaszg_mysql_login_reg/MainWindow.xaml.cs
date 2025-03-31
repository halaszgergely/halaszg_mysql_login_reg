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
        MySqlConnection connect = new MySqlConnection("server = localhost;database = asztali_11a;uid = root;password = '';");
        public MainWindow()
        {
            InitializeComponent();

        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            //kapcsolat megnyitása
            connect.Open();
            //parancs létrehozása
            var sql = $"SELECT * FROM user WHERE nev = '{txtUser.Text}' AND jelszo = '{txtPass.Password}'";
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
            connect.Close();

        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
