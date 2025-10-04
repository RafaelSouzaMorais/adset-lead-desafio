using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Adset.Veiculos.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItensOpicionais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensOpicionais", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Veiculos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Km = table.Column<int>(type: "int", nullable: false),
                    Cor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veiculos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoFotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdVeiculo = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoFotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VeiculoFotos_Veiculos_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoOpicionais",
                columns: table => new
                {
                    IdVeiculo = table.Column<int>(type: "int", nullable: false),
                    IdItemOpicional = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoOpicionais", x => new { x.IdVeiculo, x.IdItemOpicional });
                    table.ForeignKey(
                        name: "FK_VeiculoOpicionais_ItensOpicionais_IdItemOpicional",
                        column: x => x.IdItemOpicional,
                        principalTable: "ItensOpicionais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VeiculoOpicionais_Veiculos_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VeiculoPortalPacotes",
                columns: table => new
                {
                    IdVeiculo = table.Column<int>(type: "int", nullable: false),
                    Portal = table.Column<int>(type: "int", nullable: false),
                    Pacote = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VeiculoPortalPacotes", x => new { x.IdVeiculo, x.Portal, x.Pacote });
                    table.ForeignKey(
                        name: "FK_VeiculoPortalPacotes_Veiculos_IdVeiculo",
                        column: x => x.IdVeiculo,
                        principalTable: "Veiculos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ItensOpicionais",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Ar Condicionado" },
                    { 2, "Alarme" },
                    { 3, "Airbag" },
                    { 4, "Freio ABS" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoFotos_IdVeiculo",
                table: "VeiculoFotos",
                column: "IdVeiculo");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoOpicionais_IdItemOpicional",
                table: "VeiculoOpicionais",
                column: "IdItemOpicional");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoPortalPacotes_IdVeiculo_Portal",
                table: "VeiculoPortalPacotes",
                columns: new[] { "IdVeiculo", "Portal" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VeiculoFotos");

            migrationBuilder.DropTable(
                name: "VeiculoOpicionais");

            migrationBuilder.DropTable(
                name: "VeiculoPortalPacotes");

            migrationBuilder.DropTable(
                name: "ItensOpicionais");

            migrationBuilder.DropTable(
                name: "Veiculos");
        }
    }
}
