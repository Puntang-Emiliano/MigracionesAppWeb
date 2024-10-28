using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PasajesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarPrecioDecimalArticulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "habilitada",
                table: "Articulo",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "precio",
                table: "Articulo",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "habilitada",
                table: "Articulo");

            migrationBuilder.DropColumn(
                name: "precio",
                table: "Articulo");
        }
    }
}
