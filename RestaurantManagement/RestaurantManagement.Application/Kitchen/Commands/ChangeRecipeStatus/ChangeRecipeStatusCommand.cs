using MediatR;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Kitchen.Commands.ChangeRecipeStatus
{
    public class ChangeRecipeStatusCommand: IRequest<Unit>
    {
        public int RecipeId;
        public bool Active;

        public class ChangeRecipeStatusCommandHandler : IRequestHandler<ChangeRecipeStatusCommand, Unit>
        {
            private readonly IRecipeRepository RecipeRepository;

            public ChangeRecipeStatusCommandHandler(IRecipeRepository recipeRepository) 
            {
                this.RecipeRepository = recipeRepository;
            }

            public async Task<Unit> Handle(ChangeRecipeStatusCommand request, CancellationToken cancellationToken)
            {
                Recipe recipe = await RecipeRepository.GetRecipeById(request.RecipeId,cancellationToken);

                if (request.Active)
                {
                    recipe.Activate();
                }
                else 
                {
                    recipe.Deactivate();
                }

                await RecipeRepository.Save(recipe,cancellationToken);

                return new Unit();
            }
        }
    }
}
