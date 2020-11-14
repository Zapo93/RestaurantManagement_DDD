using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Domain.Models
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
