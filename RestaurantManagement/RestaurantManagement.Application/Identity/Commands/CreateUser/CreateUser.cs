using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Identity.Commands.CreateUser
{
    public class CreateUserCommand: UserInputModel, IRequest<IUser>
    {
        public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IUser>
        {
            private readonly IIdentity IdentityService;

            public CreateUserCommandHandler(IIdentity identity) 
            {
                IdentityService = identity;
            }

            public Task<IUser> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                return IdentityService.Register(request);
            }
        }
    }
}
