namespace MusicBoxAPI.Entity
{
    public class UserData
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }  // 記得要雜湊存儲

    }
}
