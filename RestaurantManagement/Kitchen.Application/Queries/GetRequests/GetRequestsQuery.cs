using MediatR;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Kitchen.Domain.Models;
using RestaurantManagement.Kitchen.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Kitchen.Application.Queries.GetRequests
{
    public class GetRequestsQuery: IRequest<GetRequestsOutputModel>
    {
        public int? RequestId = default!;
        public bool OnlyAvailable = false;

        public class GetRequestsQueryHandler : IRequestHandler<GetRequestsQuery, GetRequestsOutputModel>
        {
            private readonly IRequestRepository RequestRepository;

            public GetRequestsQueryHandler(IRequestRepository requestRepository)
            {
                this.RequestRepository = requestRepository;
            }
            public async Task<GetRequestsOutputModel> Handle(GetRequestsQuery query, CancellationToken cancellationToken)
            {
                Specification<Request> specification = GetRequestSpecification(query);
                IEnumerable<Request> requests = await RequestRepository.GetRequests(specification, cancellationToken);

                return new GetRequestsOutputModel(requests);
            }

            private Specification<Request> GetRequestSpecification(GetRequestsQuery query) 
            {
                return new OnlyOpenRequestsSpecification(query.OnlyAvailable)
                    .And(new RequestByIdSpecification(query.RequestId)); ;
            }
        }
    }
}
