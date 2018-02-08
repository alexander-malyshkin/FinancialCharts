using System;
using FinancialCharts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Layer
{
    public class FinancialChartsContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Option> Option { get; set; }

        public DbSet<UserPreference> UserPreferences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string connString = ConfigHelper.GetConfigValue("FinancialDatabase").ToString();
                string connString = "Server=localhost;Database=FinancialCharts;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connString);
            }
        }

        public FinancialChartsContext()
        {
            
        }
        public FinancialChartsContext(DbContextOptions<FinancialChartsContext> options) : base(options)
        {
            
        }

        
    }
}
