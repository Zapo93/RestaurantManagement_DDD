using RestaurantManagement.Application.Common.Contracts;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Kitchen
{
    public interface IRequestRepository : IRepository<Request>
    {
        Task<Request> GetRequestById(int requestId, CancellationToken cancellationToken);
    }
}
