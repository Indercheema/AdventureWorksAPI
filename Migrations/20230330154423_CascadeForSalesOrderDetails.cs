using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorksAPI.Migrations
{
    /// <inheritdoc />
    public partial class CascadeForSalesOrderDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDetail_Product_ProductID",
                schema: "SalesLT",
                table: "SalesOrderDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDetail_Product_ProductID",
                schema: "SalesLT",
                table: "SalesOrderDetail",
                column: "ProductID",
                principalSchema: "SalesLT",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderDetail_Product_ProductID",
                schema: "SalesLT",
                table: "SalesOrderDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderDetail_Product_ProductID",
                schema: "SalesLT",
                table: "SalesOrderDetail",
                column: "ProductID",
                principalSchema: "SalesLT",
                principalTable: "Product",
                principalColumn: "ProductID");
        }
    }
}
