using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEleganceShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class NEWPROD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_Customer_CustomerId",
                table: "OrderHeader");

            migrationBuilder.AddColumn<string>(
                name: "ProductImageUrl",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderHeader",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_Customer_CustomerId",
                table: "OrderHeader",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderHeader_Customer_CustomerId",
                table: "OrderHeader");

            migrationBuilder.DropColumn(
                name: "ProductImageUrl",
                table: "Product");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "OrderHeader",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderHeader_Customer_CustomerId",
                table: "OrderHeader",
                column: "CustomerId",
                principalTable: "Customer",
                principalColumn: "CustomerID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
