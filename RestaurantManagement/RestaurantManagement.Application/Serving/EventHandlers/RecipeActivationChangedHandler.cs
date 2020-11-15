using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Common.Domain.Events.Kitchen;
using RestaurantManagement.Domain.Serving.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Serving.EventHandlers
{
    public class RecipeActivationChangedHandler: IEventHandler<RecipeActivationChanged>
    {
        private readonly IDishRepository DishRepository;

        public RecipeActivationChangedHandler(IDishRepository dishRepository) 
        {
            this.DishRepository = dishRepository;
        }

        public async Task Handle(RecipeActivationChanged domainEvent)
        {
            Dish targetDish = await DishRepository.GetDishByRecipeId(domainEvent.RecipeId, CancellationToken.None);

            if (targetDish == null) 
            {
                return;
            }
            
            if (domainEvent.Value)
            {
                targetDish.Activate();
            }
            else 
            {
                targetDish.Deactivate();
            }

            await DishRepository.Save(targetDish, CancellationToken.None);
        }
    }
}
