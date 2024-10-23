namespace MusicBoxAPI.Entity
{
    public class MusicData
    {

        public Guid musicID { get; set; }
        public string musicName { get; set; }
        public string Type { get; set; }

        public Guid AlbumID { get; set; }
        public string Path { get; set; }
    }
}
