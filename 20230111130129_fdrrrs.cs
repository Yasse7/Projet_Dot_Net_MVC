using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AchatEnLigne.Migrations
{
    public partial class fdrrrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LignePanierCommande_Commande_CommandeId",
                table: "LignePanierCommande");

            migrationBuilder.DropForeignKey(
                name: "FK_LignePanierCommande_User_UserId",
                table: "LignePanierCommande");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LignePanierCommande",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CommandeId",
                table: "LignePanierCommande",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_LignePanierCommande_Commande_CommandeId",
                table: "LignePanierCommande",
                column: "CommandeId",
                principalTable: "Commande",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LignePanierCommande_User_UserId",
                table: "LignePanierCommande",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LignePanierCommande_Commande_CommandeId",
                table: "LignePanierCommande");

            migrationBuilder.DropForeignKey(
                name: "FK_LignePanierCommande_User_UserId",
                table: "LignePanierCommande");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "LignePanierCommande",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CommandeId",
                table: "LignePanierCommande",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LignePanierCommande_Commande_CommandeId",
                table: "LignePanierCommande",
                column: "CommandeId",
                principalTable: "Commande",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LignePanierCommande_User_UserId",
                table: "LignePanierCommande",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
