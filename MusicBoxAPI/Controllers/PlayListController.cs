using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;
using MusicBoxAPI.Interfaces.IRepository;
using MusicBoxAPI.Models;
using MusicBoxAPI.Repository;

namespace MusicBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PlayListController : ControllerBase
    {

        private readonly IPlayListRepository _playListRepository;

        // 透過依賴注入取得 repository
        public PlayListController(IPlayListRepository playListRepository)
        {
            _playListRepository = playListRepository;
        }



        [HttpPost("PersonalAlbum")]
        public ActionResult<string> PersonalAlbum(string ListName, string Type)
        {
            _playListRepository.PersonalAlbum(ListName, Type);
            return "建立個人專輯成功";

        }


        [HttpPost("PersonalMusic")]
        public ActionResult<string> PersonalMusic(PersonalMusic personalMusic)
        {
            _playListRepository.PersonalMusic(personalMusic);

            return "加入個人專輯成功";
        }

        [HttpDelete("PersonalAlbum")]
        public ActionResult<string> DeleteAlbum(Guid UserListID)
        {

            _playListRepository.DeleteAlbum(UserListID);
            return "刪除成功";



        }

        [HttpDelete("PersonalMusic")]
        public ActionResult<string> PersonalMusic(Guid MusicID)
        {

            _playListRepository.PersonalMusic(MusicID);

            return "刪除成功";



        }
    }
}
