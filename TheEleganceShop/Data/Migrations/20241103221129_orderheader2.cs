using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEleganceShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderheader2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OrderPaymentCard",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderPaymentCard",
                table: "OrderHeader");
        }
    }
}
