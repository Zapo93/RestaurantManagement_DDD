using RestaurantManagement.Application.Kitchen;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Infrastructure.Repositories
{
    public class InMemoryRequestRepository : IRequestRepository
    {
        private static Dictionary<int, Request> RequestDataSet = new Dictionary<int, Request>();

        public async Task<Request> GetRequestById(int requestId, CancellationToken cancellationToken)
        {
            return RequestDataSet[requestId];
        }

        public Task<IEnumerable<Request>> GetRequests(Specification<Request> specification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task Save(Request entity, CancellationToken cancellationToken)
        {
            //TODO this only works with 1 entity because ID is private and cannot be set
            RequestDataSet[entity.Id] = entity;
        }
    }
}
