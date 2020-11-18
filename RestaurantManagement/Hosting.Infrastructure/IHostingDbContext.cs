using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Common.Infrastructure.Persistence;
using RestaurantManagement.Hosting.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantManagement.Hosting.Infrastructure
{
    internal interface IHostingDbContext: IDbContext
    {
        DbSet<Table> Tables { get; }
        DbSet<Reservation> Reservations { get; }
        DbSet<Schedule> Schedules { get; }
    }
}
