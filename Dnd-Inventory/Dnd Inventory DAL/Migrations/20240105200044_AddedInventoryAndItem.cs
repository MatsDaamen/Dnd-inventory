using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Dnd_Inventory_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedInventoryAndItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "inventories",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventories", x => new { x.ItemId, x.SessionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_inventories_SessionUsers_SessionId_UserId",
                        columns: x => new { x.SessionId, x.UserId },
                        principalTable: "SessionUsers",
                        principalColumns: new[] { "SessionId", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: false),
                    Weight = table.Column<float>(type: "float", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    SessionId = table.Column<int>(type: "int", nullable: true),
                    InventoryItemId = table.Column<int>(type: "int", nullable: true),
                    InventorySessionId = table.Column<int>(type: "int", nullable: true),
                    InventoryUserId = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_items_inventories_InventoryItemId_InventorySessionId_Invento~",
                        columns: x => new { x.InventoryItemId, x.InventorySessionId, x.InventoryUserId },
                        principalTable: "inventories",
                        principalColumns: new[] { "ItemId", "SessionId", "UserId" });
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_inventories_SessionId_UserId",
                table: "inventories",
                columns: new[] { "SessionId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_items_InventoryItemId_InventorySessionId_InventoryUserId",
                table: "items",
                columns: new[] { "InventoryItemId", "InventorySessionId", "InventoryUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_inventories_items_ItemId",
                table: "inventories",
                column: "ItemId",
                principalTable: "items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventories_items_ItemId",
                table: "inventories");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "inventories");
        }
    }
}
