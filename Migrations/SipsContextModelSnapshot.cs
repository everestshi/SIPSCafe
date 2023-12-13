﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sips.Models;

#nullable disable

namespace Sips.Migrations
{
    [DbContext(typeof(SipsContext))]
    partial class SipsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.12");

            modelBuilder.Entity("Sips.Models.AddIn", b =>
                {
                    b.Property<int>("PkAddInId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkAddInID");

                    b.Property<string>("AddInName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("addInName");

                    b.Property<decimal>("PriceModifier")
                        .HasColumnType("DECIMAL(10, 2)")
                        .HasColumnName("priceModifier");

                    b.Property<string>("urlString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PkAddInId");

                    b.ToTable("AddIn", (string)null);
                });

            modelBuilder.Entity("Sips.Models.AddInOrderDetail", b =>
                {
                    b.Property<int>("FkAddInId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkAddInID");

                    b.Property<int>("FkOrderDetailId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkOrderDetailID");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("quantity");

                    b.HasKey("FkAddInId", "FkOrderDetailId");

                    b.HasIndex("FkOrderDetailId");

                    b.ToTable("AddIn_OrderDetail", (string)null);
                });

            modelBuilder.Entity("Sips.Models.Contact", b =>
                {
                    b.Property<int>("PkUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkUserID");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("DATETIME")
                        .HasColumnName("birthDate");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("city");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("firstName");

                    b.Property<int>("FkUserTypeId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkUserTypeID");

                    b.Property<string>("IsDrinkRedeemed")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("isDrinkRedeemed");

                    b.Property<string>("LastName")
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("lastName");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("phoneNumber");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("postalCode");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("VARCHAR(20)")
                        .HasColumnName("province");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("VARCHAR(50)")
                        .HasColumnName("street");

                    b.Property<int>("Unit")
                        .HasColumnType("INTEGER")
                        .HasColumnName("unit");

                    b.HasKey("PkUserId");

                    b.HasIndex("FkUserTypeId");

                    b.HasIndex(new[] { "Email" }, "IX_Contact_email")
                        .IsUnique();

                    b.ToTable("Contact", (string)null);
                });

            modelBuilder.Entity("Sips.Models.Credential", b =>
                {
                    b.Property<int>("PkUserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkUserTypeID");

                    b.Property<int>("UserType")
                        .HasColumnType("INTEGER")
                        .HasColumnName("userType");

                    b.HasKey("PkUserTypeId");

                    b.ToTable("Credential", (string)null);
                });

            modelBuilder.Entity("Sips.Models.Item", b =>
                {
                    b.Property<int>("PkItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkItemID");

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("DECIMAL(10, 2)")
                        .HasColumnName("basePrice");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("description");

                    b.Property<string>("Ice")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("ice");

                    b.Property<int>("Inventory")
                        .HasColumnType("INTEGER")
                        .HasColumnName("inventory");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("itemType");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("name");

                    b.Property<string>("Sweetness")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("sweetness");

                    b.Property<string>("urlString")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PkItemId");

                    b.HasIndex(new[] { "Name" }, "IX_Item_name")
                        .IsUnique();

                    b.ToTable("Item", (string)null);
                });

            modelBuilder.Entity("Sips.Models.ItemSize", b =>
                {
                    b.Property<int>("PkSizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkSizeID");

                    b.Property<decimal>("PriceModifier")
                        .HasColumnType("DECIMAL(10, 2)")
                        .HasColumnName("priceModifier");

                    b.Property<string>("SizeName")
                        .IsRequired()
                        .HasColumnType("VARCHAR(30)")
                        .HasColumnName("sizeName");

                    b.HasKey("PkSizeId");

                    b.ToTable("ItemSize", (string)null);
                });

            modelBuilder.Entity("Sips.Models.OrderDetail", b =>
                {
                    b.Property<int>("PkOrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkOrderDetailID");

                    b.Property<int>("FkItemId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkItemID");

                    b.Property<int>("FkSizeId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkSizeID");

                    b.Property<int>("FkTransactionId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkTransactionID");

                    b.Property<string>("IsBirthdayDrink")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10, 2)")
                        .HasColumnName("isBirthdayDrink");

                    b.Property<decimal>("Price")
                        .HasColumnType("DECIMAL(10, 2)")
                        .HasColumnName("price");

                    b.Property<decimal>("PromoValue")
                        .HasColumnType("DECIMAL(10, 2)")
                        .HasColumnName("promoValue");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER")
                        .HasColumnName("quantity");

                    b.HasKey("PkOrderDetailId");

                    b.HasIndex("FkItemId");

                    b.HasIndex("FkSizeId");

                    b.HasIndex("FkTransactionId");

                    b.ToTable("OrderDetail", (string)null);
                });

