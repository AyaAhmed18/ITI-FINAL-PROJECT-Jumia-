using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jumia.Context.Migrations
{
    /// <inheritdoc />
    public partial class editSpec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategorySpecifications_Products_ProductId",
                table: "SubCategorySpecifications");

            migrationBuilder.DropIndex(
                name: "IX_SubCategorySpecifications_ProductId",
                table: "SubCategorySpecifications");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecificationSubCategory_ProductId",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "SubCategorySpecifications");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationSubCategory_ProductId",
                table: "ProductSpecificationSubCategory",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSpecificationSubCategory_ProductId",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "SubCategorySpecifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorySpecifications_ProductId",
                table: "SubCategorySpecifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationSubCategory_ProductId",
                table: "ProductSpecificationSubCategory",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategorySpecifications_Products_ProductId",
                table: "SubCategorySpecifications",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
