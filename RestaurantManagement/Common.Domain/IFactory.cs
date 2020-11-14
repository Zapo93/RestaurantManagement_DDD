using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Common.Domain
{
    public interface IFactory<out TEntity>
        where TEntity : IAggregateRoot
    {
        TEntity Build();
    }
}
