using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lista_afazeres.Migrations
{
    public partial class ColunaTagIdTabelaAfazeres : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ElementoId",
                table: "Afazeres",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ElementoId",
                table: "Afazeres");
        }
    }
}
