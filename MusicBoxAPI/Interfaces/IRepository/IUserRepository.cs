using MusicBoxAPI.Entity;

namespace MusicBoxAPI.Interfaces.IRepository
{
    public interface IUserRepository
    {

        UserData GetUserByUsername(string username);
        public string RegisterUser(UserData userdata);
    }
}
