using MediatR;
using RestaurantManagement.Common.Domain.Models;
using RestaurantManagement.Kitchen.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Kitchen.Application.Commands.SetRequestStatus
{
    public class SetRequestStatusCommand: IRequest
    {
        public int RequestId;
        public int? NewRequestStatus;
        public List<ItemStatus> ItemStatuses;

        public SetRequestStatusCommand() 
        {
            ItemStatuses = new List<ItemStatus>();
        }

        public class SetRequestStatusHandler: IRequestHandler<SetRequestStatusCommand, Unit>
        {
            private readonly IRequestRepository RequestRepository;

            public SetRequestStatusHandler(IRequestRepository requestRepository) 
            {
                RequestRepository = requestRepository;
            }

            public async Task<Unit> Handle(SetRequestStatusCommand command, CancellationToken cancellationToken) 
            {
                Request request = await RequestRepository.GetRequestById(command.RequestId, cancellationToken);

                foreach (ItemStatus itemStatus in command.ItemStatuses) 
                {
                    request.SetItemStatus(itemStatus.ItemId, Enumeration.FromValue<RequestItemStatus>(itemStatus.NewStatus));
                }

                if (command.NewRequestStatus != null) 
                {
                    request.SetStatus(Enumeration.FromValue<RequestStatus>((int)command.NewRequestStatus));
                }

                await RequestRepository.Save(request, cancellationToken);

                return new Unit();
            }
        }
    }
}
