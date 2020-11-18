using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantManagement.Hosting.Application;
using RestaurantManagement.Hosting.Application.Commands.AddReservation;
using RestaurantManagement.Hosting.Application.Commands.CreateTable;
using RestaurantManagement.Hosting.Application.Commands.DeleteReservation;
using RestaurantManagement.Hosting.Application.Queries.Tables;
using RestaurantManagement.Hosting.Domain;
using RestaurantManagement.Hosting.Domain.Exceptions;
using RestaurantManagement.Hosting.Domain.Models;
using RestaurantManagement.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Tests
{
    [TestClass]
    public class HostingUseCasesTests
    {
        [TestMethod]
        public async Task CreateTable_NewTable_SuccessfulRead() 
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHostingDomain()
                .AddHostingApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createTableCommand = new CreateTableCommand();
            createTableCommand.Indoor = true;
            createTableCommand.Location = "Do Prozoreca";
            createTableCommand.Name = "Best Table EVER";
            createTableCommand.NumberOfSeats = 4;
            createTableCommand.SmokingAllowed = true;
            createTableCommand.RestaurantName = "Best Restaurant EVER";
            var createTableOutputModel = await Mediator.Send(createTableCommand);

            var getTablesQuery = new GetTablesQuery();
            var tommorow = DateTime.UtcNow.AddDays(1);
            getTablesQuery.FreeTablesTargetTime = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 13, 0, 0);
            var getTablesOutput = await Mediator.Send(getTablesQuery);

            var dbTable = getTablesOutput.Tables.FirstOrDefault(table => table.Id == createTableOutputModel.TableId);

            Assert.AreEqual(dbTable.NumberOfSeats, createTableCommand.NumberOfSeats);
            Assert.AreEqual(dbTable.Name, createTableCommand.Name);
            Assert.AreEqual(dbTable.Description.IsIndoor, createTableCommand.Indoor);
            Assert.AreEqual(dbTable.Description.Location, createTableCommand.Location);
            Assert.AreEqual(dbTable.Description.AreSmokersAllowed, createTableCommand.SmokingAllowed);
        }

        [TestMethod]
        public async Task AddReservation_NewTable_SuccessfulRead()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHostingDomain()
                .AddHostingApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createTableCommand = new CreateTableCommand();
            createTableCommand.Indoor = true;
            createTableCommand.Location = "Do Prozoreca";
            createTableCommand.Name = "Bestest Table EVER";
            createTableCommand.NumberOfSeats = 6;
            createTableCommand.SmokingAllowed = true;
            createTableCommand.RestaurantName = "Best Restaurant EVER";
            var createTableOutputModel = await Mediator.Send(createTableCommand);

            var tommorow = DateTime.UtcNow.AddDays(1);

            var addReservationCommand = new AddReservationCommand();
            addReservationCommand.TableId = createTableOutputModel.TableId;
            addReservationCommand.Start = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 13, 30, 0);
            addReservationCommand.Guest = new Guest("Goshko", "Loshkov", "0900tainamaina");
            var addReservationOutput = await Mediator.Send(addReservationCommand);

            var getTablesQuery = new GetTablesQuery();
            var getTablesOutput = await Mediator.Send(getTablesQuery);

            var dbTable = getTablesOutput.Tables.FirstOrDefault(table => table.Id == createTableOutputModel.TableId);
            var targetSchedule = dbTable.GetScheduleForDateTime(addReservationCommand.Start);
            var targetRes = targetSchedule.Reservations.FirstOrDefault(res => res.Id == addReservationOutput.ReservationId);

            Assert.AreEqual(targetRes.Guest.FirstName, addReservationCommand.Guest.FirstName);
            Assert.AreEqual(targetRes.Guest.LastName, addReservationCommand.Guest.LastName);
            Assert.AreEqual(targetRes.Guest.PhoneNumber, addReservationCommand.Guest.PhoneNumber);
            Assert.AreEqual(targetRes.TimeRange.Start, addReservationCommand.Start);
        }

        [TestMethod]
        public async Task DeleteReservation_NewReservation_SuccessfullyMissing()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHostingDomain()
                .AddHostingApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createTableCommand = new CreateTableCommand();
            createTableCommand.Indoor = true;
            createTableCommand.Location = "Do Prozoreca";
            createTableCommand.Name = "Bestest Table EVER";
            createTableCommand.NumberOfSeats = 6;
            createTableCommand.SmokingAllowed = true;
            createTableCommand.RestaurantName = "Best Restaurant EVER";
            var createTableOutputModel = await Mediator.Send(createTableCommand);

            var tommorow = DateTime.UtcNow.AddDays(1);

            var addReservationCommand = new AddReservationCommand();
            addReservationCommand.TableId = createTableOutputModel.TableId;
            addReservationCommand.Start = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 13, 30, 0);
            addReservationCommand.Guest = new Guest("Goshko", "Loshkov", "0900tainamaina");
            var addReservationOutput = await Mediator.Send(addReservationCommand);

            var deleteReservationCommand = new DeleteReservationCommand();
            deleteReservationCommand.ReservationId = addReservationOutput.ReservationId;
            await Mediator.Send(deleteReservationCommand);

            var getTablesQuery = new GetTablesQuery();
            var getTablesOutput = await Mediator.Send(getTablesQuery);

            var dbTable = getTablesOutput.Tables.FirstOrDefault(table => table.Id == createTableOutputModel.TableId);
            var targetSchedule = dbTable.GetScheduleForDateTime(addReservationCommand.Start);

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() =>
            {
                var targetRes = targetSchedule.Reservations.First(res => res.Id == addReservationOutput.ReservationId);
                return Task.CompletedTask;
            });
        }

        [TestMethod]
        public async Task GetTables()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHostingDomain()
                .AddHostingApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var getTablesQuery = new GetTablesQuery();
            var getTablesOutput = await Mediator.Send(getTablesQuery);
        }

        [TestMethod]
        public async Task AddOverlappingReservation_NewReservation_ThrowsError()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddHostingDomain()
                .AddHostingApplication()
                .AddInfrastructure("Server=.;Database=RestaurantManagementSystem;Trusted_Connection=True;MultipleActiveResultSets=true", "S0M3 M4G1C UN1C0RNS G3N3R4T3D TH1S S3CR3T");
            var serviceProviderFactory = new DefaultServiceProviderFactory();

            IServiceProvider serviceProvider = serviceProviderFactory.CreateServiceProvider(services);

            IMediator Mediator = serviceProvider.GetService<IMediator>();

            var createTableCommand = new CreateTableCommand();
            createTableCommand.Indoor = true;
            createTableCommand.Location = "Do Prozoreca";
            createTableCommand.Name = "Bestest Table EVER";
            createTableCommand.NumberOfSeats = 6;
            createTableCommand.SmokingAllowed = true;
            createTableCommand.RestaurantName = "Best Restaurant EVER";
            var createTableOutputModel = await Mediator.Send(createTableCommand);

            var tommorow = DateTime.UtcNow.AddDays(1);

            var addReservationCommand = new AddReservationCommand();
            addReservationCommand.TableId = createTableOutputModel.TableId;
            addReservationCommand.Start = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 13, 30, 0);
            addReservationCommand.Guest = new Guest("Goshko", "Loshkov", "0900tainamaina");
            var addReservationOutput = await Mediator.Send(addReservationCommand);

            addReservationCommand = new AddReservationCommand();
            addReservationCommand.TableId = createTableOutputModel.TableId;
            addReservationCommand.Start = new DateTime(tommorow.Year, tommorow.Month, tommorow.Day, 13, 30, 0);
            addReservationCommand.Guest = new Guest("Goshko", "Loshkov", "0900tainamaina");
            
            await Assert.ThrowsExceptionAsync<ReservationConflictException>(() =>
            {
                return Mediator.Send(addReservationCommand); 
            });
        }
    }
}
