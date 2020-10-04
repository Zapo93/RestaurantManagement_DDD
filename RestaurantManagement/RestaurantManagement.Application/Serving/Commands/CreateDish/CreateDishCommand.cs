using RestaurantManagement.Domain.Serving.Factories;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Serving.Commands.CreateDish
{
    public class CreateDishCommand
    {
        public string Name = default!;
        public int RecipeId = default!;
        public string Description = default!;
        public Money Price = null!;
        public Uri ImageUrl = default!;

        public class CreateDishCommandHandler
        {
            private readonly IDishRepository DishRepository;
            private readonly IDishFactory DishFactory;

            public CreateDishCommandHandler(
                IDishRepository dishRepository,
                IDishFactory dishFactory) 
            {
                DishRepository = dishRepository;
                DishFactory = dishFactory;
            }

            public async Task<CreateDishOutputModel> Handle(CreateDishCommand command, CancellationToken cancellationToken) 
            {
                DishFactory
                    .WithName(command.Name)
                    .WithRecipeId(command.RecipeId)
                    .WithDescription(command.Description)
                    .WithPrice(command.Price.Value, command.Price.CurrencyAbbreviation)
                    .WithImage(command.ImageUrl);

                Dish newDish = DishFactory.Build();
                await DishRepository.Save(newDish, cancellationToken);

                return new CreateDishOutputModel(newDish.Id);
            }
        }
    }
}
