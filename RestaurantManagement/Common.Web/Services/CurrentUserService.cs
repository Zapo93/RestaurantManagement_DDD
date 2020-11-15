using Microsoft.AspNetCore.Http;
using RestaurantManagement.Common.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace RestaurantManagement.Common.Web.Services
{
    internal class CurrentUserService : ICurrentUser
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }

            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
