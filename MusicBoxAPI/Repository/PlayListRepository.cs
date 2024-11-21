using System.Data;
using System.Runtime.Intrinsics.Arm;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using MusicBoxAPI.Interfaces.IRepository;
using MusicBoxAPI.Interfaces.IService;
using MusicBoxAPI.Models;

namespace MusicBoxAPI.Repository
{
    public class PlayListRepository : IPlayListRepository
    {

        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public PlayListRepository(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public string PersonalAlbum(string ListName, string Type)
        {
            Guid newGuid = Guid.NewGuid();
            string query = @"INSERT INTO UserPlayList (UserListID, ListName, Type) 
                    VALUES (@UserListID, @ListName, @Type)";

            _connection.Execute(query, new
            {
                UserListID = Guid.NewGuid(),
                ListName = ListName,
                Type = Type
            });
            return "建立個人專輯成功";
        }
        public string PersonalMusic(PersonalMusic personalMusic)
        {
            string MusicName = "123";
            Guid Userid = Guid.Parse("26485b67-9d4f-4448-a1b4-7546361c8590");
            string query = $"INSERT INTO UserMusicDetail(ID, UserID, MusicName, UserListID, MusicID) VALUES " +
                $"(@ID, @UserID, @MusicName, @UserListID, @MusicID)";


            _connection.Execute(query, new
            {
                ID = Guid.NewGuid(),
                UserID = Userid,
                MusicName = MusicName,
                UserListID = personalMusic.UserListID,
                MusicID = personalMusic.MusicID
            });
            return "加入個人專輯成功";
        }

        public string DeleteAlbum(Guid UserListID)
        {
          
            string sql = $"DELETE FROM UserPlayList WHERE UserLIstID = @UserListID";
            string sql2 = $"DELETE FROM UserMusicDetail WHERE UserLIstID = @UserListID";
            _connection.Execute(sql2, new
            {
                UserListID = UserListID
            });
            _connection.Execute(sql, new
            {
                UserListID = UserListID
            });

            return "刪除成功";
        }

        public string PersonalMusic(Guid MusicID)
        {
            string sql = $"DELETE FROM UserMusicDetail WHERE MusicID = @MusicID";
            _connection.Execute(sql, new
            {
                MusicID = MusicID
            }); 

            return "刪除成功";

        }


    }
}
