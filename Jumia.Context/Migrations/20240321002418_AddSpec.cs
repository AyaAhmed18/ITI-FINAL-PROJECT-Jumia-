using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Jumia.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddSpec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategory_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecificationSubCategory_Specifications_SpecificationId",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecificationSubCategory_SubCategory_SubCategoryId",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropTable(
                name: "SpecificationSubCategory");

            migrationBuilder.DropIndex(
                name: "IX_ProductSpecificationSubCategory_SpecificationId",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropColumn(
                name: "SpecificationId",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "ProductSpecificationSubCategory",
                newName: "SubSpecId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSpecificationSubCategory_SubCategoryId",
                table: "ProductSpecificationSubCategory",
                newName: "IX_ProductSpecificationSubCategory_SubSpecId");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SubCategorySpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false),
                    specificationId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategorySpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategorySpecifications_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategorySpecifications_Specifications_specificationId",
                        column: x => x.specificationId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubCategorySpecifications_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorySpecifications_ProductId",
                table: "SubCategorySpecifications",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorySpecifications_specificationId",
                table: "SubCategorySpecifications",
                column: "specificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategorySpecifications_SubCategoryId",
                table: "SubCategorySpecifications",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategory_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecificationSubCategory_SubCategorySpecifications_SubSpecId",
                table: "ProductSpecificationSubCategory",
                column: "SubSpecId",
                principalTable: "SubCategorySpecifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategory_SubCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSpecificationSubCategory_SubCategorySpecifications_SubSpecId",
                table: "ProductSpecificationSubCategory");

            migrationBuilder.DropTable(
                name: "SubCategorySpecifications");

            migrationBuilder.RenameColumn(
                name: "SubSpecId",
                table: "ProductSpecificationSubCategory",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSpecificationSubCategory_SubSpecId",
                table: "ProductSpecificationSubCategory",
                newName: "IX_ProductSpecificationSubCategory_SubCategoryId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProductSpecificationSubCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProductSpecificationSubCategory",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProductSpecificationSubCategory",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SpecificationId",
                table: "ProductSpecificationSubCategory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "ProductSpecificationSubCategory",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "ProductSpecificationSubCategory",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "SpecificationSubCategory",
                columns: table => new
                {
                    SpecificationsId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecificationSubCategory", x => new { x.SpecificationsId, x.SubCategoryId });
                    table.ForeignKey(
                        name: "FK_SpecificationSubCategory_Specifications_SpecificationsId",
                        column: x => x.SpecificationsId,
                        principalTable: "Specifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecificationSubCategory_SubCategory_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductSpecificationSubCategory_SpecificationId",
                table: "ProductSpecificationSubCategory",
                column: "SpecificationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecificationSubCategory_SubCategoryId",
                table: "SpecificationSubCategory",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategory_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecificationSubCategory_Specifications_SpecificationId",
                table: "ProductSpecificationSubCategory",
                column: "SpecificationId",
                principalTable: "Specifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSpecificationSubCategory_SubCategory_SubCategoryId",
                table: "ProductSpecificationSubCategory",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
