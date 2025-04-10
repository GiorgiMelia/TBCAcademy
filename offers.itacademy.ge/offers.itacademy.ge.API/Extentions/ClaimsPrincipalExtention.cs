using System.Security.Claims;

namespace ITAcademy.Offers.API.Extentions
{
    public static class ClaimsPrincipalExtension
    {
        public static int GetBuyerId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirst("BuyerId")!.Value);
        }

        public static int GetCompanyId(this ClaimsPrincipal principal)
        {
            return int.Parse(principal.FindFirst("CompanyId")!.Value);
        }

        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.Name)!.Value;
        }

        public static string GetClientType(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("ClientType")!.Value;
        }

        public static string GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        }
    }
}