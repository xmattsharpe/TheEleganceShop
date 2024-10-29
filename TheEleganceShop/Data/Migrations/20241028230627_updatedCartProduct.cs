using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEleganceShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedCartProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartProduct",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cart",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartProduct");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cart");
        }
    }
}
