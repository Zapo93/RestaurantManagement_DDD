using RestaurantManagement.Application.Identity.Commands;
using RestaurantManagement.Application.Identity.Commands.LoginUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Identity
{
    public interface IIdentity
    {
        Task<IUser> Register(UserInputModel userInput);

        Task<LoginOutputModel> Login(UserInputModel userInput);
    }
}
