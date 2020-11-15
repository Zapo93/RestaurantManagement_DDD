using MediatR;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagement.Application.Hosting.Commands.AddReservation;
using RestaurantManagement.Application.Hosting.Commands.CreateTable;
using RestaurantManagement.Application.Hosting.Commands.DeleteReservation;
using RestaurantManagement.Application.Hosting.Queries.Tables;
using RestaurantManagement.Common.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagement.Web.Controllers
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
