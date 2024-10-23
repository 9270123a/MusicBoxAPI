namespace MusicBoxAPI.Models
{
    public class MusicFile
    {
        public Guid AlbumID { get; set; }
        
        public string FileName { get; set; } 
        public List<IFormFile> FormFile { get; set; }

    }
}
