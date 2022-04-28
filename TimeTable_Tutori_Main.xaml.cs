using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TimeTable
{
    /// <summary>
    /// Логика взаимодействия для TimeTable_Tutori_Main.xaml
    /// </summary>
    public partial class TimeTable_Tutori_Main : Window
    {
        string role { get; set; }
        int login { get; set; }
        string connectionString { get; set; }

        public TimeTable_Tutori_Main(string inrole, int inlogin, string inconnectionString)
        {
            role = inrole;
            login = inlogin;
            connectionString = inconnectionString;
            InitializeComponent();
            page_label.Content += " " + login;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            lesson_1.Content = "-";
            group_1.Content = "-";
            cabinet_1.Content = "-";

            lesson_2.Content = "-";
            group_2.Content = "-";
            cabinet_2.Content = "-";

            lesson_3.Content = "-";
            group_3.Content = "-";
            cabinet_3.Content = "-";

            lesson_4.Content = "-";
            group_4.Content = "-";
            cabinet_4.Content = "-";

            lesson_5.Content = "-";
            group_5.Content = "-";
            cabinet_5.Content = "-";

            lesson_6.Content = "-";
            group_6.Content = "-";
            cabinet_6.Content = "-";

            object tutor;

            string GroupException = "SELECT T.tutor_name " +
                "FROM Tutors as T INNER JOIN LUser as L " +
                "ON L.user_id = T.user_id " +
                $"WHERE L.user_id = {login};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand tutorCommand = new SqlCommand(GroupException, connection);
                tutor = tutorCommand.ExecuteScalar();
                string tutorstring = Convert.ToString(tutor);

                string SqlExpression = "" +
                    "SELECT DL.number_in_day, L.lesson, G.group_name, DL.cabinet " +
                    "FROM Groups as G INNER JOIN( " +
                    "dlesson as DL INNER JOIN( " +
                    "Lessons as L INNER JOIN Tutors as T " +
                    "ON L.tutor_id = T.tutor_id) " +
                    "ON DL.lesson_id = L.lesson_id) " +
                    "ON DL.group_id = G.group_id " +
                    $"WHERE tutor_name = '{tutorstring}' and dlesson_day = '{Calendar.SelectedDate}' " +
                    $"ORDER BY number_in_day;";

                SqlCommand command = new SqlCommand(SqlExpression, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        switch (reader.GetInt32(0))
                        {
                            case 1:
                                lesson_1.Content = reader.GetString(1);
                                group_1.Content = reader.GetString(2);
                                cabinet_1.Content = reader.GetInt32(3);
                                break;

                            case 2:
                                lesson_2.Content = reader.GetString(1);
                                group_2.Content = reader.GetString(2);
                                cabinet_2.Content = reader.GetInt32(3);
                                break;

                            case 3:
                                lesson_3.Content = reader.GetString(1);
                                group_3.Content = reader.GetString(2);
                                cabinet_3.Content = reader.GetInt32(3);
                                break;

                            case 4:
                                lesson_4.Content = reader.GetString(1);
                                group_4.Content = reader.GetString(2);
                                cabinet_4.Content = reader.GetInt32(3);
                                break;

                            case 5:
                                lesson_5.Content = reader.GetString(1);
                                group_5.Content = reader.GetString(2);
                                cabinet_5.Content = reader.GetInt32(3);
                                break;

                            case 6:
                                lesson_6.Content = reader.GetString(1);
                                group_6.Content = reader.GetString(2);
                                cabinet_6.Content = reader.GetInt32(3);
                                break;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("На этот день расписания нет!", "Нет расписания", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                reader.Close();
                connection.Close();
            }
        }
    }
}
