using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Bondarenko_LibraryDataBase
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Кнопка "Забыл"
        private void Forgot_Button(object sender, RoutedEventArgs e)
        {
            forgotWindow forgot = new forgotWindow();
            forgot.Show();
            Close();
        }


        //Кнопка "Регистрация"
        private void Reg_Button(object sender, RoutedEventArgs e)
        {
            regWindow reg = new regWindow();
            reg.Show();
            Close();
        }


        //Кнопка "Вход"
        private void Enter_Button(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(LoginBox, PassBox))
            {
                MessageBox.Show("Данные не введены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (LoginCheck(LoginBox))
                {
                    MessageBox.Show("Логин должен быть вида <>@example.com", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (PassCheck(PassBox))
                {
                    MessageBox.Show("Пароль должен быть длинной 6 символов и содержать только латинские буквы и цифры.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DataBase(LoginBox, PassBox);
                }
            }
        }


        //Проверка полей
        private bool IsEmpty(TextBox textBox, PasswordBox passBox)
        {
            bool result = false;

            if (textBox.Text == "")
            {
                result = true;
            }

            if (passBox.Password == "")
            {
                result = true;
            }

            return result;
        }

        private bool LoginCheck(TextBox textBox)
        {
            bool result = false;

            string regex = (@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$");

            if (!(Regex.IsMatch(textBox.Text, regex, RegexOptions.IgnoreCase)))
            {
                result = true;
            }

            return result;
        }

        private bool PassCheck(PasswordBox passBox)
        {
            bool result = false;

            string regex = (@"[a-z0-9]");

            if (passBox.Password.Length < 6)
            {
                result = true;
            }
            else if(!(Regex.IsMatch(passBox.Password, regex, RegexOptions.IgnoreCase)))
            {
                result = true;
            }

            return result;
        }


        //БД
        private void DataBase(TextBox LoginBox, PasswordBox PassBox)
        {
            string connectionString = @"Data Source=(localDB);Initial Catalog=(DBName);Integrated Security=false;User ID=(UserID);Password=(Password)";
            string SqlExpression = "" +
                "SELECT login " +
                "FROM LUser " +
                $"WHERE login = '{LoginBox.Text}'";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(SqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    string login = reader.GetString(1);
                    MessageBox.Show($"{login}");
                }
                else
                {
                    MessageBox.Show("Пользователь не существует", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                reader.Close();
                connection.Close();
                
            }
        }
    }
}
