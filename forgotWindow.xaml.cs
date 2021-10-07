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
    public partial class forgotWindow : Window
    {
        public forgotWindow()
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

        //Кнопка "Подтвердить"
        private void Accept_Button(object sender, RoutedEventArgs e)
        {
            if (IsEmpty(LoginBox, AnswerBox))
            {
                MessageBox.Show("Данные не введены", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                if (LoginCheck(LoginBox))
                {
                    MessageBox.Show("Логин должен быть вида <>@example.com", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    DataBase(LoginBox, AnswerBox);
                }
            }
        }


        //Проверка полей
        private bool IsEmpty(TextBox textBox, TextBox answerBox)
        {
            bool result = false;

            if (textBox.Text == "")
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


        //БД
        private void DataBase(TextBox LoginBox, TextBox answerBox)
        {
            //TODO: Логика запроса БД
        }
    }
}
