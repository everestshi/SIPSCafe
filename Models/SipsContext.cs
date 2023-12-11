using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sips.Models;

public partial class SipsContext : DbContext
{
    public SipsContext()
    {
    }

    public SipsContext(DbContextOptions<SipsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AddIn> AddIns { get; set; }

    public virtual DbSet<AddInOrderDetail> AddInOrderDetails { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Credential> Credentials { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemSize> ItemSizes { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=.\\wwwroot\\SipsDatabase.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddIn>(entity =>
        {
            entity.HasKey(e => e.PkAddInId);

            entity.ToTable("AddIn");

            entity.Property(e => e.PkAddInId).HasColumnName("pkAddInID");
            entity.Property(e => e.AddInName)
                .HasColumnType("VARCHAR(30)")
                .HasColumnName("addInName");
            entity.Property(e => e.PriceModifier)
                .HasColumnType("DECIMAL(10, 2)")
                .HasColumnName("priceModifier");
        });

        modelBuilder.Entity<AddInOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.FkAddInId, e.FkOrderDetailId });

            entity.ToTable("AddIn_OrderDetail");

            entity.Property(e => e.FkAddInId).HasColumnName("fkAddInID");
            entity.Property(e => e.FkOrderDetailId).HasColumnName("fkOrderDetailID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.FkAddIn).WithMany(p => p.AddInOrderDetails).HasForeignKey(d => d.FkAddInId);

            entity.HasOne(d => d.FkOrderDetail).WithMany(p => p.AddInOrderDetails).HasForeignKey(d => d.FkOrderDetailId);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.PkUserId);

            entity.ToTable("Contact");

            entity.HasIndex(e => e.Email, "IX_Contact_email").IsUnique();

            entity.Property(e => e.PkUserId).HasColumnName("pkUserID");
            entity.Property(e => e.BirthDate)
                .HasColumnType("DATETIME")
                .HasColumnName("birthDate");
            entity.Property(e => e.City)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasColumnType("VARCHAR(30)")
                .HasColumnName("firstName");
            entity.Property(e => e.FkUserTypeId).HasColumnName("fkUserTypeID");
            entity.Property(e => e.IsDrinkRedeemed)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("isDrinkRedeemed");
            entity.Property(e => e.LastName)
                .HasColumnType("VARCHAR(30)")
                .HasColumnName("lastName");
            entity.Property(e => e.PhoneNumber)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("phoneNumber");
            entity.Property(e => e.PostalCode)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("postalCode");
            entity.Property(e => e.Province)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("province");
            entity.Property(e => e.Street)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("street");
            entity.Property(e => e.Unit).HasColumnName("unit");

            entity.HasOne(d => d.FkUserType).WithMany(p => p.Contacts).HasForeignKey(d => d.FkUserTypeId);
        });

