using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace offers.itacademy.ge.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NewConfigurationss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Buyers_BuyerId",
                table: "Purchases");

            migrationBuilder.AddColumn<int>(
                name: "BuyerId1",
                table: "Purchases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_BuyerId1",
                table: "Purchases",
                column: "BuyerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Buyers_BuyerId",
                table: "Purchases",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Buyers_BuyerId1",
                table: "Purchases",
                column: "BuyerId1",
                principalTable: "Buyers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Buyers_BuyerId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Buyers_BuyerId1",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_BuyerId1",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "BuyerId1",
                table: "Purchases");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Buyers_BuyerId",
                table: "Purchases",
                column: "BuyerId",
                principalTable: "Buyers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
