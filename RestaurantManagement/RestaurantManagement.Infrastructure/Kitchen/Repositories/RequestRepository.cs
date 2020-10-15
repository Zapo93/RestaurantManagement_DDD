using RestaurantManagement.Application.Kitchen;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Kitchen.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Kitchen.Repositories
{
    internal class RequestRepository : DataRepository<IKitchenDbContext, Request>,
        IRequestRepository
    {
        public RequestRepository(IKitchenDbContext db) : base(db)
        {
        }

        public Task<Request> GetRequestById(int requestId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Request>> GetRequests(Specification<Request> specification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
