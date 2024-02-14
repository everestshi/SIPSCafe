using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sips.SipsModels;

public partial class SipsdatabaseModels : DbContext
{
    public SipsdatabaseModels()
    {
    }

    public SipsdatabaseModels(DbContextOptions<SipsdatabaseModels> options)
        : base(options)
    {
    }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:sips.database.windows.net,1433;Initial Catalog=SIPSDatabase;Persist Security Info=False;User ID=SipsAdmin;Password=P@ssw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__E4FEDE2A3CD9A146");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId).HasColumnName("orderDetailID");
            entity.Property(e => e.IsBirthdayDrink).HasColumnName("isBirthdayDrink");
            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PromoValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("promoValue");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SizeId).HasColumnName("sizeID");
            entity.Property(e => e.TransactionId).HasColumnName("transactionID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
