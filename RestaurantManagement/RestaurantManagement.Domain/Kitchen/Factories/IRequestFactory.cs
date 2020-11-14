using RestaurantManagement.Common.Domain;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Kitchen.Factories
{
    public interface IRequestFactory: IFactory<Request>
    {
        IRequestFactory WithCreatorReferenceId(string creatorReferenceId);
        IRequestFactory WithItem(Recipe recipe, string note);
    }
}
