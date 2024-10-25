using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEleganceShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductCategory",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductShoeSize",
                table: "OrderDetail",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductCategory",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductShoeSize",
                table: "OrderDetail");
        }
    }
}
