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
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Prese\source\repos\Google_Calendar_Desktop_App\Calendar_Events.mdf;Integrated Security=True;MultipleActiveResultSets=True";

        private static SqlConnectionStringBuilder cs = new SqlConnectionStringBuilder {
            MultipleActiveResultSets = true,
            DataSource = @"(LocalDB)\MSSQLLocalDB",
            AttachDBFilename = @"C:\Users\Prese\source\repos\Google_Calendar_Desktop_App\Calendar_Events.mdf",
            IntegratedSecurity = true
        };

        #region 1 вариант



        private static SqlConnection connection = new SqlConnection(cs.ToString());

        /// <summary>
        /// Метод выполнения запроса
        /// </summary>
        /// <param name="query">Запрос</param>
        public async static void Execution_query(string query)
        {
            using (SqlConnection connection = new SqlConnection(cs.ToString()))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(query, connection);
                await command.ExecuteNonQueryAsync();
                //connection.Close();
                //MessageBox.Show("Команда выполнена");
            }
        }

        ///// <summary>
        ///// Метод выполнения запроса
        ///// </summary>
        ///// <param name="query">Запрос</param>
        //public static void Execution_query(string query)
        //{
        //        SqlCommand command = new SqlCommand(query, connection);
        //        command.ExecuteNonQuery();
        //        //MessageBox.Show("Команда выполнена");

        //}


        ///// <summary>
        ///// Выполнение Select запросов
        ///// </summary>
        ///// <param name="query">Запрос</param>
        ///// <returns>Данные выборки</returns>
        //public static SqlDataReader Select_query(string query)
        //{

        //    SqlCommand command = new SqlCommand(query, connection);
        //    SqlDataReader reader = command.ExecuteReader();

        //    //reader.Close();


        //    return reader;
        //}


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
                //connection.Close();
                return dt;
            }



        }




        public static void Open_con()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            
        }

        public static void Close_con()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            
        }


        public static string Encode_text(string text, bool side)
        {
            Encoding source = null;
            Encoding end = null;

            if (side)
            {
                source = Encoding.Default;
                end = Encoding.UTF8;

                return text;

            }
            else
            {
                source = Encoding.UTF8;
                end = Encoding.Default;

                byte[] sourceText = end.GetBytes(text);
                byte[] endText = Encoding.Convert(source, end, sourceText);

                return end.GetString(endText);
            }

            
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
