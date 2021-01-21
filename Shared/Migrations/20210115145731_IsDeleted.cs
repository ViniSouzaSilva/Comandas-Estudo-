using Microsoft.EntityFrameworkCore.Migrations;

namespace AmbiStore.Shared.Migrations
{
    public partial class IsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ISDELETED",
                table: "COMANDA_HISTORICOs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISDELETED",
                table: "COMANDA_HISTORICOs");
        }
    }
}
