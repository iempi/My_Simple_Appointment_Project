using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heka.Domain.Models.Appointment;
using Heka.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Heka.DataAccess.Context
{
    public class HekaDbContext :DbContext
    {
        public HekaDbContext(DbContextOptions<HekaDbContext> options)
            : base(options)
        {

        }


        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Appointment> Appointments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Appointment>().HasQueryFilter(a => !a.IsDelete);
            
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            base.OnModelCreating(modelBuilder);

        }

    }
}
