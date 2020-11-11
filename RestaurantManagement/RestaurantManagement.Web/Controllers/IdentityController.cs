using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Identity;
using RestaurantManagement.Application.Identity.Commands.CreateUser;
using RestaurantManagement.Application.Identity.Commands.LoginUser;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Web.Controllers
{
    public class IdentityController: BaseAPIController
    {
        [HttpPost]
        public async Task<ActionResult<IUser>> CreateTable(CreateUserCommand createUserCommand)
        {
            return await Send(createUserCommand);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<LoginOutputModel>> Login(LoginUserCommand loginUserCommand)
        {
            return await Send(loginUserCommand);
        }
    }
}
