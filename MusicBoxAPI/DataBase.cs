using System;
using System.Data.SqlClient;
using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace MusicBoxAPI
{
    public class DataBase
    {
        private readonly string _connectionString = "Server=localhost; Database=musicbox; Integrated Security=True;";

        //同步版本 - 查詢多筆資料
        public List<T> Query<T>(string sql, object param = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Console.WriteLine($"[同步] 開始連線，時間: {DateTime.Now:HH:mm:ss.fff}");
                connection.Open();
                var result = connection.Query<T>(sql, param).ToList();
                Console.WriteLine($"[同步] 結束連線，時間: {DateTime.Now:HH:mm:ss.fff}");
                return result;
            }
        }

        // 同步版本 - 查詢單筆資料
        public T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Console.WriteLine($"[同步] 開始連線，時間: {DateTime.Now:HH:mm:ss.fff}");
                connection.Open();
                var result = connection.QueryFirstOrDefault<T>(sql, param);
                Console.WriteLine($"[同步] 結束連線，時間: {DateTime.Now:HH:mm:ss.fff}");
                return result;
            }
        }

        // 同步版本 - 執行命令
        public int ExecuteSqlCommand(string sql, object param = null)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                Console.WriteLine($"[同步] 開始連線，時間: {DateTime.Now:HH:mm:ss.fff}");
                connection.Open();
                var result = connection.Execute(sql, param);
                Console.WriteLine($"[同步] 結束連線，時間: {DateTime.Now:HH:mm:ss.fff}");
                return result;
            }
        }

        // 非同步版本 - 查詢多筆資料
        //public async Task<IEnumerable<T>> Query<T>(string sql, object param = null)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        Console.WriteLine($"[非同步] 開始連線，時間: {DateTime.Now:HH:mm:ss.fff}");
        //        await connection.OpenAsync();
        //        var result = await connection.QueryAsync<T>(sql, param);
        //        Console.WriteLine($"[非同步] 結束連線，時間: {DateTime.Now:HH:mm:ss.fff}");
        //        return result;
        //    }
        //}

        //// 非同步版本 - 查詢單筆資料
        //public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        Console.WriteLine($"[非同步] 開始連線，時間: {DateTime.Now:HH:mm:ss.fff}");
        //        await connection.OpenAsync();
        //        var result = await connection.QueryFirstOrDefaultAsync<T>(sql, param);
        //        Console.WriteLine($"[非同步] 結束連線，時間: {DateTime.Now:HH:mm:ss.fff}");
        //        return result;
        //    }
        //}

        //// 非同步版本 - 執行命令
        //public async Task<int> ExecuteSqlCommand(string sql, object param = null)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        Console.WriteLine($"[非同步] 開始連線，時間: {DateTime.Now:HH:mm:ss.fff}");
        //        await connection.OpenAsync();
        //        var result = await connection.ExecuteAsync(sql, param);
        //        Console.WriteLine($"[非同步] 結束連線，時間: {DateTime.Now:HH:mm:ss.fff}");
        //        return result;
        //    }
        //}

        //    // 模擬耗時操作的測試方法 - 同步版本
        //    public List<T> QueryWithDelay<T>(string sql, int delayMs = 2000)
        //    {
        //        using (SqlConnection connection = new SqlConnection(_connectionString))
        //        {
        //            var startTime = DateTime.Now;
        //            Console.WriteLine($"[同步] 開始查詢，時間: {startTime:HH:mm:ss.fff}");
        //            connection.Open();

        //            // 模擬耗時操作
        //            Thread.Sleep(delayMs);

        //            var result = connection.Query<T>(sql).ToList();
        //            var endTime = DateTime.Now;
        //            Console.WriteLine($"[同步] 結束查詢，時間: {endTime:HH:mm:ss.fff}");
        //            Console.WriteLine($"[同步] 總耗時: {(endTime - startTime).TotalMilliseconds}ms");
        //            return result;
        //        }
        //    }

        //    // 模擬耗時操作的測試方法 - 非同步版本
        //    public async Task<IEnumerable<T>> QueryWithDelayAsync<T>(string sql, int delayMs = 2000)
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        {
        //            var startTime = DateTime.Now;
        //            Console.WriteLine($"[非同步] 開始查詢，時間: {startTime:HH:mm:ss.fff}");
        //            await connection.OpenAsync();

        //            // 模擬耗時操作
        //            await Task.Delay(delayMs);

        //            var result = await connection.QueryAsync<T>(sql);
        //            var endTime = DateTime.Now;
        //            Console.WriteLine($"[非同步] 結束查詢，時間: {endTime:HH:mm:ss.fff}");
        //            Console.WriteLine($"[非同步] 總耗時: {(endTime - startTime).TotalMilliseconds}ms");
        //            return result;
        //        }
        //    }
        //}

        // ADO.NET (C# 原生讀取資料庫寫法)
        //  List<User> list = new List<User>();
        //  SqlCommand command = new SqlCommand(sql,connection);
        //  SqlDataReader reader = command.ExecuteReader();
        //  while(!reader.IsEnd) {
        //      User user = new User();
        //      user.name = rader["Name"];
        //      user.Phone = rader["Phnoe"];
        //      user.Address = rader["Address"];
        //      user.UserID = reader["UserID"];
        //      list.Add(user);
        //  }
        //
        //
        //  foreach(User user in list) {
        //      Console.WriteLine(user.Name);
        //  }
        //
        //
        //  Dapper 寫法
        //  List<User> list = connection.Query<User>(sql);
    }
}