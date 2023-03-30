using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorksAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeleteBehaviourOnSalesOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderHeader_Customer_CustomerID",
                schema: "SalesLT",
                table: "SalesOrderHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderHeader_Customer_CustomerID",
                schema: "SalesLT",
                table: "SalesOrderHeader",
                column: "CustomerID",
                principalSchema: "SalesLT",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderHeader_Customer_CustomerID",
                schema: "SalesLT",
                table: "SalesOrderHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderHeader_Customer_CustomerID",
                schema: "SalesLT",
                table: "SalesOrderHeader",
                column: "CustomerID",
                principalSchema: "SalesLT",
                principalTable: "Customer",
                principalColumn: "CustomerID");
        }
    }
}
