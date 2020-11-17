using MediatR;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Serving.Domain.Models;
using RestaurantManagement.Serving.Domain.Specificaitons.Dishes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Serving.Queries.GetDishes
{
    public class DishesQuery: IRequest<GetDishesOutputModel>
    {
        public bool OnlyActive = false;

        public class DishesQueryHandler : IRequestHandler<DishesQuery, GetDishesOutputModel>
        {
            private readonly IDishRepository DishRepository;

            public DishesQueryHandler(IDishRepository dishRepository) 
            {
                this.DishRepository = dishRepository;
            }

            public async Task<GetDishesOutputModel> Handle(DishesQuery query, CancellationToken cancellationToken)
            {
                Specification<Dish> dishSpec = GetDishesSpecification(query);

                IEnumerable<Dish> dishes = await DishRepository.GetDishes(dishSpec,cancellationToken);
                return new GetDishesOutputModel(dishes);
            }

            private Specification<Dish> GetDishesSpecification(DishesQuery query)
            {
                return new OnlyActiveDishesSpecification(query.OnlyActive);
            }
        }
    }
}
