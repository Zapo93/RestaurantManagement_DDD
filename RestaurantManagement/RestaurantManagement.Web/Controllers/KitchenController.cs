using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Kitchen.Commands.ChangeRecipeStatus;
using RestaurantManagement.Application.Kitchen.Commands.CreateRecipe;
using RestaurantManagement.Application.Kitchen.Commands.SetRequestStatus;
using RestaurantManagement.Application.Kitchen.Queries.GetRecipes;
using RestaurantManagement.Application.Kitchen.Queries.GetRequests;
using RestaurantManagement.Common.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Web.Controllers
{
    public class KitchenController: BaseAPIController
    {
        [HttpPost]
        [Route("Recipes")]
        public async Task<ActionResult<CreateRecipeOutputModel>> CreateRecipe(CreateRecipeCommand createRecipeCommand) 
        {
            return await Send(createRecipeCommand);
        }

        [HttpPut]
        [Route("Recipes")]
        public async Task<ActionResult<Unit>> ChangeRecipeStatus(ChangeRecipeStatusCommand changeRecipeStatusCommand)
        {
            return await Send(changeRecipeStatusCommand);
        }

        [HttpGet]
        [Route("Recipes")]
        public async Task<ActionResult<GetRecipesOutputModel>> GetRecipiesQuery([FromQuery] GetRecipesQuery getRecipesQuery)
        {
            return await Send(getRecipesQuery);
        }

        [HttpPut]
        [Route("Requests")]
        public async Task<ActionResult<Unit>> SetRequestStatus(SetRequestStatusCommand setRequestStatusCommand)
        {
            return await Send(setRequestStatusCommand);
        }

        [HttpGet]
        [Route("Requests")]
        public async Task<ActionResult<GetRequestsOutputModel>> GetRequestsQuery([FromQuery] GetRequestsQuery getRequestsQuery)
        {
            return await Send(getRequestsQuery);
        }
    }
}
