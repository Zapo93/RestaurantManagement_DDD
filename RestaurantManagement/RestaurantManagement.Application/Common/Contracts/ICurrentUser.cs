using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Common.Contracts
{
    public interface ICurrentUser
    {
        string UserId { get; }
    }
}
