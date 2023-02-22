using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AchatEnLigne.Migrations
{
    public partial class jdksf : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Panier_PanierId",
                table: "Commande");

            migrationBuilder.AlterColumn<int>(
                name: "PanierId",
                table: "Commande",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Panier_PanierId",
                table: "Commande",
                column: "PanierId",
                principalTable: "Panier",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commande_Panier_PanierId",
                table: "Commande");

            migrationBuilder.AlterColumn<int>(
                name: "PanierId",
                table: "Commande",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Commande_Panier_PanierId",
                table: "Commande",
                column: "PanierId",
                principalTable: "Panier",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
