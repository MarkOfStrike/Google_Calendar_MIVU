using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Google_Calendar_Desktop_App
{
    public class WorkBD
    {
        private static SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder {
            MultipleActiveResultSets = true,
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            AttachDBFilename = @"C:\Users\Prese\source\repos\Google_Calendar_Desktop_App\Calendar_Events.mdf",
            IntegratedSecurity = true
        };


        /// <summary>
        /// Метод выполнения запроса
        /// </summary>
        /// <param name="query">Запрос</param>
        public static void Execution_query(string query)
        {
            using (SqlConnection connection = new SqlConnection(cs.ToString()))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Выполнение Select запросов
        /// </summary>
        /// <param name = "query" > Запрос </ param >
        /// < returns > Данные выборки</returns>
        public static DataTable Select_query(string query)
        {
            using (SqlConnection connection = new SqlConnection(cs.ToString()))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                return dt;
            }



        }

    }
}
