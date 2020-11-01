using MediatR;
using RestaurantManagement.Domain.Common;
using RestaurantManagement.Domain.Hosting.Models;
using RestaurantManagement.Domain.Hosting.Services;
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
        public DateTime? FreeTablesTargetTime = null!;
        public int MinimumNumberOfSeats = default!;

        public class GetTablesQueryHandler : IRequestHandler<GetTablesQuery, GetTablesOutputModel>
        {
            private readonly ITableRepository TableRepository;
            private readonly ITablesScheduleService TablesScheduleService;

            public GetTablesQueryHandler(ITableRepository tableRepository, ITablesScheduleService tablesScheduleService)
            {
                this.TableRepository = tableRepository;
                this.TablesScheduleService = tablesScheduleService;
            }
            public async Task<GetTablesOutputModel> Handle(GetTablesQuery request, CancellationToken cancellationToken)
            {
                Specification<Table> tableSpecification = GetTableSpecification(request);

                IReadOnlyCollection<Table> tables = await TableRepository.GetTables(tableSpecification, cancellationToken);

                if (request.FreeTablesTargetTime.HasValue) 
                {
                    tables = TablesScheduleService.GetFreeTablesForTargetTime(tables, request.FreeTablesTargetTime.Value);
                }
                
                return new GetTablesOutputModel(tables);
            }

            private Specification<Table> GetTableSpecification(GetTablesQuery request)
            {
                return new TableMinimumSeatsSpecification(request.MinimumNumberOfSeats);
            }
        }
    }
}
