using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManagement.Application;
using RestaurantManagement.Application.Kitchen.Commands.CreateRecipe;
using RestaurantManagement.Application.Kitchen.Commands.CreateRequest;
using RestaurantManagement.Application.Kitchen.Commands.SetRequestStatus;
using RestaurantManagement.Application.Kitchen.Queries.GetRecipes;
using RestaurantManagement.Application.Kitchen.Queries.GetRequests;
using RestaurantManagement.Domain;
using RestaurantManagement.Domain.Kitchen.Models;
using RestaurantManagement.Infrastructure;
using RestaurantManagement.Web;
using RestaurantManagement.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantManagement.Tests
{
    [TestClass]
    public class KitchenUseCasesTests
    {
        [TestMethod]
        public async Task CreateRecipe_NewRecipe_SuccessfullRead() 
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDomain()
                .AddApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createRecipeCommand = new CreateRecipeCommand();

            createRecipeCommand.Name = "Mandja";
            createRecipeCommand.Description = "Vkusno";
            createRecipeCommand.Preparation = "Gotvish";

            createRecipeCommand.Ingredients.Add(new Ingredient("Qdene", 500));

            CreateRecipeOutputModel createRecipeOutput;

            createRecipeOutput = await Mediator.Send(createRecipeCommand);

            var recipiesQuery = new GetRecipesQuery();
            recipiesQuery.OnlyActive = true;

            var recipesResult = await Mediator.Send(recipiesQuery);
            var recipe = recipesResult.Recipes.FirstOrDefault(recipe => recipe.Id == createRecipeOutput.RecipeId);

            Assert.AreEqual(createRecipeCommand.Name, recipe.Name);
            Assert.AreEqual(createRecipeCommand.Description, recipe.Description);
            Assert.AreEqual(createRecipeCommand.Preparation, recipe.Preparation);
        }

        [TestMethod]
        public async Task CreateRequest_NewRequest_SuccessfullRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDomain()
                .AddApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createRequestCommand = new CreateRequestCommand();

            createRequestCommand.CreatorReferenceId = "TestId";
            createRequestCommand.Items.Add(new RequestItemInputModel(1,"Bez Chesun"));
            createRequestCommand.Items.Add(new RequestItemInputModel(2, "Bez Chesun"));

            CreateRequestOutputModel createRequestOutput;

            createRequestOutput = await Mediator.Send(createRequestCommand);

            var getRequestsQuery = new GetRequestsQuery();

            var getRequestsResult = await Mediator.Send(getRequestsQuery);
            var request = getRequestsResult.Requests.FirstOrDefault(request => request.Id == createRequestOutput.RequestId);

            Assert.AreEqual(createRequestCommand.CreatorReferenceId, request.CreatorReferenceId);
            foreach (var item in request.Items) 
            {
                var commandItem = createRequestCommand.Items.FirstOrDefault(commandItem => commandItem.RecipeId == item.Recipe.Id);
                Assert.AreEqual(item.Recipe.Id, commandItem.RecipeId);
            }
        }

        [TestMethod]
        public async Task RequestStatusChange_NewRequest_SuccessfullRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDomain()
                .AddApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createRequestCommand = new CreateRequestCommand();
            createRequestCommand.CreatorReferenceId = "sdafsad";
            createRequestCommand.Items.Add(new RequestItemInputModel(1, "Bez Chesun"));
            createRequestCommand.Items.Add(new RequestItemInputModel(2, "Bez Chesun"));
            CreateRequestOutputModel createRequestOutput;
            createRequestOutput = await Mediator.Send(createRequestCommand);

            var getRequestsQuery = new GetRequestsQuery();
            var getRequestsResult = await Mediator.Send(getRequestsQuery);
            var request = getRequestsResult.Requests.FirstOrDefault(request => request.Id == createRequestOutput.RequestId);
            
            var setRequestStatusCommand = new SetRequestStatusCommand();
            setRequestStatusCommand.RequestId = createRequestOutput.RequestId;
            setRequestStatusCommand.NewRequestStatus = RequestStatus.InProgress.Value;
            var newItemStatus = new ItemStatus(request.Items.ElementAt(0).Id, RequestItemStatus.Ready.Value);
            setRequestStatusCommand.ItemStatuses.Add(newItemStatus);
            await Mediator.Send(setRequestStatusCommand);

            getRequestsResult = await Mediator.Send(getRequestsQuery);
            request = getRequestsResult.Requests.FirstOrDefault(request => request.Id == createRequestOutput.RequestId);

            Assert.AreEqual(setRequestStatusCommand.NewRequestStatus.Value, request.Status.Value);
            foreach (var commandItemStatus in setRequestStatusCommand.ItemStatuses)
            {
                var requestItem = request.Items.FirstOrDefault(requestItem => requestItem.Id == commandItemStatus.ItemId);
                Assert.AreEqual(requestItem.Status.Value, commandItemStatus.NewStatus);
            }
        }

        [TestMethod]
        public async Task GetRequestById_NewRequest_SuccessfullRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDomain()
                .AddApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createRequestCommand = new CreateRequestCommand();
            createRequestCommand.CreatorReferenceId = "sdafsad";
            createRequestCommand.Items.Add(new RequestItemInputModel(1, "Bez Chesun"));
            createRequestCommand.Items.Add(new RequestItemInputModel(2, "Bez Chesun"));
            CreateRequestOutputModel createRequestOutput;
            createRequestOutput = await Mediator.Send(createRequestCommand);

            var getRequestsQuery = new GetRequestsQuery();
            getRequestsQuery.RequestId = createRequestOutput.RequestId;
            var getRequestsResult = await Mediator.Send(getRequestsQuery);
            var request = getRequestsResult.Requests.ElementAt(0);

            Assert.AreEqual(createRequestCommand.CreatorReferenceId, request.CreatorReferenceId);
            foreach (var item in request.Items)
            {
                var commandItem = createRequestCommand.Items.FirstOrDefault(commandItem => commandItem.RecipeId == item.Recipe.Id);
                Assert.AreEqual(item.Recipe.Id, commandItem.RecipeId);
            }
        }

        [TestMethod]
        public async Task GetRequest_AllRequests_SuccessfullRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDomain()
                .AddApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var getRequestsQuery = new GetRequestsQuery();
            var getRequestsResult = await Mediator.Send(getRequestsQuery);

            foreach (var request in getRequestsResult.Requests)
            {
                Assert.IsNotNull(request.Items);
                foreach (var item in request.Items) 
                {
                    Assert.IsNotNull(item.Recipe);
                }
            }
        }
    }
}
