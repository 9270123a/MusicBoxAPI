using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;

namespace MusicBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicBoxController : ControllerBase
    {


        [HttpGet("GetListID")]
        public List<MusicListData> GetListID(Guid userID)
        {
            DataBase dataBase = new DataBase();
            string sql = $"select * from UserMusicDetail where userID = '{userID.ToString()}'";

            return dataBase.Query<MusicListData>(sql);
        }

        [HttpGet("UserMusicDetailData")]
        public List<UserMusicDetailData> AllMusicInList(Guid ID)
        {
            DataBase dataBase = new DataBase();
            string sql = $"select * from UserMusicDetail where ID = '{ID.ToString()}'";

            return dataBase.Query<UserMusicDetailData>(sql);
        }




    }
}
