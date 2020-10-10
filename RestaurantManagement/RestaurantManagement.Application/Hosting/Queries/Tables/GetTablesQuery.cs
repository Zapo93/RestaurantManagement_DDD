using MediatR;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Hosting.Models;
using RestaurantManagement.Domain.Hosting.Specifications;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Hosting.Queries.Tables
{
    public class GetTablesQuery: IRequest<GetTablesOutputModel>
    {
        public DateTime FreeTablesTargetTime = default!;
        public int MinimumNumberOfSeats = default!;

        public class GetTablesQueryHandler : IRequestHandler<GetTablesQuery, GetTablesOutputModel>
        {
            private readonly ITableRepository TableRepository;

            public GetTablesQueryHandler(ITableRepository tableRepository) 
            {
                this.TableRepository = tableRepository;
            }
            public async Task<GetTablesOutputModel> Handle(GetTablesQuery request, CancellationToken cancellationToken)
            {
                Specification<Table> tableSpecification = GetTableSpecification(request);

                IReadOnlyCollection<Table> tables = await TableRepository.GetTables(tableSpecification, cancellationToken);

                return new GetTablesOutputModel(tables);
            }

            private Specification<Table> GetTableSpecification(GetTablesQuery request)
            {
                return new TableFreeAtDateTimeSpecification(request.FreeTablesTargetTime)
                    .And(new TableMinimumSeatsSpecification(request.MinimumNumberOfSeats));
            }
        }
    }
}
