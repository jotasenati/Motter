using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Motter.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPlanoLocacaoData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserir dados dos planos de locação
            migrationBuilder.InsertData(
                table: "PlanoLocacao",
                columns: new[] { "Id", "DuracaoDias", "ValorDiaria" },
                values: new object[,]
                {
                    { 1, 7, 30.00m },
                    { 2, 15, 28.00m },
                    { 3, 30, 22.00m },
                    { 4, 45, 20.00m },
                    { 5, 50, 18.00m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
         table: "PlanosLocacao",
         keyColumn: "Id",
         keyValues: new object[] { 7, 15, 30, 45, 50 });
        }
    }
}
