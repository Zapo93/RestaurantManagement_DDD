using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Identity.Application;
using RestaurantManagement.Identity.Application.Commands.CreateUser;
using RestaurantManagement.Identity.Application.Commands.LoginUser;
using RestaurantManagement.Common.Web;
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
