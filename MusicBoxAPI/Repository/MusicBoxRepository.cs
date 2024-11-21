using System.Data;
using Dapper;
using MusicBoxAPI.Entity;
using MusicBoxAPI.Interfaces.IRepository;

namespace MusicBoxAPI.Repository
{
    public class MusicBoxRepository : IMusicBoxRepository
    {

        private readonly IDbConnection _connection;
        private readonly IConfiguration _configuration;

        public MusicBoxRepository(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _configuration = configuration;
        }


        public List<MusicListData> GetListID(Guid userID)
        {
            string query = $"select * from UserMusicDetail where userID = userid = @userID";
            return _connection.Query<MusicListData>(query).ToList();
        }

        public  List<UserMusicDetailData> AllMusicInList(Guid ID)
        {

            string query = "select * from UserMusicDetail where ID = @ID";
            return _connection.Query<UserMusicDetailData>(query).ToList();

        }

    }
}
