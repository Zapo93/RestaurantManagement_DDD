using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManagement.Common.Application.Contracts;
using RestaurantManagement.Serving.Application.Commands.Common;
using RestaurantManagement.Serving.Application.Commands.CreateDish;
using RestaurantManagement.Serving.Application.Commands.CreateOrder;
using RestaurantManagement.Serving.Application.Queries.GetDishes;
using RestaurantManagement.Serving.Application.Queries.GetOrders;
using RestaurantManagement.Serving.Domain.Exceptions;
using RestaurantManagement.Serving.Domain.Models;
using RestaurantManagement.Tests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantManagement.Serving.Domain;
using RestaurantManagement.Serving.Application.Configuration;
using RestaurantManagement.Serving.Infrastructure.Configuration;

namespace RestaurantManagement.Tests
{
    [TestClass]
    public class ServingUseCasesTests
    {
        [TestMethod]
        public async Task CreateDish_NewDish_SuccessfullRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServingDomain()
                .AddServingApplication()
                .AddServingInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createDishCommand = new CreateDishCommand();
            createDishCommand.Description = "Vkusno";
            createDishCommand.Name = "Vkusna Mandja";
            createDishCommand.RecipeId = 1;
            createDishCommand.Price = new Money(10);
            var createDishCommandOutput = await Mediator.Send(createDishCommand);

            var getDishesQuery = new DishesQuery();
            var dbDish = (await Mediator.Send(getDishesQuery)).Dishes.FirstOrDefault(dish => dish.Id == createDishCommandOutput.DishId);

            Assert.AreEqual(dbDish.Id, createDishCommandOutput.DishId);
            Assert.AreEqual(dbDish.Description, createDishCommand.Description);
            Assert.AreEqual(dbDish.Name, createDishCommand.Name);
            Assert.AreEqual(dbDish.Price, createDishCommand.Price);
            Assert.AreEqual(dbDish.RecipeId, createDishCommand.RecipeId);
        }

        [TestMethod]
        public async Task CreateDish_RecipeAlreadyAdded_ExceptionThrown()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServingDomain()
                .AddServingApplication()
                .AddServingInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createDishCommand = new CreateDishCommand();
            createDishCommand.Description = "Vkusno";
            createDishCommand.Name = "Mnogo Vkusna Mandja";
            createDishCommand.RecipeId = 2;
            createDishCommand.Price = new Money(10);
            await Mediator.Send(createDishCommand);

            createDishCommand = new CreateDishCommand();
            createDishCommand.Description = "Vkusno";
            createDishCommand.Name = "Oshte Po Vkusna Mandja";
            createDishCommand.RecipeId = 2;
            createDishCommand.Price = new Money(10);
            await Assert.ThrowsExceptionAsync<InvalidDishException>(() => Mediator.Send(createDishCommand));
        }

        [TestMethod]
        public async Task CreateOrder_NewOrder_SuccessfullRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServingDomain()
                .AddServingApplication()
                .AddServingInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T")
                .AddTransient<ICurrentUser, CurrentUserServiceMock>();
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createDishCommand = new CreateDishCommand();
            createDishCommand.Description = "Vkusno";
            createDishCommand.Name = "Mnogo Vkusna Mandja";
            createDishCommand.RecipeId = 3;
            createDishCommand.Price = new Money(10);
            await Mediator.Send(createDishCommand);

            createDishCommand = new CreateDishCommand();
            createDishCommand.Description = "Vkusno";
            createDishCommand.Name = "Oshte Po Vkusna Mandja";
            createDishCommand.RecipeId = 4;
            createDishCommand.Price = new Money(10);
            await Mediator.Send(createDishCommand);

            var createOrderCommand = new CreateOrderCommand();
            createOrderCommand.TableId = 5;
            createOrderCommand.AssigneeId = "Goshko";
            createOrderCommand.Items.Add(new OrderItemInputModel(1, "Bez Kurkuma"));
            createOrderCommand.Items.Add(new OrderItemInputModel(2));
            var createOrderCommandOutput = await Mediator.Send(createOrderCommand);

            var getOrdersQuery = new OrdersQuery();
            var dbOrder = (await Mediator.Send(getOrdersQuery)).Orders.FirstOrDefault(order => order.Id == createOrderCommandOutput.OrderId);

            Assert.AreEqual(dbOrder.Id, createOrderCommandOutput.OrderId);
            Assert.AreEqual(dbOrder.TableId, createOrderCommand.TableId);
            Assert.AreEqual(dbOrder.AssigneeId, createOrderCommand.AssigneeId);
            foreach (var item in dbOrder.Items)
            {
                var commandItem = createOrderCommand.Items.FirstOrDefault(commandItem => commandItem.DishId == item.Dish.Id);
                Assert.AreEqual(item.Dish.Id, commandItem.DishId);
                Assert.AreEqual(item.Note, commandItem.Note);
            }
        }

        [TestMethod]
        public async Task CreateOrder_NonExistingDish_ExceptionThrown()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServingDomain()
                .AddServingApplication()
                .AddServingInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T")
                .AddTransient<ICurrentUser, CurrentUserServiceMock>(); ;
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createOrderCommand = new CreateOrderCommand();
            createOrderCommand.TableId = 5;
            createOrderCommand.AssigneeId = "Goshko";
            createOrderCommand.Items.Add(new OrderItemInputModel(-1, "Bez Kurkuma"));

            await Assert.ThrowsExceptionAsync<InvalidDishException>(() => Mediator.Send(createOrderCommand));
        }

        [TestMethod]
        public async Task GetOrders_AllOrders_SuccessfulRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddServingDomain()
                .AddServingApplication()
                .AddServingInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var getOrdersQuery = new OrdersQuery();
            var dbOrders = await Mediator.Send(getOrdersQuery);

            foreach (var order in dbOrders.Orders)
            {
                Assert.IsNotNull(order.Items);
                foreach (var item in order.Items) 
                {
                    Assert.IsNotNull(item.Dish);
                }
            }
        }
    }
}
