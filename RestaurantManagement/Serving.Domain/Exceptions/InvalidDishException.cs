﻿using RestaurantManagement.Common.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Serving.Domain.Exceptions
{
    public class InvalidDishException: BaseDomainException
    {
        public InvalidDishException(string error) 
        {
            Error = error;
        }
    }
}
