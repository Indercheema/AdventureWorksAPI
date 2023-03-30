using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdventureWorksAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeleteBehaviourForSalesOrderHeader : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderHeader_Address_BillTo_AddressID",
                schema: "SalesLT",
                table: "SalesOrderHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderHeader_Address_BillTo_AddressID",
                schema: "SalesLT",
                table: "SalesOrderHeader",
                column: "BillToAddressID",
                principalSchema: "SalesLT",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderHeader_Address_BillTo_AddressID",
                schema: "SalesLT",
                table: "SalesOrderHeader");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderHeader_Address_BillTo_AddressID",
                schema: "SalesLT",
                table: "SalesOrderHeader",
                column: "BillToAddressID",
                principalSchema: "SalesLT",
                principalTable: "Address",
                principalColumn: "AddressID");
        }
    }
}
