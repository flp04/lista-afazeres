using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lista_afazeres.Migrations
{
    public partial class AddColunasTimedStampTabelaListas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "Listas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "deleted_at",
                table: "Listas",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "update_at",
                table: "Listas",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Listas");

            migrationBuilder.DropColumn(
                name: "deleted_at",
                table: "Listas");

            migrationBuilder.DropColumn(
                name: "update_at",
                table: "Listas");
        }
    }
}
