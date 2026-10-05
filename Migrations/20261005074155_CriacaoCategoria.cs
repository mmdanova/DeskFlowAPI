using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskFlow.API.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tb_categorias",
                columns: table => new
                {
                    codCategoria = table.Column<string>(type: "varchar(150)", nullable: false),
                    nomeCategoria = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tb_categorias", x => x.codCategoria);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tb_categorias");
        }
    }
}
