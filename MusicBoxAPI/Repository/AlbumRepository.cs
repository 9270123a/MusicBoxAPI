using System.Data;
using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;
using System.Globalization;
using MusicBoxAPI.Interfaces.IRepository;
using MusicBoxAPI.Models;
using Dapper;

namespace MusicBoxAPI.Repository
{
    public class AlbumRepository : IAlbumRepository
    {
        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public AlbumRepository(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }

        public List<AlbumData> GetAllAlbum ()
        {
            string query = "select * from Album";
            return _connection.Query<AlbumData>(query).ToList();
        }

        public List<MusicData> AllMusicInAlbum(Guid AlbumID)
        {
            string query = $"select * from Music where AlbumID =  @AlbumId";
            var parameters = new { AlbumId = AlbumID };
            return _connection.Query<MusicData>(query, parameters).ToList();
        }

        public ResponseModel GenerateAlbum(string AlbumName)
        {

            Guid newGuid = Guid.NewGuid(); // 生成新的 GUID
            DateTime currentDate = DateTime.Now;
            string formattedDate = currentDate.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            string sql = $"INSERT INTO Album(albumid, albumname, date) VALUES ('{newGuid}', '{AlbumName}', '{formattedDate}')";
            _connection.Execute(sql);
            ResponseModel responseModel = new ResponseModel();
            responseModel.Message = "成功建立";
            responseModel.NewGuid = newGuid;
            responseModel.FileName = AlbumName;
            return responseModel;

        }

        public string DeleteAlbum(Guid ID)
        {
            string sql = $"DELETE FROM Album WHERE albumID = '{ID}'";
            _connection.Execute(sql);
            return "刪除成功";
        }

        public string DeleteMusicList(Guid ID)
        {
            string sql = $"DELETE FROM Music WHERE musicID = '{ID}'";
            string sql2 = $"DELETE FROM UserMusicDetail WHERE musicID = '{ID}'";
            _connection.Execute(sql2);
            _connection.Execute(sql);
            return "刪除成功";
        }

        //public (int count, long size, string errorMessage) UploadMusic(MusicFile files)
        //{
        //    if (files.FormFile == null || files.FormFile.Count == 0)
        //    {
        //        return (0, 0, "No files uploaded.");
        //    }

        //    var size = files.FormFile.Sum(f => f.Length);
        //    var count = 0;
        //    var baseFolder = _configuration["FileStorage:Path"] ?? @"C:\Users\92701\OneDrive\桌面\musicData";

        //    foreach (var file in files.FormFile)
        //    {
        //        if (file.Length > 0)
        //        {
        //            try
        //            {
        //                var folderPath = Path.Combine(baseFolder, files.FileName);
        //                Directory.CreateDirectory(folderPath);

        //                var filePath = Path.Combine(folderPath, file.FileName);
        //                using (var stream = new FileStream(filePath, FileMode.Create))
        //                {
        //                    file.CopyTo(stream);
        //                }

        //                var fileId = Guid.NewGuid();
        //                const string query = @"
        //                INSERT INTO Music (musicID, musicName, Type, albumID, Path) 
        //                VALUES (@MusicId, @MusicName, @Type, @AlbumId, @Path)";

        //                var parameters = new
        //                {
        //                    MusicId = fileId,
        //                    MusicName = file.FileName,
        //                    Type = file.FileName,
        //                    AlbumId = files.AlbumID,
        //                    Path = filePath
        //                };

        //                _connection.Execute(query, parameters);
        //                count++;
        //            }
        //            catch (Exception ex)
        //            {
        //                return (count, size, ex.Message);
        //            }
        //        }
        //    }

        //    // 返回上传的文件计数和总大小
        //    return (count, size, null); 

        }
    }

