using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace offers.itacademy.ge.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class QuantityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Purchases",
                newName: "OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases",
                newName: "IX_Purchases_OfferId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Offers_OfferId",
                table: "Purchases",
                column: "OfferId",
                principalTable: "Offers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Offers_OfferId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Offers");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "Purchases",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_OfferId",
                table: "Purchases",
                newName: "IX_Purchases_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Products_ProductId",
                table: "Purchases",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
