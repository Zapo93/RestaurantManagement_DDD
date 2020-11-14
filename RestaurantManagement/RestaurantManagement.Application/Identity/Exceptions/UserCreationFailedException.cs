using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Identity.Exceptions
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
