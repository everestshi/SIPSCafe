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

    public virtual DbSet<AddIn> AddIns { get; set; }

    public virtual DbSet<AddInOrderDetail> AddInOrderDetails { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Ice> Ices { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemSize> ItemSizes { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<MilkChoice> MilkChoices { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Sweetness> Sweetnesses { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<PayPalVM> PayPalVMs {  get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:sips.database.windows.net,1433;Initial Catalog=SIPSDatabase;Persist Security Info=False;User ID=SipsAdmin;Password=P@ssw0rd!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddIn>(entity =>
        {
            entity.HasKey(e => e.AddInId).HasName("PK__AddIn__806C395C1C4E15E3");

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
            entity.HasKey(e => new { e.AddInId, e.OrderDetailId }).HasName("PK__AddIn_Or__3E23D4BEB1CCBD78");

            entity.ToTable("AddIn_OrderDetail");

            entity.Property(e => e.AddInId).HasColumnName("addInID");
            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orderDetailID");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.AddIn).WithMany(p => p.AddInOrderDetails)
                .HasForeignKey(d => d.AddInId)
                .HasConstraintName("FK__AddIn_Ord__addIn__61BB7BD9");

            entity.HasOne(d => d.OrderDetail).WithMany(p => p.AddInOrderDetails)
                .HasForeignKey(d => d.OrderDetailId)
                .HasConstraintName("FK__AddIn_Ord__order__62AFA012");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Contact__CB9A1CDF00369C22");

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
            entity.HasKey(e => e.IceId).HasName("PK__Ice__298F0B67AECAB80E");

            entity.ToTable("Ice");

            entity.Property(e => e.IceId).HasColumnName("iceID");
            entity.Property(e => e.IcePercent)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("icePercent");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__56A1284AA8034A7F");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.BasePrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("basePrice");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Inventory).HasColumnName("inventory");
            entity.Property(e => e.ItemTypeId).HasColumnName("itemTypeID");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UrlString)
                .HasColumnType("text")
                .HasColumnName("urlString");

            entity.HasOne(d => d.ItemType).WithMany(p => p.Items)
                .HasForeignKey(d => d.ItemTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Item__itemTypeID__5555A4F4");
        });

        modelBuilder.Entity<ItemSize>(entity =>
        {
            entity.HasKey(e => e.SizeId).HasName("PK__ItemSize__55B1E577A0C33A8A");

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
            entity.HasKey(e => e.ItemTypeId).HasName("PK__ItemType__371A0696DD5E6430");

            entity.ToTable("ItemType");

            entity.Property(e => e.ItemTypeId).HasColumnName("itemTypeID");
            entity.Property(e => e.ItemTypeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("itemTypeName");
        });

        modelBuilder.Entity<MilkChoice>(entity =>
        {
            entity.HasKey(e => e.MilkChoiceId).HasName("PK__MilkChoi__F73C851D7550AC92");

            entity.ToTable("MilkChoice");

            entity.Property(e => e.MilkChoiceId).HasColumnName("milkChoiceID");
            entity.Property(e => e.MilkType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("milkType");
            entity.Property(e => e.PriceModifier)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("priceModifier");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__E4FEDE2A48C4010C");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetailId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("orderDetailID");
            entity.Property(e => e.IceId).HasColumnName("iceID");
            entity.Property(e => e.IsBirthdayDrink).HasColumnName("isBirthdayDrink");
            entity.Property(e => e.ItemId).HasColumnName("itemID");
            entity.Property(e => e.MilkChoiceId).HasColumnName("milkChoiceID");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.PromoValue)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("promoValue");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.SizeId).HasColumnName("sizeID");
            entity.Property(e => e.SweetnessId).HasColumnName("sweetnessID");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("transactionID");

            entity.HasOne(d => d.Ice).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__OrderDeta__iceID__5DEAEAF5");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__OrderDeta__itemI__5A1A5A11");

            entity.HasOne(d => d.MilkChoice).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.MilkChoiceId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__OrderDeta__milkC__5EDF0F2E");

            entity.HasOne(d => d.Size).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK__OrderDeta__sizeI__5C02A283");

            entity.HasOne(d => d.Sweetness).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.SweetnessId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__OrderDeta__sweet__5CF6C6BC");

            entity.HasOne(d => d.Transaction).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.TransactionId)
                .HasConstraintName("FK__OrderDeta__trans__5B0E7E4A");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__OrderSta__36257A3858D3858C");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.StatusId).HasColumnName("statusID");
            entity.Property(e => e.IsOrdered).HasColumnName("isOrdered");
            entity.Property(e => e.IsPickedUp).HasColumnName("isPickedUp");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Rating__2D290D4974602873");

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
                .HasConstraintName("FK__Rating__storeID__51851410");

            entity.HasOne(d => d.User).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Rating__userID__52793849");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__Store__1EA716334554C854");

            entity.ToTable("Store");

            entity.Property(e => e.StoreId).HasColumnName("storeID");
            entity.Property(e => e.StoreHours)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("storeHours");
        });

        modelBuilder.Entity<Sweetness>(entity =>
        {
            entity.HasKey(e => e.SweetnessId).HasName("PK__Sweetnes__84EB147C10D1B1CA");

            entity.ToTable("Sweetness");

            entity.Property(e => e.SweetnessId).HasColumnName("sweetnessID");
            entity.Property(e => e.SweetnessPercent)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("sweetnessPercent");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Transact__9B57CF52E421D853");

            entity.ToTable("Transaction");

            entity.Property(e => e.TransactionId)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("transactionID");
            entity.Property(e => e.DateOrdered)
                .HasColumnType("date")
                .HasColumnName("dateOrdered");
            entity.Property(e => e.StatusId).HasColumnName("statusID");
            entity.Property(e => e.StoreId).HasColumnName("storeID");
            entity.Property(e => e.UserId).HasColumnName("userID");

            entity.HasOne(d => d.Status).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Transacti__statu__4EA8A765");

            entity.HasOne(d => d.Store).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Transacti__store__4CC05EF3");

            entity.HasOne(d => d.User).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Transacti__userI__4DB4832C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
