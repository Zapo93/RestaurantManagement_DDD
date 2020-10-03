using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Factories
{
    public interface IRequestFactory: IFactory<Request>
    {
        IRequestFactory WithItem(Recipe recipe, string note);
    }
}
