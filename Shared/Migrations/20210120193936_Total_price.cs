using Microsoft.EntityFrameworkCore.Migrations;

namespace AmbiStore.Shared.Migrations
{
    public partial class Total_price : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "VLR_TOTAL",
                table: "COMANDA_HISTORICOs",
                type: "NUMERIC(18,4)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VLR_TOTAL",
                table: "COMANDA_HISTORICOs");
        }
    }
}
