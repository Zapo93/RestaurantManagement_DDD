using MediatR;
using RestaurantManagement.Hosting.Domain.Factories;
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Hosting.Application.Commands.CreateTable
{
    public class CreateTableCommand: IRequest<CreateTableOutputModel>
    {
        public string Name = default!;
        public int NumberOfSeats = default!;
        public string Location = default!;
        public bool SmokingAllowed = default!;
        public bool Indoor = default!;
        public string? RestaurantName = null;

        public class CreateTableCommandHandler : IRequestHandler<CreateTableCommand, CreateTableOutputModel>
        {
            private readonly ITableRepository TableRepository;
            private readonly ITableFactory TableFactory;

            public CreateTableCommandHandler(ITableRepository tableRepository, ITableFactory tableFactory)
            {
                this.TableRepository = tableRepository;
                this.TableFactory = tableFactory;
            }

            public async Task<CreateTableOutputModel> Handle(CreateTableCommand request, CancellationToken cancellationToken)
            {
                TableFactory
                    .WithName(request.Name)
                    .WithNumberOfSeats(request.NumberOfSeats)
                    .WithDescription(request.Location, request.SmokingAllowed, request.Indoor);

                if (request.RestaurantName != null) 
                {
                    TableFactory.WithRestaurantName(request.RestaurantName);
                }

                Table newTable = TableFactory.Build();
                await TableRepository.Save(newTable, cancellationToken);

                return new CreateTableOutputModel(newTable.Id);
            }
        }
    }
}
