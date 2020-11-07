using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Identity
{
    public interface IUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
