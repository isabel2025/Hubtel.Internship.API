using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

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
        
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
	        public ApplicationDbContext CreateDbContext(string[] args)
	        {
		        IConfigurationRoot configuration = new ConfigurationBuilder()
			        .SetBasePath(Directory.GetCurrentDirectory())
			        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json")).Build();
		        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
		        var connectionString = configuration.GetConnectionString("DbConnection");
		        builder.UseNpgsql(connectionString);
		        return new ApplicationDbContext(builder.Options);
	        }
        }
    }
}

