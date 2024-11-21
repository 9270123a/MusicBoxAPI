using MusicBoxAPI.Entity;

namespace MusicBoxAPI.Interfaces.IRepository
{
    public interface IMusicBoxRepository
    {

        public List<MusicListData> GetListID(Guid userID);

        public List<UserMusicDetailData> AllMusicInList(Guid ID);
    }
}
