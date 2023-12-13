using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sips.Migrations
{
    /// <inheritdoc />
    public partial class initialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddIn",
                columns: table => new
                {
                    pkAddInID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    addInName = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    priceModifier = table.Column<decimal>(type: "DECIMAL(10, 2)", nullable: false),
                    urlString = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddIn", x => x.pkAddInID);
                });

            migrationBuilder.CreateTable(
                name: "Credential",
                columns: table => new
                {
                    pkUserTypeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Credential", x => x.pkUserTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    pkItemID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    description = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    ice = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    sweetness = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    basePrice = table.Column<decimal>(type: "DECIMAL(10, 2)", nullable: false),
                    inventory = table.Column<int>(type: "INTEGER", nullable: false),
                    itemType = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    urlString = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.pkItemID);
                });

            migrationBuilder.CreateTable(
                name: "ItemSize",
                columns: table => new
                {
                    pkSizeID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    sizeName = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    priceModifier = table.Column<decimal>(type: "DECIMAL(10, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSize", x => x.pkSizeID);
                });

            migrationBuilder.CreateTable(
                name: "OrderStatus",
                columns: table => new
                {
                    pkStatusID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    isOrdered = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    isPickedUp = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStatus", x => x.pkStatusID);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    pkStoreID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    storeHours = table.Column<string>(type: "VARCHAR(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.pkStoreID);
                });

            migrationBuilder.CreateTable(
                name: "Contact",
                columns: table => new
                {
                    pkUserID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    lastName = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    phoneNumber = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    unit = table.Column<int>(type: "INTEGER", nullable: false),
                    street = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    city = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    province = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    postalCode = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    birthDate = table.Column<DateTime>(type: "DATETIME", nullable: true),
                    isDrinkRedeemed = table.Column<string>(type: "VARCHAR(10)", nullable: false),
                    fkUserTypeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contact", x => x.pkUserID);
                    table.ForeignKey(
                        name: "FK_Contact_Credential_fkUserTypeID",
                        column: x => x.fkUserTypeID,
                        principalTable: "Credential",
                        principalColumn: "pkUserTypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    pkRatingID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    rating = table.Column<string>(type: "VARCHAR(5)", nullable: false),
                    date = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    comment = table.Column<string>(type: "VARCHAR(255)", nullable: false),
                    fkStoreID = table.Column<int>(type: "INTEGER", nullable: false),
                    fkUserID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.pkRatingID);
                    table.ForeignKey(
                        name: "FK_Rating_Contact_fkUserID",
                        column: x => x.fkUserID,
                        principalTable: "Contact",
                        principalColumn: "pkUserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_Store_fkStoreID",
                        column: x => x.fkStoreID,
                        principalTable: "Store",
                        principalColumn: "pkStoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    pkTransactionID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    dateOrdered = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    fkStoreID = table.Column<int>(type: "INTEGER", nullable: false),
                    fkUserID = table.Column<int>(type: "INTEGER", nullable: false),
                    fkStatusID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.pkTransactionID);
                    table.ForeignKey(
                        name: "FK_Transaction_Contact_fkUserID",
                        column: x => x.fkUserID,
                        principalTable: "Contact",
                        principalColumn: "pkUserID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_OrderStatus_fkStatusID",
                        column: x => x.fkStatusID,
                        principalTable: "OrderStatus",
                        principalColumn: "pkStatusID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_Store_fkStoreID",
                        column: x => x.fkStoreID,
                        principalTable: "Store",
                        principalColumn: "pkStoreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    pkOrderDetailID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    price = table.Column<decimal>(type: "DECIMAL(10, 2)", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    isBirthdayDrink = table.Column<string>(type: "VARCHAR(10, 2)", nullable: false),
                    promoValue = table.Column<decimal>(type: "DECIMAL(10, 2)", nullable: false),
                    fkItemID = table.Column<int>(type: "INTEGER", nullable: false),
                    fkTransactionID = table.Column<int>(type: "INTEGER", nullable: false),
                    fkSizeID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.pkOrderDetailID);
                    table.ForeignKey(
                        name: "FK_OrderDetail_ItemSize_fkSizeID",
                        column: x => x.fkSizeID,
                        principalTable: "ItemSize",
                        principalColumn: "pkSizeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Item_fkItemID",
                        column: x => x.fkItemID,
                        principalTable: "Item",
                        principalColumn: "pkItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Transaction_fkTransactionID",
                        column: x => x.fkTransactionID,
                        principalTable: "Transaction",
                        principalColumn: "pkTransactionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AddIn_OrderDetail",
                columns: table => new
                {
                    fkAddInID = table.Column<int>(type: "INTEGER", nullable: false),
                    fkOrderDetailID = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddIn_OrderDetail", x => new { x.fkAddInID, x.fkOrderDetailID });
                    table.ForeignKey(
                        name: "FK_AddIn_OrderDetail_AddIn_fkAddInID",
                        column: x => x.fkAddInID,
                        principalTable: "AddIn",
                        principalColumn: "pkAddInID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AddIn_OrderDetail_OrderDetail_fkOrderDetailID",
                        column: x => x.fkOrderDetailID,
                        principalTable: "OrderDetail",
                        principalColumn: "pkOrderDetailID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddIn_OrderDetail_fkOrderDetailID",
                table: "AddIn_OrderDetail",
                column: "fkOrderDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Contact_email",
                table: "Contact",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contact_fkUserTypeID",
                table: "Contact",
                column: "fkUserTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_name",
                table: "Item",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_fkItemID",
                table: "OrderDetail",
                column: "fkItemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_fkSizeID",
                table: "OrderDetail",
                column: "fkSizeID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_fkTransactionID",
                table: "OrderDetail",
                column: "fkTransactionID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_fkStoreID",
                table: "Rating",
                column: "fkStoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_fkUserID",
                table: "Rating",
                column: "fkUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_fkStatusID",
                table: "Transaction",
                column: "fkStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_fkStoreID",
                table: "Transaction",
                column: "fkStoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_fkUserID",
                table: "Transaction",
                column: "fkUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddIn_OrderDetail");

            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "AddIn");

            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropTable(
                name: "ItemSize");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "Contact");

            migrationBuilder.DropTable(
                name: "OrderStatus");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "Credential");
        }
    }
}
