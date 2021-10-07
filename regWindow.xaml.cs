using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Bondarenko_LibraryDataBase
{
    public partial class regWindow : Window
    {
        public regWindow()
        {
            InitializeComponent();
        }


        //Кнопка "Назад"
        private void Back_Button(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }


        //Кнопка "Подтверждение"
        private void AcceptButton(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(LoginBox, PassBox, AgPassBox, QuestionBox, AnswerBox))
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
                else if (PassCheck(AgPassBox))
                {
                    MessageBox.Show("Пароль должен быть длинной 6 символов и содержать только латинские буквы и цифры.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (!(PassBox.Password == AgPassBox.Password))
                {
                    MessageBox.Show("Пароли не совпадают.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DataBase(LoginBox, PassBox);
                }
            }
        }


        //Проверка полей
        private bool IsEmpty(TextBox textBox, PasswordBox passBox, PasswordBox agpassBox, ComboBox questionBox, TextBox answerBox)
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

            if (agpassBox.Password == "")
            {
                result = true;
            }

            if (questionBox.SelectedItem == null)
            {
                result = true;
            }

            if (answerBox.Text == "")
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
            else if (!(Regex.IsMatch(passBox.Password, regex, RegexOptions.IgnoreCase)))
            {
                result = true;
            }

            return result;
        }


        //БД
        private void DataBase(TextBox LoginBox, PasswordBox PassBox)
        {
            //TODO: Логика запроса БД
        }
    }
}
