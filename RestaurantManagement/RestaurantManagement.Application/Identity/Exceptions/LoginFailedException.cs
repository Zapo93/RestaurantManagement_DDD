using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Identity.Exceptions
{
    public class LoginFailedException: BaseDomainException
    {
        public LoginFailedException(string error) 
        {
            this.Error = error;
        }
    }
}
