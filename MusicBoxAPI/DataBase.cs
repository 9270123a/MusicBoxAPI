using System;
using System.Data.SqlClient;
using Dapper;

namespace MusicBoxAPI
{
    class DataBase
    {
        string connectionString = "Server=localhost; Database=musicbox; Integrated Security=True;";

        public List<T> Query<T>(string sql)
        {
            List<T> list = new List<T>();

           
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("数据库连接成功.");
                    return connection.Query<T>(sql).ToList();

                }
                

            return list; 
        }

        public void ExecuteSqlCommand(string sql)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("数据库连接成功.");

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.ExecuteNonQuery(); // 执行非查询命令
                }
            }
        }



    }
}
