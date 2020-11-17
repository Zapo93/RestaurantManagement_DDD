using RestaurantManagement.Identity.Application.Commands;
using RestaurantManagement.Identity.Application.Commands.LoginUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Identity.Application
{
    public interface IIdentity
    {
        Task<IUser> Register(UserInputModel userInput);

        Task<LoginOutputModel> Login(UserInputModel userInput);
    }
}
