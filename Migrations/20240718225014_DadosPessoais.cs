using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DailyFitAPI.Migrations
{
    public partial class DadosPessoais : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DadosPessoais_DadosPessoaisId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "DadosPessoais");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DadosPessoaisId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DadosPessoaisId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DadosPessoaisId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DadosPessoais",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Altura = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CPF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DataNacimento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Peso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosPessoais", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DadosPessoaisId",
                table: "AspNetUsers",
                column: "DadosPessoaisId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DadosPessoais_DadosPessoaisId",
                table: "AspNetUsers",
                column: "DadosPessoaisId",
                principalTable: "DadosPessoais",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