            modelBuilder.Entity("Sips.Models.OrderStatus", b =>
                {
                    b.Property<int>("PkStatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkStatusID");

                    b.Property<string>("IsOrdered")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("isOrdered");

                    b.Property<string>("IsPickedUp")
                        .IsRequired()
                        .HasColumnType("VARCHAR(10)")
                        .HasColumnName("isPickedUp");

                    b.HasKey("PkStatusId");

                    b.ToTable("OrderStatus", (string)null);
                });

            modelBuilder.Entity("Sips.Models.Rating", b =>
                {
                    b.Property<int>("PkRatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkRatingID");

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("comment");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DATETIME")
                        .HasColumnName("date");

                    b.Property<int>("FkStoreId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkStoreID");

                    b.Property<int>("FkUserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkUserID");

                    b.Property<string>("Rating1")
                        .IsRequired()
                        .HasColumnType("VARCHAR(5)")
                        .HasColumnName("rating");

                    b.HasKey("PkRatingId");

                    b.HasIndex("FkStoreId");

                    b.HasIndex("FkUserId");

                    b.ToTable("Rating", (string)null);
                });

            modelBuilder.Entity("Sips.Models.Store", b =>
                {
                    b.Property<int>("PkStoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkStoreID");

                    b.Property<string>("StoreHours")
                        .IsRequired()
                        .HasColumnType("VARCHAR(255)")
                        .HasColumnName("storeHours");

                    b.HasKey("PkStoreId");

                    b.ToTable("Store", (string)null);
                });

            modelBuilder.Entity("Sips.Models.Transaction", b =>
                {
                    b.Property<int>("PkTransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("pkTransactionID");

                    b.Property<DateTime>("DateOrdered")
                        .HasColumnType("DATETIME")
                        .HasColumnName("dateOrdered");

                    b.Property<int>("FkStatusId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkStatusID");

                    b.Property<int>("FkStoreId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkStoreID");

                    b.Property<int>("FkUserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("fkUserID");

                    b.HasKey("PkTransactionId");

                    b.HasIndex("FkStatusId");

                    b.HasIndex("FkStoreId");

                    b.HasIndex("FkUserId");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("Sips.Models.AddInOrderDetail", b =>
                {
                    b.HasOne("Sips.Models.AddIn", "FkAddIn")
                        .WithMany("AddInOrderDetails")
                        .HasForeignKey("FkAddInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sips.Models.OrderDetail", "FkOrderDetail")
                        .WithMany("AddInOrderDetails")
                        .HasForeignKey("FkOrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FkAddIn");

                    b.Navigation("FkOrderDetail");
                });

            modelBuilder.Entity("Sips.Models.Contact", b =>
                {
                    b.HasOne("Sips.Models.Credential", "FkUserType")
                        .WithMany("Contacts")
                        .HasForeignKey("FkUserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FkUserType");
                });

            modelBuilder.Entity("Sips.Models.OrderDetail", b =>
                {
                    b.HasOne("Sips.Models.Item", "FkItem")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FkItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sips.Models.ItemSize", "FkSize")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FkSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sips.Models.Transaction", "FkTransaction")
                        .WithMany("OrderDetails")
                        .HasForeignKey("FkTransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FkItem");

                    b.Navigation("FkSize");

                    b.Navigation("FkTransaction");
                });

            modelBuilder.Entity("Sips.Models.Rating", b =>
                {
                    b.HasOne("Sips.Models.Store", "FkStore")
                        .WithMany("Ratings")
                        .HasForeignKey("FkStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sips.Models.Contact", "FkUser")
                        .WithMany("Ratings")
                        .HasForeignKey("FkUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FkStore");

                    b.Navigation("FkUser");
                });

            modelBuilder.Entity("Sips.Models.Transaction", b =>
                {
                    b.HasOne("Sips.Models.OrderStatus", "FkStatus")
                        .WithMany("Transactions")
                        .HasForeignKey("FkStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sips.Models.Store", "FkStore")
                        .WithMany("Transactions")
                        .HasForeignKey("FkStoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sips.Models.Contact", "FkUser")
                        .WithMany("Transactions")
                        .HasForeignKey("FkUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FkStatus");

                    b.Navigation("FkStore");

                    b.Navigation("FkUser");
                });

            modelBuilder.Entity("Sips.Models.AddIn", b =>
                {
                    b.Navigation("AddInOrderDetails");
                });

            modelBuilder.Entity("Sips.Models.Contact", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Sips.Models.Credential", b =>
                {
                    b.Navigation("Contacts");
                });

            modelBuilder.Entity("Sips.Models.Item", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Sips.Models.ItemSize", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Sips.Models.OrderDetail", b =>
                {
                    b.Navigation("AddInOrderDetails");
                });

            modelBuilder.Entity("Sips.Models.OrderStatus", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Sips.Models.Store", b =>
                {
                    b.Navigation("Ratings");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Sips.Models.Transaction", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
