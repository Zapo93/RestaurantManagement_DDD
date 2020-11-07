using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Identity.Commands.LoginUser
{
    public class LoginOutputModel
    {
        public LoginOutputModel(string token,string userId)
        {
            this.UserId = userId;
            this.Token = token;
        }

        public string UserId { get; }
        public string Token { get; }
    }
}
