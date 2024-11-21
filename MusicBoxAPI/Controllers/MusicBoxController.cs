using System.Data.SqlClient;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;
using MusicBoxAPI.Interfaces.IRepository;
using MusicBoxAPI.Repository;

namespace MusicBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class MusicBoxController : ControllerBase
    {

        private readonly IMusicBoxRepository _musicBoxRepository;

        // 透過依賴注入取得 repository
        public MusicBoxController(IMusicBoxRepository musicBoxRepository)
        {
            _musicBoxRepository = musicBoxRepository;
        }


        [HttpGet("GetListID")]
        public ActionResult<List<MusicListData>> GetListID(Guid userID)
        {
            return Ok(_musicBoxRepository.GetListID(userID));
        }

        [HttpGet("UserMusicDetailData")]
        public ActionResult<List<UserMusicDetailData>> AllMusicInList(Guid ID)
        {
            return Ok(_musicBoxRepository.AllMusicInList(ID));
        }




    }
}
