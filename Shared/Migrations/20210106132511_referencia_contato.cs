using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AmbiStore.Shared.Migrations
{
    public partial class referencia_contato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SAT_SIGN_AC",
                table: "TERMINALs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "REFERENCIAs",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NOME = table.Column<string>(nullable: false),
                    FONE = table.Column<string>(nullable: false),
                    CONTATO_ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REFERENCIAs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REFERENCIAs_CONTATOs_CONTATO_ID",
                        column: x => x.CONTATO_ID,
                        principalTable: "CONTATOs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_REFERENCIAs_CONTATO_ID",
                table: "REFERENCIAs",
                column: "CONTATO_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "REFERENCIAs");

            migrationBuilder.DropColumn(
                name: "SAT_SIGN_AC",
                table: "TERMINALs");
        }
    }
}
