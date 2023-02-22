using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AchatEnLigne.Migrations
{
    public partial class jdks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NombreArticles",
                table: "Commande",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "produit",
                table: "Commande",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreArticles",
                table: "Commande");

            migrationBuilder.DropColumn(
                name: "produit",
                table: "Commande");
        }
    }
}
