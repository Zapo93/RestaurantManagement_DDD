using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Identity.Application.Exceptions
{
    //TODO Move some things from application to domain
    public class UserCreationFailedException:BaseDomainException
    {
        public UserCreationFailedException(string error) 
        {
            this.Error = error;
        }
    }
}
