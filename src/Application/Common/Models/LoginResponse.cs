namespace Application.Common.Models
{
    public class LoginResponse
    {
        public int User_Id { get; set; }
        public Token Access_Token { get; set; }
        public Token Refresh_Token { get; set; }
    }
}
