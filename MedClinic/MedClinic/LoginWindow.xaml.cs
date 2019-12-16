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
using System.IO;
using Microsoft.Win32;

namespace MedClinic
{

    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ButtonEnter_click(object sender, RoutedEventArgs e)
        {
            string log, pas;
            string[] str=File.ReadAllText("login.txt").Split(' ');
            log = str[0];
            pas = str[1];
          

            if (LoginTxtBox.Text == log && PassBox.Password.ToString() == pas)
            {
                MessageBox.Show("Welcome");



                MedClinicWindow MCW = new MedClinicWindow();
                this.Close();
                if (MCW.ShowDialog().Value == true)
                {

                }

            }
            else
            {
                MessageBox.Show("Incorrect Login or Password try again!!!");
                LoginTxtBox.Clear();
                PassBox.Password = "";
            }
        }
    }
}
