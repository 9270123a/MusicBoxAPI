namespace MusicBoxAPI.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }

        public string FileName { get; set; }
        public Guid NewGuid { get; set; }
    }

}
