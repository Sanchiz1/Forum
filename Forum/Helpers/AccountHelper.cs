using System.Security.Claims;

namespace Forum.Helpers
{
    public static class AccountHelper
    {
        public static int GetUserIdFromClaims(ClaimsPrincipal user)
        {
            var id = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "UserId")!.Value);
            return id;
        }
    }
}
