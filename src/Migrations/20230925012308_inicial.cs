using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RinhaBackend2023Q3.Migrations
{
    public partial class inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Apelido = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Nascimento = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Stack = table.Column<List<string>>(type: "jsonb", nullable: true),
                    SearchIndex = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_Apelido",
                table: "Pessoas",
                column: "Apelido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_SearchIndex",
                table: "Pessoas",
                column: "SearchIndex");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
