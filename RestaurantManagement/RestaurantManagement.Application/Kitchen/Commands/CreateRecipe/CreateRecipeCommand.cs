using MediatR;
using RestaurantManagement.Domain.Kitchen.Factories;
using RestaurantManagement.Domain.Kitchen.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Kitchen.Commands.CreateRecipe
{
    public class CreateRecipeCommand: IRequest<CreateRecipeOutputModel>
    {
        public string Name = default!;
        public string Preparation = default!;
        public string Description = default!;

        //TODO check if this is initialized properly by the JSON serializer when the constructor is internal
        public List<Ingredient> Ingredients;

        public CreateRecipeCommand() 
        {
            Ingredients = new List<Ingredient>();
        }

        public class CreateRecipeCommandHandler: IRequestHandler<CreateRecipeCommand,CreateRecipeOutputModel>
        {
            private readonly IRecipeRepository RecipeRepository;
            private readonly IRecipeFactory RecipeFactory;

            public CreateRecipeCommandHandler(
                IRecipeRepository recipeRepository, 
                IRecipeFactory recipeFactory) 
            {
                RecipeRepository = recipeRepository;
                RecipeFactory = recipeFactory;
            }

            public async Task<CreateRecipeOutputModel> Handle(CreateRecipeCommand command, CancellationToken cancellationToken) 
            {
                RecipeFactory
                    .WithName(command.Name)
                    .WithPreparation(command.Preparation)
                    .WithDescription(command.Description);

                foreach (Ingredient ingredient in command.Ingredients) 
                {
                    RecipeFactory.WithIngredient(ingredient.Name, ingredient.QuantityInGrams);
                }

                Recipe newRecipe = RecipeFactory.Build();
                await RecipeRepository.Save(newRecipe, cancellationToken);

                return new CreateRecipeOutputModel(newRecipe.Id);
            }
        }
    }
}
