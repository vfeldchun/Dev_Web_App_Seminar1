using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbWebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct_Price_Field : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "price",
                table: "products",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "products");
        }
    }
}
