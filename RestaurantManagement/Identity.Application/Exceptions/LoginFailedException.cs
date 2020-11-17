using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Identity.Application.Exceptions
{
    public class LoginFailedException: BaseDomainException
    {
        public LoginFailedException(string error) 
        {
            this.Error = error;
        }
    }
}
