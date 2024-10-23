namespace MusicBoxAPI.Entity
{
    public class UserMusicDetailData
    {
        public Guid ID { get; set; }
        public Guid userID { get; set; }

        public string MusicName { get; set; }
        public Guid UserLIstID { get; set; }
        public Guid MusicID { get; set; }

    }
}
