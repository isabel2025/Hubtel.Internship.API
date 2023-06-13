using System;
using Microsoft.EntityFrameworkCore;

namespace Hubtel.Internship.Api.Models
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Tasks> Tasks { get; set; }



        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
	    {
			
	    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tasks>().Property(e => e.Id).UseIdentityAlwaysColumn();
        }
    }
}

