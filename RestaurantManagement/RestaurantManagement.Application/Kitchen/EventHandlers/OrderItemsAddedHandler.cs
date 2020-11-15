using MediatR;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Application.Kitchen.Commands.CreateRequest;
using RestaurantManagement.Domain.Kitchen.Factories;
using RestaurantManagement.Domain.Serving.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Kitchen.EventHandlers
{
    public class OrderItemsAddedHandler : IEventHandler<OrderItemsAddedEvent>
    {
        private readonly IRequestHandler<CreateRequestCommand, CreateRequestOutputModel> CreateRequestCommandHandler;

        public OrderItemsAddedHandler(IRequestHandler<CreateRequestCommand, CreateRequestOutputModel> createRequestCommandHandler) 
        {
            this.CreateRequestCommandHandler = createRequestCommandHandler;
        }

        public async Task Handle(OrderItemsAddedEvent itemsAddedEvent)
        {
            CreateRequestCommand createRequestCommand = new CreateRequestCommand();
            createRequestCommand.CreatorReferenceId = itemsAddedEvent.CreatorReferenceId;

            List<RequestItemInputModel> requestInputItems = new List<RequestItemInputModel>();
            foreach (AddedOrderItem orderItem in itemsAddedEvent.Items)
            {
                createRequestCommand.Items.Add(new RequestItemInputModel(orderItem.RecipeId, orderItem.Note));
            }

            await CreateRequestCommandHandler.Handle(createRequestCommand, CancellationToken.None);
        }
    }
}
