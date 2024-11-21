using MusicBoxAPI.Models;

namespace MusicBoxAPI.Interfaces.IRepository
{
    public interface IPlayListRepository
    {
        public string PersonalAlbum(string ListName, string Type);
        public string PersonalMusic(PersonalMusic personalMusic);

        public string DeleteAlbum(Guid UserListID);

        public string PersonalMusic(Guid MusicID);
    }
}
