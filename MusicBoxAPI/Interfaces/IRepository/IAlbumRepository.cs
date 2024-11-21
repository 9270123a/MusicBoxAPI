using Microsoft.AspNetCore.Mvc;
using MusicBoxAPI.Entity;
using MusicBoxAPI.Models;

namespace MusicBoxAPI.Interfaces.IRepository
{
    public interface IAlbumRepository
    {
        List<AlbumData> GetAllAlbum();
        List<MusicData> AllMusicInAlbum(Guid albumId);
        ResponseModel GenerateAlbum(string AlbumName);
        string DeleteAlbum(Guid ID);
        string DeleteMusicList(Guid ID);
        //(int count, long size, string errorMessage) UploadMusic(MusicFile files);
    }
}
