using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Common
{
    public abstract class BaseDomainException: Exception
    {
        private string? error;

        public string Error 
        {
            get { return error ?? base.Message; }
            set { error = value; }
        }
    }
}
