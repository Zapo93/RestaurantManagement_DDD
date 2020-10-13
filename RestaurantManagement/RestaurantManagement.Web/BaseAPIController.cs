using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RestaurantManagement.Web
{
    [ApiController]
    [Route("[controller]")]
    public abstract class BaseAPIController: ControllerBase
    {
        private IMediator? mediator;
        protected IMediator Mediator
            => this.mediator ??= this.HttpContext
                .RequestServices
                .GetService<IMediator>();
        
        protected async Task<ActionResult<TResult>> Send<TResult>(IRequest<TResult> request)
        {
            return await this.Mediator.Send(request);
        }

    }
}
