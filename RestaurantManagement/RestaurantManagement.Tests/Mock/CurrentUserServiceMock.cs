using RestaurantManagement.Common.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Tests.Mock
{
    public class CurrentUserServiceMock : ICurrentUser
    {
        public string UserId => "Goshko";
    }
}
