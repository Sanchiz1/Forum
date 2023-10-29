using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Forum.Models.Identity
{
    public class Credentials
    {
        public string LoginOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
