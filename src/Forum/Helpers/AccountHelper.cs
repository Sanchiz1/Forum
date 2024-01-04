using System.Security.Claims;

namespace Forum.Helpers
{
    public static class AccountHelper
    {
        public static int GetUserIdFromClaims(ClaimsPrincipal user)
        {
            var id_claim = user.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (id_claim == null) return 0;
            var id = int.Parse(id_claim.Value);
            return id;
        }
        public static string GetUserRoleFromClaims(ClaimsPrincipal user)
        {
            var role_claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
            if (role_claim == null) return "";
            return role_claim.Value;
        }
    }
}
