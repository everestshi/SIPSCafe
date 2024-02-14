using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sips.ViewModels;

namespace Sips.SipsModels;

public partial class SipsdatabaseContext : DbContext
{
    public SipsdatabaseContext()
    {
    }

    public SipsdatabaseContext(DbContextOptions<SipsdatabaseContext> options)
        : base(options)
    {
    }
    //public virtual DbSet<ProductVM> ProductsVM { get; set; }

    public virtual DbSet<AddIn> AddIns { get; set; }

    public virtual DbSet<AddInOrderDetail> AddInOrderDetails { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Ice> Ices { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemSize> ItemSizes { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Sweetness> Sweetnesses { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=tcp:sips.database.windows.net,1433;Initial Catalog=SIPSDatabase;Persist Security Info=False;User ID=SipsAdmin;Password=P@ssw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddIn>(entity =>
        {
            entity.HasKey(e => e.AddInId).HasName("PK__AddIn__806C395C317CC7CB");

            entity.ToTable("AddIn");

            entity.Property(e => e.AddInId).HasColumnName("addInID");
            entity.Property(e => e.AddInName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("addInName");
            entity.Property(e => e.PriceModifier)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("priceModifier");
            entity.Property(e => e.UrlString)
                .HasColumnType("text")
                .HasColumnName("urlString");
        });

        modelBuilder.Entity<AddInOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.AddInId, e.OrderDetailId }).HasName("PK__AddIn_Or__3E23D4BE6111A769");

            entity.ToTable("AddIn_OrderDetail");

            entity.Property(e => e.AddInId).HasColumnName("addInID");
            entity.Property(e => e.OrderDetailId).HasColumnName("orderDetailID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.AddIn).WithMany(p => p.AddInOrderDetails)
                .HasForeignKey(d => d.AddInId)
                .HasConstraintName("FK__AddIn_Ord__addIn__2FCF1A8A");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.AddInOrderDetails)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("FK__AddIn_Ord__order__30C33EC3");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Contact__CB9A1CDF96DAE89C");

            entity.ToTable("Contact");

            entity.Property(e => e.UserId).HasColumnName("userID");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birthDate");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("firstName");
            entity.Property(e => e.IsDrinkRedeemed).HasColumnName("isDrinkRedeemed");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("lastName");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phoneNumber");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("postalCode");
            entity.Property(e => e.Province)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("province");
            entity.Property(e => e.Street)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.Unit).HasColumnName("unit");
        });

        modelBuilder.Entity<Ice>(entity =>
        {
            entity.HasKey(e => e.IceId).HasName("PK__Ice__298F0B67A03B1F34");

            entity.ToTable("Ice");

            entity.Property(e => e.IceId).HasColumnName("iceID");
            entity.Property(e => e.IcePercent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("icePercent");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__56A1284A728B1CBF");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.BasePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("basePrice");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IceId).HasColumnName("iceID");
            entity.Property(e => e.Inventory).HasColumnName("inventory");
            entity.Property(e => e.ItemTypeId).HasColumnName("itemTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SweetnessId).HasColumnName("sweetnessID");
            entity.Property(e => e.UrlString)
                .HasColumnType("text")
                .HasColumnName("urlString");

            entity.HasOne(d => d.Ice).WithMany(p => p.Items)
                .HasForeignKey(d => d.IceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Item__iceID__2739D489");

            entity.HasOne(d => d.ItemType).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Item__itemTypeID__282DF8C2");

            entity.HasOne(d => d.Sweetness).WithMany(p => p.Items)
                .HasForeignKey(d => d.SweetnessId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Item__sweetnessI__2645B050");
        });

        modelBuilder.Entity<ItemSize>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__ItemSize__55B1E57757FC1390");

            entity.ToTable("ItemSize");

            entity.Property(e => e.SizeId).HasColumnName("sizeID");
            entity.Property(e => e.PriceModifier)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("priceModifier");
            entity.Property(e => e.SizeName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("sizeName");
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.HasKey(e => e.ItemTypeId).HasName("PK__ItemType__371A06964C8B8364");

            entity.ToTable("ItemType");

            entity.Property(e => e.ItemTypeId).HasColumnName("itemTypeID");
            entity.Property(e => e.ItemTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("itemTypeName");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__E4FEDE2A9C147AD5");

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

            entity.HasOne(d => d.Item).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__OrderDeta__itemI__2B0A656D");

            entity.HasOne(d => d.Size).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__OrderDeta__sizeI__2CF2ADDF");

            entity.HasOne(d => d.Transaction).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__OrderDeta__trans__2BFE89A6");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__OrderSta__36257A38A3079A28");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.StatusId).HasColumnName("statusID");
            entity.Property(e => e.IsOrdered).HasColumnName("isOrdered");
            entity.Property(e => e.IsPickedUp).HasColumnName("isPickedUp");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Rating__2D290D49754F98A6");

            entity.ToTable("Rating");

            entity.Property(e => e.RatingId).HasColumnName("ratingID");
            entity.Property(e => e.Comment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comment");
            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
            entity.Property(e => e.Rating1)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("rating");
            entity.Property(e => e.StoreId).HasColumnName("storeID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Store).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Rating__storeID__22751F6C");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Rating__userID__236943A5");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__1EA7163327852FA3");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId).HasColumnName("storeID");
            entity.Property(e => e.StoreHours)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("storeHours");
        });

        modelBuilder.Entity<Sweetness>(entity =>
        {
            entity.HasKey(e => e.SweetnessId).HasName("PK__Sweetnes__84EB147C6E99A599");

            entity.ToTable("Sweetness");

            entity.Property(e => e.SweetnessId).HasColumnName("sweetnessID");
            entity.Property(e => e.SweetnessPercent)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("sweetnessPercent");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__9B57CF5207C42415");

            entity.ToTable("Transaction");

            entity.Property(e => e.TransactionId).HasColumnName("transactionID");
            entity.Property(e => e.DateOrdered)
                .HasColumnType("date")
                .HasColumnName("dateOrdered");
            entity.Property(e => e.IsOrdered).HasColumnName("isOrdered");
            entity.Property(e => e.IsPickedUp).HasColumnName("isPickedUp");
            entity.Property(e => e.StatusId).HasColumnName("statusID");
            entity.Property(e => e.StoreId).HasColumnName("storeID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Status).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Transacti__statu__1F98B2C1");

            entity.HasOne(d => d.Store).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Transacti__store__1DB06A4F");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Transacti__userI__1EA48E88");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
