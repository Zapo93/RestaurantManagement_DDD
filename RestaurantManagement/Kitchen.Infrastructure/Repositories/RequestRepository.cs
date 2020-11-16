using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Kitchen.Application;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Kitchen.Infrastructure.Repositories
{
    internal class RequestRepository : DataRepository<IKitchenDbContext, Request>,
        IRequestRepository
    {
        public RequestRepository(IKitchenDbContext db) : base(db)
        {
        }

        public Task<Request> GetRequestById(int requestId, CancellationToken cancellationToken)
        {
            return this
                .All()
                .Include("Items.Recipe.Ingredients")
                .FirstOrDefaultAsync(request => request.Id == requestId, cancellationToken);
        }

        public async Task<IEnumerable<Request>> GetRequests(Specification<Request> specification, CancellationToken cancellationToken)
        {
            return await this.Data.Requests
                .Where(specification)
                .Include("Items.Recipe.Ingredients")
                .ToListAsync();
        }
    }
}