        modelBuilder.Entity<Credential>(entity =>
        {
            entity.HasKey(e => e.PkUserTypeId);

            entity.ToTable("Credential");

            entity.Property(e => e.PkUserTypeId).HasColumnName("pkUserTypeID");
            entity.Property(e => e.UserType).HasColumnName("userType");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.PkItemId);

            entity.ToTable("Item");

            entity.HasIndex(e => e.Name, "IX_Item_name").IsUnique();

            entity.Property(e => e.PkItemId).HasColumnName("pkItemID");
            entity.Property(e => e.BasePrice)
                .HasColumnType("DECIMAL(10, 2)")
                .HasColumnName("basePrice");
            entity.Property(e => e.Description)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("description");
            entity.Property(e => e.Ice)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("ice");
            entity.Property(e => e.Inventory).HasColumnName("inventory");
            entity.Property(e => e.ItemType)
                .HasColumnType("VARCHAR(30)")
                .HasColumnName("itemType");
            entity.Property(e => e.Name)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("name");
            entity.Property(e => e.Sweetness)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("sweetness");
        });

        modelBuilder.Entity<ItemSize>(entity =>
        {
            entity.HasKey(e => e.PkSizeId);

            entity.ToTable("ItemSize");

            entity.Property(e => e.PkSizeId).HasColumnName("pkSizeID");
            entity.Property(e => e.PriceModifier)
                .HasColumnType("DECIMAL(10, 2)")
                .HasColumnName("priceModifier");
            entity.Property(e => e.SizeName)
                .HasColumnType("VARCHAR(30)")
                .HasColumnName("sizeName");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.PkOrderDetailId);

            entity.ToTable("OrderDetail");

            entity.Property(e => e.PkOrderDetailId).HasColumnName("pkOrderDetailID");
            entity.Property(e => e.FkItemId).HasColumnName("fkItemID");
            entity.Property(e => e.FkSizeId).HasColumnName("fkSizeID");
            entity.Property(e => e.FkTransactionId).HasColumnName("fkTransactionID");
            entity.Property(e => e.IsBirthdayDrink)
                .HasColumnType("VARCHAR(10, 2)")
                .HasColumnName("isBirthdayDrink");
            entity.Property(e => e.Price)
                .HasColumnType("DECIMAL(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PromoValue)
                .HasColumnType("DECIMAL(10, 2)")
                .HasColumnName("promoValue");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.FkItem).WithMany(p => p.OrderDetails).HasForeignKey(d => d.FkItemId);

            entity.HasOne(d => d.FkSize).WithMany(p => p.OrderDetails).HasForeignKey(d => d.FkSizeId);

            entity.HasOne(d => d.FkTransaction).WithMany(p => p.OrderDetails).HasForeignKey(d => d.FkTransactionId);
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.PkStatusId);

            entity.ToTable("OrderStatus");

            entity.Property(e => e.PkStatusId).HasColumnName("pkStatusID");
            entity.Property(e => e.IsOrdered)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("isOrdered");
            entity.Property(e => e.IsPickedUp)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("isPickedUp");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.PkRatingId);

            entity.ToTable("Rating");

            entity.Property(e => e.PkRatingId).HasColumnName("pkRatingID");
            entity.Property(e => e.Comment)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasColumnType("DATETIME")
                .HasColumnName("date");
            entity.Property(e => e.FkStoreId).HasColumnName("fkStoreID");
            entity.Property(e => e.FkUserId).HasColumnName("fkUserID");
            entity.Property(e => e.Rating1)
                .HasColumnType("VARCHAR(5)")
                .HasColumnName("rating");

            entity.HasOne(d => d.FkStore).WithMany(p => p.Ratings).HasForeignKey(d => d.FkStoreId);

            entity.HasOne(d => d.FkUser).WithMany(p => p.Ratings).HasForeignKey(d => d.FkUserId);
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.PkStoreId);

            entity.ToTable("Store");

            entity.Property(e => e.PkStoreId).HasColumnName("pkStoreID");
            entity.Property(e => e.StoreHours)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("storeHours");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.PkTransactionId);

            entity.ToTable("Transaction");

            entity.Property(e => e.PkTransactionId).HasColumnName("pkTransactionID");
            entity.Property(e => e.DateOrdered)
                .HasColumnType("DATETIME")
                .HasColumnName("dateOrdered");
            entity.Property(e => e.FkStatusId).HasColumnName("fkStatusID");
            entity.Property(e => e.FkStoreId).HasColumnName("fkStoreID");
            entity.Property(e => e.FkUserId).HasColumnName("fkUserID");

            entity.HasOne(d => d.FkStatus).WithMany(p => p.Transactions).HasForeignKey(d => d.FkStatusId);

            entity.HasOne(d => d.FkStore).WithMany(p => p.Transactions).HasForeignKey(d => d.FkStoreId);

            entity.HasOne(d => d.FkUser).WithMany(p => p.Transactions).HasForeignKey(d => d.FkUserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
