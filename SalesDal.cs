using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    public class SalesDal
    {
        private const string CONNECTIONSTRING = "Data Source=WIN-0TNQQV8DGU0;Initial Catalog=Sales;Integrated Security=True";

        private string _tableName;
        private string _query;
        public DataSet DataSet { get; set; }

        public SalesDal(string tableName)
        {
            _tableName = tableName;
            _query = string.Format("select * from {0}", _tableName);
            DataSet = new DataSet();
        }

        public void UpdateDatabase()
        {
            using (var connection = new SqlConnection(CONNECTIONSTRING))
            {
                var sqlAdapter = new SqlDataAdapter(string.Format(_query, _tableName), connection);
                var sqlCommandBuilder = new SqlCommandBuilder(sqlAdapter);

                Console.WriteLine(sqlCommandBuilder.GetInsertCommand().CommandText);
                Console.WriteLine(sqlCommandBuilder.GetDeleteCommand().CommandText);
                Console.WriteLine(sqlCommandBuilder.GetUpdateCommand().CommandText);

                sqlAdapter.Update(DataSet);
            }
        }

        private DataSet LoadData()
        {
            //1. Формирование строки запроса
            string sqlQuery = string.Format(_query, _tableName);

            //2. Открытие подключения к базе данных
            using (var connection = new SqlConnection(CONNECTIONSTRING))
            {
                try
                {
                    connection.Open();

                    //3. Создание объекта SqlDataAdapter
                    var sqlDataAdapter = new SqlDataAdapter(sqlQuery, connection);

                    //4. Насыщение объекта DataSet
                    sqlDataAdapter.Fill(DataSet);

                    return DataSet;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            return DataSet;
        }

            //Удалить строку из таблицы авторов по айди
            public static DataSet DropCustomerById(DataSet dataSet, int customerId)
        {
            string sqlQuery = string.Format("DropCustomerNamesById");

            using (var connection = new SqlConnection(CONNECTIONSTRING))
            {
                try
                {
                    connection.Open();

                    var sqlCommand = new SqlCommand(sqlQuery, connection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.Remove(new SqlParameter("@Id", customerId));

                    var adapter = new SqlDataAdapter(sqlCommand);
                    adapter.Fill(dataSet);
                }
                catch(Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
                return dataSet;
            }
        }
    }
}
