using Microsoft.AspNetCore.Http;
using Prototype.Shared.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Prototype.Infra.Data.Interfaces
{
    public class UserNet : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public UserNet(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;


        public bool IsAuthenticated()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _accessor.HttpContext.User.Claims;
        }

       

    }
}
