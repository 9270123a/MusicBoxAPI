using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;
using MusicBoxAPI.Models;

namespace MusicBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : ControllerBase
    {





        [HttpPost("PersonalAlbum")]
        public string PersonalAlbum(string ListName, string Type)
        {
            DataBase dataBase = new DataBase();
            Guid newGuid = Guid.NewGuid();
            string sql = $"INSERT INTO UserPlayList(UserListID, ListName, Type) VALUES ('{newGuid}', '{ListName}', '{Type}')";
            dataBase.ExecuteSqlCommand(sql);
            return "建立個人專輯成功";
        }


        [HttpPost("PersonalMusic")]
        public string PersonalMusic(PersonalMusic personalMusic)
        {
            DataBase dataBase = new DataBase();
            Guid newGuid = Guid.NewGuid();
            string MusicName = "123";
            Guid Userid = Guid.Parse("26485b67-9d4f-4448-a1b4-7546361c8590");
            string sql = $"INSERT INTO UserMusicDetail(ID, UserID, MusicName, UserListID, MusicID) VALUES ('{newGuid}', '{Userid}', '{MusicName}', '{personalMusic.UserListID}', '{personalMusic.MusicID}')";
            dataBase.ExecuteSqlCommand(sql);
            return "加入個人專輯成功";
        }

        [HttpDelete("PersonalAlbum")]
        public string DeleteAlbum(Guid UserListID)
        {
            DataBase dataBase = new DataBase();
            string sql = $"DELETE FROM UserPlayList WHERE UserLIstID = '{UserListID}'";
            string sql2 = $"DELETE FROM UserMusicDetail WHERE UserLIstID = '{UserListID}'";
            dataBase.ExecuteSqlCommand(sql2);
            dataBase.ExecuteSqlCommand(sql);

            return "刪除成功";



        }

        [HttpDelete("PersonalMusic")]
        public string PersonalMusic(Guid MusicID)
        {
            DataBase dataBase = new DataBase();
            string sql = $"DELETE FROM UserMusicDetail WHERE MusicID = '{MusicID}'";
            dataBase.ExecuteSqlCommand(sql);

            return "刪除成功";



        }
    }
}
