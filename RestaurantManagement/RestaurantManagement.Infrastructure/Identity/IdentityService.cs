using Microsoft.AspNetCore.Identity;
using RestaurantManagement.Identity.Application;
using RestaurantManagement.Identity.Application.Commands;
using RestaurantManagement.Identity.Application.Commands.LoginUser;
using RestaurantManagement.Identity.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Identity
{
    internal class IdentityService : IIdentity
    {
        private const string InvalidErrorMessage = "Invalid credentials.";

        private readonly UserManager<User> userManager;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public IdentityService(UserManager<User> userManager, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<IUser> Register(UserInputModel userInput)
        {
            var user = new User(userInput.Email);

            var identityResult = await this.userManager.CreateAsync(user, userInput.Password);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? user
                : throw new UserCreationFailedException("User Creation Failed!");
        }

        public async Task<LoginOutputModel> Login(UserInputModel userInput)
        {
            var user = await this.userManager.FindByEmailAsync(userInput.Email);
            if (user == null)
            {
                throw new LoginFailedException("User does not exist!");
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, userInput.Password);
            if (!passwordValid)
            {
                throw new LoginFailedException("Invalid password!");
            }

            var token = this.jwtTokenGenerator.GenerateToken(user);

            return new LoginOutputModel(token, user.Id);
        }
    }
}
