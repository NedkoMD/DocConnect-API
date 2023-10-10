using System.Security.Claims;

namespace DocConnect.Presentation.API.Extensions
{
    public static class ClaimPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal principal) => principal.FindFirstValue(ClaimTypes.Email);
    }
}
