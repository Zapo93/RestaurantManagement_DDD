using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Common.Models
{
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> Events { get; }

        void ClearEvents();
    }
}
