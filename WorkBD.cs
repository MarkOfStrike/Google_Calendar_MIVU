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
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Prese\source\repos\Google_Calendar_Desktop_App\Calendar_Events.mdf;Integrated Security=True";

        #region 1 вариант



        private static SqlConnection connection = new SqlConnection(connectionString);

        /// <summary>
        /// Метод выполнения запроса
        /// </summary>
        /// <param name="query">Запрос</param>
        public static void Execution_query(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                command.ExecuteNonQuery();
                //MessageBox.Show("Команда выполнена");
            }
        }

        /// <summary>
        /// Выполнение Select запросов
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Данные выборки</returns>
        public static SqlDataReader Select_query(string query)
        {

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            //reader.Close();



            return reader;
        }

        public static void Open_con()
        {
            connection.Open();
        }

        public static void Close_con()
        {
            connection.Close();
        }

        #endregion


        #region 2 Вариант

        ///// <summary>
        ///// Метод выполнения запроса
        ///// </summary>
        ///// <param name="query">Запрос</param>
        //public static void Execution_query(string query)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.ExecuteNonQuery();
        //        MessageBox.Show("Команда выполнена");
        //    }
        //}

        ///// <summary>
        ///// Выполнение Select запросов
        ///// </summary>
        ///// <param name="query">Запрос</param>
        ///// <returns>Данные выборки</returns>
        //public static SqlDataReader Select_query(string query)
        //{

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        SqlCommand command = new SqlCommand(query, connection);
        //        SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);

        //        DataTable data = new DataTable();



        //        //data.Load(reader);

        //        //reader.Close();



        //        return reader;
        //    }


        //}


        #endregion


    }
}
