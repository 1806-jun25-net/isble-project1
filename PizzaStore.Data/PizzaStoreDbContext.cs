using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaStore.Data
{
    public partial class PizzaStoreDbContext : DbContext
    {
        public PizzaStoreDbContext()
        {
        }

        public PizzaStoreDbContext(DbContextOptions<PizzaStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PizzaPie> PizzaPie { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.StoreNumber);

                entity.ToTable("Locations", "PizzaStore");

                entity.Property(e => e.Bbqchicken).HasColumnName("BBQChicken");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("Orders", "PizzaStore");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.TimeOfOrder).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.StoreNumberNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_StoreNumber");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_UserID");
            });

            modelBuilder.Entity<PizzaPie>(entity =>
            {
                entity.ToTable("PizzaPie", "PizzaStore");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Bbqchicken).HasColumnName("BBQChicken");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PizzaPie)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaPie_OrderID");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "PizzaStore");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.PrefLocationNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PrefLocation)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_PrefLocation");
            });
        }
    }
}
