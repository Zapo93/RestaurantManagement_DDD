using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Hosting.Domain.Models;
using RestaurantManagement.Infrastructure.Common.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Infrastructure.Hosting
{
    internal interface IHostingDbContext: IDbContext
    {
        DbSet<Table> Tables { get; }
        DbSet<Reservation> Reservations { get; }
        DbSet<Schedule> Schedules { get; }
    }
}
