using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Application.Serving
{
    public interface IOrderRepository: IRepository<Order>
    {
    }
}
