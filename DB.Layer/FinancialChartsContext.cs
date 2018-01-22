using System;
using FinancialCharts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DB.Layer
{
    public partial class FinancialChartsContext : DbContext
    {
        public virtual DbSet<Asset> Asset { get; set; }
        public virtual DbSet<Option> Option { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //string connString = ConfigHelper.GetConfigValue("DatabaseConnectionString").ToString();
                string connString = "Server=localhost;Database=FinancialCharts;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Option>(entity =>
            {
                entity.ToTable("option");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BaseAssetId).HasColumnName("base_asset_id");

                entity.Property(e => e.ExpDate)
                    .HasColumnName("exp_date")
                    .HasColumnType("datetime2(0)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Strike)
                    .HasColumnName("strike")
                    .HasColumnType("decimal(20, 5)");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.HasOne(d => d.BaseAsset)
                    .WithMany(p => p.Option)
                    .HasForeignKey(d => d.BaseAssetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__option__base_ass__47DBAE45");
            });
        }
    }
}
