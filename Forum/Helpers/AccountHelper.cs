using System.Security.Claims;

namespace Forum.Helpers
{
    public static class AccountHelper
    {
        public static int GetUserIdFromClaims(ClaimsPrincipal user)
        {
            var id_calim = user.Claims.FirstOrDefault(c => c.Type == "UserId");
            if (id_calim == null) return 0;
            var id = int.Parse(id_calim.Value);
            return id;
        }
    }
}
