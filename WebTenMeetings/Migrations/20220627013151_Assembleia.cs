using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebTenMeetings.Migrations
{
    public partial class Assembleia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assembleia",
                columns: table => new
                {
                    AssembleiaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assembleia", x => x.AssembleiaId);
                });

            migrationBuilder.CreateTable(
                name: "Participante",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssembleiaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participante", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participante_Assembleia_AssembleiaId",
                        column: x => x.AssembleiaId,
                        principalTable: "Assembleia",
                        principalColumn: "AssembleiaId");
                });

            migrationBuilder.CreateTable(
                name: "Pauta",
                columns: table => new
                {
                    PautaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QtdTotalVotos = table.Column<int>(type: "int", nullable: false),
                    AssembleiaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pauta", x => x.PautaId);
                    table.ForeignKey(
                        name: "FK_Pauta_Assembleia_AssembleiaId",
                        column: x => x.AssembleiaId,
                        principalTable: "Assembleia",
                        principalColumn: "AssembleiaId");
                });

            migrationBuilder.CreateTable(
                name: "ParticipantePauta",
                columns: table => new
                {
                    ParticipantesId = table.Column<int>(type: "int", nullable: false),
                    PautasVotadasPautaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParticipantePauta", x => new { x.ParticipantesId, x.PautasVotadasPautaId });
                    table.ForeignKey(
                        name: "FK_ParticipantePauta_Participante_ParticipantesId",
                        column: x => x.ParticipantesId,
                        principalTable: "Participante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParticipantePauta_Pauta_PautasVotadasPautaId",
                        column: x => x.PautasVotadasPautaId,
                        principalTable: "Pauta",
                        principalColumn: "PautaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participante_AssembleiaId",
                table: "Participante",
                column: "AssembleiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantePauta_PautasVotadasPautaId",
                table: "ParticipantePauta",
                column: "PautasVotadasPautaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pauta_AssembleiaId",
                table: "Pauta",
                column: "AssembleiaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ParticipantePauta");

            migrationBuilder.DropTable(
                name: "Participante");

            migrationBuilder.DropTable(
                name: "Pauta");

            migrationBuilder.DropTable(
                name: "Assembleia");
        }
    }
}
