using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Identity.Commands.LoginUser
{
    public class LoginUserCommand:UserInputModel, IRequest<LoginOutputModel>
    {
        public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginOutputModel>
        {
            private readonly IIdentity IdentityService;

            public LoginUserCommandHandler(IIdentity identity) 
            {
                IdentityService = identity;
            }
            public Task<LoginOutputModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
            {
                return IdentityService.Login(request); ;
            }
        }
    }
}
