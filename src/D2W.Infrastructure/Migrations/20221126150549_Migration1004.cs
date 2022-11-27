using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.Migrations
{
    public partial class Migration1004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FabricId",
                table: "DraperyCalculations");

            migrationBuilder.AddColumn<bool>(
                name: "IsRepeating",
                table: "DraperyCalculations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRepeating",
                table: "DraperyCalculations");

            migrationBuilder.AddColumn<Guid>(
                name: "FabricId",
                table: "DraperyCalculations",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
