using MediatR;
using RestaurantManagement.Domain.Hosting.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Hosting.Commands.AddReservation
{
    public class AddReservationCommand: IRequest<AddReservationOutputModel>
    {
        public int TableId = default!;
        public DateTime Start;
        public Guest? Guest;

        public AddReservationCommand() 
        {
            Start = DateTime.UtcNow + TimeSpan.FromMinutes(1);
        }

        public class AddReservationCommandHandler : IRequestHandler<AddReservationCommand, AddReservationOutputModel>
        {
            private readonly ITableRepository TableRepository;

            public AddReservationCommandHandler(ITableRepository tableRepository) 
            {
                this.TableRepository = tableRepository;
            }

            public async Task<AddReservationOutputModel> Handle(AddReservationCommand request, CancellationToken cancellationToken)
            {
                Table table = await TableRepository.GetTableWithScheduleForTime(
                    request.TableId,
                    request.Start,
                    cancellationToken);

                Reservation newRes = table.AddReservation(request.Start, request.Guest);
                await TableRepository.Save(table,cancellationToken);

                return new AddReservationOutputModel(newRes.Id);
            }
        }
    }
}
