using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using TimeTable.DataBase.Models;

namespace TimeTable
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //Кнопка "Забыл"
        private void Forgot_Button(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBox.Show("Обратитесь к администратору информационной системы для восстановления пароля или регистрации в базе данных.", "Регистрация или забыл пароль", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    if (DataBase(LoginBox, PassBox))
                    {
                        MessageBox.Show("OK");
                    }
                    else
                    {
                        MessageBox.Show("NO");
                    }
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
        private bool DataBase(TextBox LoginBox, PasswordBox PassBox)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = db.users.ToList();
                foreach (User usr in users)
                {
                    if ((usr.Login == LoginBox.Text) && (usr.Password == PassBox.Password))
                    {
                        return true;
                    }
                }
                return false;
            }
            //string SqlExpression = "" +
            //    "SELECT user_id " +
            //    "FROM LUser " +
            //    $"WHERE login = '{LoginBox.Text}' and password = '{PassBox.Password}'";

            //using(SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    connection.Open();
            //    SqlCommand command = new SqlCommand(SqlExpression, connection);
            //    object count = command.ExecuteScalar();

            //    if (count != null)
            //    {
            //        int login = (int)count;
            //        string Role = RoleCheck(login, connection);

            //        switch (Role)
            //        {

            //            case "pup":
            //                PupilTimeTable TT_Pup_Main = new PupilTimeTable(Role, login, connectionString);
            //                TT_Pup_Main.Show();
            //                Close();
            //                break;

            //            case "tut":
            //                TutorTimeTable TT_Tut_Main = new TutorTimeTable(Role, login, connectionString);
            //                TT_Tut_Main.Show();
            //                Close();
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Неправильный логин или пароль", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }

            //    //reader.Close();
            //    connection.Close();
                
            //}
        }

        private string RoleCheck(int login, SqlConnection connection)
        {
            string result = "";

            //PupCheck
            string SqlPupExpression = "SELECT is_pup " +
                "FROM LUser " +
                $"WHERE user_id = '{login}'";

            SqlCommand PupCommand = new SqlCommand(SqlPupExpression, connection);
            object isPup = PupCommand.ExecuteScalar();
            if ((bool)isPup == true)
            {
                result = "pup";
            }

            //TutCheck
            string SqlTutExpression = "SELECT is_tut " +
                "FROM LUser " +
                $"WHERE user_id = '{login}'";

            SqlCommand TutCommand = new SqlCommand(SqlTutExpression, connection);
            object isTut = TutCommand.ExecuteScalar();
            if ((bool)isTut == true)
            {
                result = "tut";
            }

            return result;
        }
    }
}
