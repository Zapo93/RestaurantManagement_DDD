using RestaurantManagement.Domain.Kitchen.Factories;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Kitchen.Commands.CreateRequest
{
    public class CreateRequestCommand
    {
        public List<RequestItemInputModel> Items;

        //TODO test if this is initialized correctly by JSON serializer
        public CreateRequestCommand() 
        {
            Items = new List<RequestItemInputModel>();
        }

        public class CreateRequestCommandHandler 
        {
            private readonly IRequestFactory RequestFactory;
            private readonly IRequestRepository RequestRepository;
            private readonly IRecipeRepository RecipeRepository;

            public CreateRequestCommandHandler(
                IRequestFactory requestFactory,
                IRequestRepository requestRepository,
                IRecipeRepository recipeRepository) 
            {
                RequestFactory = requestFactory;
                RequestRepository = requestRepository;
                RecipeRepository = recipeRepository;
            }

            public async Task<CreateRequestOutputModel> Handle(CreateRequestCommand command, CancellationToken cancellationToken) 
            {
                foreach (var item in command.Items) 
                {
                    Recipe recipe = await RecipeRepository.GetRecipeById(item.RecipeId, cancellationToken);
                    RequestFactory.WithItem(recipe, item.Note);
                }

                Request newRequest = RequestFactory.Build();
                await RequestRepository.Save(newRequest, cancellationToken);

                return new CreateRequestOutputModel(newRequest.Id);

            }
        }
    }
}
