using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheEleganceShop.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderheader1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderHeader",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderHeader");
        }
    }
}
