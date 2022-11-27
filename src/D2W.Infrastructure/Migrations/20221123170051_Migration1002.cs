using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.Migrations
{
    public partial class Migration1002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FabricCalculations_Fabrics_FabricId",
                table: "DraperyCalculations");

            migrationBuilder.DropIndex(
                name: "IX_FabricCalculations_FabricId",
                table: "DraperyCalculations");

            migrationBuilder.AddColumn<string>(
                name: "MaterialType",
                table: "Fabrics",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaterialType",
                table: "Fabrics");

            migrationBuilder.CreateIndex(
                name: "IX_FabricCalculations_FabricId",
                table: "DraperyCalculations",
                column: "FabricId");

            migrationBuilder.AddForeignKey(
                name: "FK_FabricCalculations_Fabrics_FabricId",
                table: "DraperyCalculations",
                column: "FabricId",
                principalTable: "Fabrics",
                principalColumn: "Id");
        }
    }
}
