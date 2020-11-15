using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Application.Contracts
{
    public interface ICurrentUser
    {
        string UserId { get; }
    }
}
