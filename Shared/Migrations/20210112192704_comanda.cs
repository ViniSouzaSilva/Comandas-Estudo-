using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmbiStore.Shared.Migrations
{
    public partial class comanda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMANDAs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DATA_CRIACAO = table.Column<DateTime>(nullable: false),
                    STATUS_COMANDA = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMANDAs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COMANDA_HISTORICOs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DATA_COMANDA = table.Column<DateTime>(nullable: false),
                    STATUS = table.Column<bool>(nullable: false),
                    COMANDA_ID = table.Column<int>(nullable: false),
                    ESTOQUE_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMANDA_HISTORICOs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COMANDA_HISTORICOs_COMANDAs_COMANDA_ID",
                        column: x => x.COMANDA_ID,
                        principalTable: "COMANDAs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_COMANDA_HISTORICOs_ESTOQUEs_ESTOQUE_ID",
                        column: x => x.ESTOQUE_ID,
                        principalTable: "ESTOQUEs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_COMANDA_HISTORICOs_COMANDA_ID",
                table: "COMANDA_HISTORICOs",
                column: "COMANDA_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMANDA_HISTORICOs_ESTOQUE_ID",
                table: "COMANDA_HISTORICOs",
                column: "ESTOQUE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMANDA_HISTORICOs");

            migrationBuilder.DropTable(
                name: "COMANDAs");
        }
    }
}
