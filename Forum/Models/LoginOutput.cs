namespace Forum.Models
{
    public class LoginOutput
    {
        public int user_id { get; set; }
        public Token access_token { get; set; } = null!;
        public Token refresh_token { get; set; } = null!;
    }
}
