using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TimeTable
{
    public partial class PupilTimeTable : Window
    {
        string role { get; set; }
        int login { get; set; }
        string connectionString { get; set; }

        public PupilTimeTable(string inrole, int inlogin, string inconnectionString)
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
            tutor_1.Content = "-";
            cabinet_1.Content = "-";

            lesson_2.Content = "-";
            tutor_2.Content = "-";
            cabinet_2.Content = "-";

            lesson_3.Content = "-";
            tutor_3.Content = "-";
            cabinet_3.Content = "-";

            lesson_4.Content = "-";
            tutor_4.Content = "-";
            cabinet_4.Content = "-";

            lesson_5.Content = "-";
            tutor_5.Content = "-";
            cabinet_5.Content = "-";

            lesson_6.Content = "-";
            tutor_6.Content = "-";
            cabinet_6.Content = "-";

            object group;

            string GroupException = "SELECT G.group_name " +
                "FROM Groups as G INNER JOIN( " +
                "Pupils as P INNER JOIN LUser as LU ON P.user_id = LU.user_id) " +
                "ON G.group_id = P.group_id " +
                $"WHERE LU.user_id = '{login}';";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand groupCommand = new SqlCommand(GroupException, connection);
                group = groupCommand.ExecuteScalar();
                int groupint = Convert.ToInt32(group);

                string SqlExpression = "" +
                    "SELECT DL.number_in_day, L.lesson, T.tutor_name, DL.cabinet " +
                    "FROM Groups as G INNER JOIN( " +
                    "dlesson as DL INNER JOIN( " +
                    "Lessons as L INNER JOIN Tutors as T " +
                    "ON L.tutor_id = T.tutor_id) " +
                    "ON DL.lesson_id = L.lesson_id) " +
                    "ON DL.group_id = G.group_id " +
                    $"WHERE group_name = '{groupint}' and dlesson_day = '{Calendar.SelectedDate}' " +
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
                                tutor_1.Content = reader.GetString(2);
                                cabinet_1.Content = reader.GetInt32(3);
                                break;

                            case 2:
                                lesson_2.Content = reader.GetString(1);
                                tutor_2.Content = reader.GetString(2);
                                cabinet_2.Content = reader.GetInt32(3);
                                break;

                            case 3:
                                lesson_3.Content = reader.GetString(1);
                                tutor_3.Content = reader.GetString(2);
                                cabinet_3.Content = reader.GetInt32(3);
                                break;

                            case 4:
                                lesson_4.Content = reader.GetString(1);
                                tutor_4.Content = reader.GetString(2);
                                cabinet_4.Content = reader.GetInt32(3);
                                break;

                            case 5:
                                lesson_5.Content = reader.GetString(1);
                                tutor_5.Content = reader.GetString(2);
                                cabinet_5.Content = reader.GetInt32(3);
                                break;

                            case 6:
                                lesson_6.Content = reader.GetString(1);
                                tutor_6.Content = reader.GetString(2);
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
