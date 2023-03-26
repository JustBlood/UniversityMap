using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UniversityMap.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return SignIn(new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                        new Claim("my_role", "admin")
                    },
                    "cookie",
                    nameType: null,
                    roleType: "my_role"
                    )
                ),
                authenticationScheme: "cookie"
            );
        }
    }
}
