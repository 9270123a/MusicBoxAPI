using System.Globalization;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;
using MusicBoxAPI.Models;

namespace MusicBoxAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {

        [HttpGet("Album")]
        public List<AlbumData> GetMusicList()
        {
            DataBase dataBase = new DataBase();

            return dataBase.Query<AlbumData>("select * from Album");
        }

        [HttpGet("AlbumList")]
        public List<MusicData> AllMusicInAlbumList(Guid AlbumID)
        {
            DataBase dataBase = new DataBase();
            string sql = $"select * from musicList where AlbumID = '{AlbumID.ToString()}'";

            return dataBase.Query<MusicData>(sql);
        }

        [HttpPost("Album")]
        public ResponseModel GenerateAlbum(string AlbumName)
        {
            DataBase dataBase = new DataBase();

            Guid newGuid = Guid.NewGuid(); // 生成新的 GUID
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            string sql = $"INSERT INTO Album(albumid, albumname, date) VALUES ('{newGuid}', '{AlbumName}', '{formattedDate}')";
            dataBase.ExecuteSqlCommand(sql);
            ResponseModel responseModel = new ResponseModel();
            responseModel.Message = "成功建立";
            responseModel.NewGuid = newGuid;
            responseModel.FileName = AlbumName;
            return responseModel;

        }

        [HttpDelete("Album")]
        public string DeleteAlbum(Guid ID)
        {
            DataBase dataBase = new DataBase();
            string sql = $"DELETE FROM Album WHERE albumID = '{ID}'";
            dataBase.ExecuteSqlCommand(sql);
            return "刪除成功";
        }

        [HttpDelete("MusicList")]
        public string DeleteMusicList(Guid ID)
        {
            DataBase dataBase = new DataBase();
            string sql = $"DELETE FROM Music WHERE musicID = '{ID}'";
            string sql2 = $"DELETE FROM UserMusicDetail WHERE musicID = '{ID}'";
            dataBase.ExecuteSqlCommand(sql2);
            dataBase.ExecuteSqlCommand(sql);
            return "刪除成功";
        }
        //[HttpPost("FileData")]
        //public async Task<IActionResult> Upload(List<MusicFile> files)
        //{
        //    var size = files.Sum(f => f.Length);

        //    foreach (var file in files)
        //    {
        //        if (file.Length > 0)
        //        {

        //            var path = @"C:\Users\92701\OneDrive\桌面\musicData";
        //            using (var stream = new FileStream(path+"\\"+file.FileName, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //        }
        //    }

        //    return Ok(new { count = files.Count, size });
        //}
        [HttpPost("FileData")]
        public async Task<IActionResult> Upload([FromForm] MusicFile files)
        {
            if (files.FormFile == null || files.FormFile.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            DataBase dataBase = new DataBase();
            long size = files.FormFile.Sum(f => f.Length);
            int count = 0;

            foreach (var file in files.FormFile)
            {
                if (file.Length > 0)
                {
                    // 假设使用 FileName 作为文件夹名称
                    var folderPath = Path.Combine(@"C:\Users\92701\OneDrive\桌面\musicData", files.FileName);
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // 使用文件的原始名称保存到指定文件夹
                    var filePath = Path.Combine(folderPath, file.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // 插入文件信息到数据库
                    Guid fileId = Guid.NewGuid(); // 生成新的 FileID
                    string sql = $"INSERT INTO Music (musicID, musicName, Type, albumID, Path) VALUES ('{fileId}', '{file.FileName}', '{file.FileName}', '{files.AlbumID}', '{filePath}')";
                    dataBase.ExecuteSqlCommand(sql);

                    count++;
                }
            }

            // 返回上传的文件计数和总大小
            return Ok(new { count, size });

        }




    }
}
