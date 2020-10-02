using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Domain.Common
{
    public interface IFactory<out TEntity>
        where TEntity : IAggregateRoot
    {
        TEntity Build();
    }
}
