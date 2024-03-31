using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jumia.Context.Migrations
{
    /// <inheritdoc />
    public partial class Ordership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressAr",
                table: "Shippments");

            migrationBuilder.DropColumn(
                name: "AdressInformationAr",
                table: "Shippments");

            migrationBuilder.DropColumn(
                name: "FirstNameAr",
                table: "Shippments");

            migrationBuilder.DropColumn(
                name: "LastNameAr",
                table: "Shippments");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Order",
                newName: "TotalOrderPrice");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "Specifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "Specifications");

            migrationBuilder.RenameColumn(
                name: "TotalOrderPrice",
                table: "Order",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<string>(
                name: "AddressAr",
                table: "Shippments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AdressInformationAr",
                table: "Shippments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstNameAr",
                table: "Shippments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastNameAr",
                table: "Shippments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
