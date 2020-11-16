using RestaurantManagement.Common.Domain;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Kitchen.Domain.Factories
{
    public interface IRequestFactory: IFactory<Request>
    {
        IRequestFactory WithCreatorReferenceId(string creatorReferenceId);
        IRequestFactory WithItem(Recipe recipe, string note);
    }
}
