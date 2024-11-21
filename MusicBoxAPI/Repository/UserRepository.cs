using MusicBoxAPI.Entity;
using System.Data;
using MusicBoxAPI.Interfaces.IRepository;
using Dapper;

namespace MusicBoxAPI.Repository
{
    public class UserRepository: IUserRepository
    {

        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public UserRepository(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public UserData GetUserByUsername(string Name)
        {
            const string query = "SELECT * FROM Users WHERE Name = @Name";
            var parameters = new { Name = Name };
            return _connection.Query<UserData>(query, parameters).FirstOrDefault();
        }

        public string RegisterUser(UserData userdata)
        {
            string query = @"INSERT INTO Users (UserId, Name, Mail, Password) 
                    VALUES (@UserId, @Name, @Email, @Password)";

            _connection.Execute(query, new
            {
                UserId = userdata.UserId,
                Name = userdata.Name,
                Email = userdata.Email,
                Password = userdata.Password
            });

            return "註冊成功 ";

        }



            
    }
}
