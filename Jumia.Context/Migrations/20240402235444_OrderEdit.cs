using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jumia.Context.Migrations
{
    /// <inheritdoc />
    public partial class OrderEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippments_AspNetUsers_UserIdentityId",
                table: "Shippments");

            migrationBuilder.DropForeignKey(
                name: "FK_Shippments_Order_OrderId",
                table: "Shippments");

            migrationBuilder.DropIndex(
                name: "IX_Shippments_OrderId",
                table: "Shippments");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Shippments");

            migrationBuilder.AlterColumn<int>(
                name: "UserIdentityId",
                table: "Shippments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippments_AspNetUsers_UserIdentityId",
                table: "Shippments",
                column: "UserIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shippments_AspNetUsers_UserIdentityId",
                table: "Shippments");

            migrationBuilder.AlterColumn<int>(
                name: "UserIdentityId",
                table: "Shippments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Shippments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Shippments_OrderId",
                table: "Shippments",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shippments_AspNetUsers_UserIdentityId",
                table: "Shippments",
                column: "UserIdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shippments_Order_OrderId",
                table: "Shippments",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
