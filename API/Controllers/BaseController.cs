using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Auth;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    public class BaseController : Controller
    {
        protected string? UserId
        {
            get
            {
                if (!Request.Headers.TryGetValue("user", out var user))
                    return null;

                return user.ToString();
            }
        }

        protected bool IsAuthenticated()
        {
            return UserId != null && FakeUsers.Users.ContainsKey(UserId);
        }

        protected bool IsAdmin()
        {

            if (UserId == null)
                return false;

            return FakeUsers.Users.TryGetValue(UserId, out var role)
            && role.Equals("admin", StringComparison.OrdinalIgnoreCase);
            
        }

       
    }
}

