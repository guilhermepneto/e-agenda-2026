using eAgenda.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eAgenda.Infraestrutura.Compartilhado.Orm.Migrations;

[DbContext(typeof(EAgendaDbContext))]
[Migration("20261020180000_Add_TBContatosCompromissos")]
public sealed class AddTBContatosCompromissos : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TBContatos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Email = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                Telefone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Cargo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                Empresa = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_TBContatos", x => x.Id));

        migrationBuilder.CreateIndex(name: "IX_TBContatos_Email", table: "TBContatos", column: "Email", unique: true);
        migrationBuilder.CreateIndex(name: "IX_TBContatos_Telefone", table: "TBContatos", column: "Telefone", unique: true);

        migrationBuilder.CreateTable(
            name: "TBCompromissos",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Assunto = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                DataOcorrencia = table.Column<DateOnly>(type: "date", nullable: false),
                HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                HoraTermino = table.Column<TimeOnly>(type: "time", nullable: false),
                Tipo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                LinkOuLocal = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                ContatoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TBCompromissos", x => x.Id);
                table.ForeignKey("FK_TBCompromissos_TBContatos_ContatoId", x => x.ContatoId, "TBContatos", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(name: "IX_TBCompromissos_ContatoId", table: "TBCompromissos", column: "ContatoId");
        migrationBuilder.CreateIndex(name: "IX_TBCompromissos_DataOcorrencia_HoraInicio_HoraTermino", table: "TBCompromissos", columns: new[] { "DataOcorrencia", "HoraInicio", "HoraTermino" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "TBCompromissos");
        migrationBuilder.DropTable(name: "TBContatos");
    }
}
