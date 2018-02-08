using System;
using FinancialCharts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Layer
{
    public partial class FinancialChartsContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<Option> Option { get; set; }

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
