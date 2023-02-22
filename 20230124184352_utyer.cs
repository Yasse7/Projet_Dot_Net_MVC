using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AchatEnLigne.Migrations
{
    public partial class utyer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Produit",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Produit");
        }
    }
}
