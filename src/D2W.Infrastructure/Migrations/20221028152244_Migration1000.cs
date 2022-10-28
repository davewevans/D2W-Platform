using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.Migrations
{
    public partial class Migration1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "ContactDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Fax",
                table: "ContactDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LogoUri",
                table: "ContactDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeardAboutUsFrom",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "Fax",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "LogoUri",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "HeardAboutUsFrom",
                table: "AspNetUsers");
        }
    }
}
