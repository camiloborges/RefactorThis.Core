using Microsoft.EntityFrameworkCore.Migrations;

namespace RefactorThis.Core.Migrations
{
    public partial class ProductProductOptionsKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductOption_ProductId",
                table: "ProductOption",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOption_Product_ProductId",
                table: "ProductOption",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOption_Product_ProductId",
                table: "ProductOption");

            migrationBuilder.DropIndex(
                name: "IX_ProductOption_ProductId",
                table: "ProductOption");
        }
    }
}
