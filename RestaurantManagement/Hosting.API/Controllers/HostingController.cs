using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Hosting.Application.Commands.AddReservation;
using RestaurantManagement.Hosting.Application.Commands.CreateTable;
using RestaurantManagement.Hosting.Application.Commands.DeleteReservation;
using RestaurantManagement.Hosting.Application.Queries.Tables;
using RestaurantManagement.Common.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Hosting.Web.Controllers
{
    public class HostingController:BaseAPIController
    {
        [HttpPost]
        public async Task<ActionResult<CreateTableOutputModel>> CreateTable(CreateTableCommand createTableCommand)
        {
            return await Send(createTableCommand);
        }

        [HttpPost]
        [Route("Reservations")]
        public async Task<ActionResult<AddReservationOutputModel>> AddReservation(AddReservationCommand addReservationCommand)
        {
            return await Send(addReservationCommand);
        }

        [HttpDelete]
        [Route("Reservations")]
        public async Task<ActionResult<Unit>> DeleteReservation([FromQuery]DeleteReservationCommand deleteReservationCommand)
        {
            return await Send(deleteReservationCommand);
        }

        [HttpGet]
        public async Task<ActionResult<GetTablesOutputModel>> GetTables([FromQuery] GetTablesQuery tablesQuery)
        {
            return await Send(tablesQuery);
        }
    }
}
