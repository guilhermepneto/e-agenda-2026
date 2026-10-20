using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eAgenda.Infraestrutura.Compartilhado.Orm.Migrations
{
    /// <inheritdoc />
    public partial class Add_TBItensTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Concluida",
                table: "TBTarefas",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DataConclusao",
                table: "TBTarefas",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PercentualConcluido",
                table: "TBTarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TBItensTarefa",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TarefaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Concluido = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBItensTarefa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBItensTarefa_TBTarefas_TarefaId",
                        column: x => x.TarefaId,
                        principalTable: "TBTarefas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBItensTarefa_TarefaId",
                table: "TBItensTarefa",
                column: "TarefaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBItensTarefa");

            migrationBuilder.DropColumn(
                name: "Concluida",
                table: "TBTarefas");

            migrationBuilder.DropColumn(
                name: "DataConclusao",
                table: "TBTarefas");

            migrationBuilder.DropColumn(
                name: "PercentualConcluido",
                table: "TBTarefas");
        }
    }
}
