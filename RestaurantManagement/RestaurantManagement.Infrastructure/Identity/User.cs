using Microsoft.AspNetCore.Identity;
using RestaurantManagement.Identity.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Identity
{
    public class User: IdentityUser,IUser
    {
        internal User(string email)
            : base(email) 
        {
            this.Email = email;
        }
    }
}
