using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace offers.itacademy.ge.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactInfo",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "balacane",
                table: "Buyers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Companies",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Buyers",
                newName: "PhotoUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Companies",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Companies",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Balance",
                table: "Buyers",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Buyers",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Buyers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Buyers");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Companies",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "Buyers",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "ContactInfo",
                table: "Companies",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "balacane",
                table: "Buyers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
