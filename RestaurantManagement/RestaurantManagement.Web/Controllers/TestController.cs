using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using RestaurantManagement.Application.Kitchen.Commands.CreateRecipe;
using RestaurantManagement.Domain.Kitchen.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IMediator? mediator;
        protected IMediator Mediator
            => this.mediator ??= this.HttpContext
                .RequestServices
                .GetService<IMediator>();

        // POST api/<TestController>
        [HttpPost]
        public async Task<ActionResult<CreateRecipeOutputModel>> Post([FromBody] string value)
        {
            var command = new CreateRecipeCommand();
            command.Name = "Mandja";
            command.Description = "Vkusno";
            command.Preparation = "Gotvish";
            command.Ingredients.Add(new Ingredient("Hranitelni Produkti", 500));
            return await Send(command);
        }


        protected async Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
        { 
            return await this.Mediator.Send(request);
        }

    }
}
