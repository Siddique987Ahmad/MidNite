using Microsoft.EntityFrameworkCore;
using MidNite.Core.Events;
using MidNite.Core.RegisterUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MidNite.EntityFramework.Data
{
    public class MidNiteAPIDbContext : DbContext
    {
        public MidNiteAPIDbContext(DbContextOptions<MidNiteAPIDbContext> options)
        : base(options)
        {

        }
        public DbSet<RegisterUser> RegisterUsers { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
