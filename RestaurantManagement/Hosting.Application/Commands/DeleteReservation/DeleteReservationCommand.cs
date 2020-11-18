using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Hosting.Application.Commands.DeleteReservation
{
    public class DeleteReservationCommand: IRequest<Unit>
    {
        public int ReservationId { get; set; }

        public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand>
        {
            private readonly ITableRepository TableRepository;

            public DeleteReservationCommandHandler(ITableRepository tableRepository) 
            {
                this.TableRepository = tableRepository;
            }

            public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
            {
                await TableRepository.DeleteReservation(request.ReservationId, cancellationToken);
                return new Unit();
            }
        }
    }
}
