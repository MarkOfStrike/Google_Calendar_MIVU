using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Google_Calendar_Desktop_App
{
    public class WorkBD
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Prese\source\repos\Google_Calendar_Desktop_App\Calendar_Events.mdf;Integrated Security=True";

        /// <summary>
        /// Метод выполнения запроса
        /// </summary>
        /// <param name="query">Запрос</param>
        public void Execution_query(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                MessageBox.Show("Команда выполнена");
            }
        }

        /// <summary>
        /// Выполнение Select запросов
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Данные выборки</returns>
        public SqlDataReader Select_query(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                reader.Close();

                return reader;
            }
        }


    }
}
