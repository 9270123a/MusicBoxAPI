using System.Globalization;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;
using MusicBoxAPI.Interfaces.IRepository;
using MusicBoxAPI.Models;
using MusicBoxAPI.Repository;
using Microsoft.AspNetCore.Authorization;
namespace MusicBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]



    public class AlbumController : ControllerBase
    {


        private readonly IAlbumRepository _albumRepository;

        // 透過依賴注入取得 repository
        public AlbumController(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }
        [HttpGet("Album")]

        public ActionResult<List<AlbumData>> AllMusicInAlbum(Guid AlbumID)
        {
            return Ok(_albumRepository.AllMusicInAlbum(AlbumID));
        }

        [HttpGet("AlbumList")]
        public ActionResult<List<MusicFile>> GetAllAlbum()
        {
            return Ok(_albumRepository.GetAllAlbum());
        }

        [HttpPost("Album")]
        public ActionResult<ResponseModel> GenerateAlbum(string albumName)
        {
            var response = _albumRepository.GenerateAlbum(albumName);
            return Ok(response);
        }

        [HttpDelete("Album")]
        public ActionResult DeleteAlbum(Guid id)
        {
            _albumRepository.DeleteAlbum(id);
            return Ok("刪除成功");
        }

        [HttpDelete("MusicList")]
        public ActionResult DeleteMusicList(Guid id)
        {
            _albumRepository.DeleteMusicList(id);
            return Ok("刪除成功");
        }

        //[HttpPost("FileData")]
        //public ActionResult Upload(MusicFile files)
        //{
        //    if (files.FormFile == null || files.FormFile.Count == 0)
        //    {
        //        return BadRequest("No files uploaded.");
        //    }

        //    var result = _albumRepository.UploadMusic(files);
        //    return Ok(new { count = 1, size = files.FormFile.Sum(f => f.Length) });
        //}



    }
}
