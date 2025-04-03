using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace offers.itacademy.ge.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class IsCanceledAddedToOffer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Offers");
        }
    }
}
