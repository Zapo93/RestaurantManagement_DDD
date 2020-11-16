using MediatR;
using RestaurantManagement.Common.Domain;
using RestaurantManagement.Kitchen.Domain.Models;
using RestaurantManagement.Kitchen.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantManagement.Application.Kitchen.Queries.GetRecipes
{
    public class GetRecipesQuery: IRequest<GetRecipesOutputModel>
    {
        public bool OnlyActive = false;

        public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, GetRecipesOutputModel>
        {
            private readonly IRecipeRepository RecipeRepository;

            public GetRecipesQueryHandler(IRecipeRepository recipeRepository) 
            {
                this.RecipeRepository = recipeRepository;
            }
            public async Task<GetRecipesOutputModel> Handle(GetRecipesQuery query, CancellationToken cancellationToken)
            {
                Specification<Recipe> recipeSpec = GetRecipeSpecification(query);
                IEnumerable<Recipe> recipes = await RecipeRepository.GetRecipes(recipeSpec,cancellationToken);

                return new GetRecipesOutputModel(recipes);
            }

            private Specification<Recipe> GetRecipeSpecification(GetRecipesQuery query) 
            {
                return new OnlyActiveRecipesSpecificaiton(query.OnlyActive);
            }
        }
    }
}
