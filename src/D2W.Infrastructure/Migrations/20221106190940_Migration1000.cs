using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.Migrations
{
    public partial class Migration1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_ApplicationUserId",
                table: "ContactDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "ContactDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ApplicationUserId",
                table: "ContactDetails",
                column: "ApplicationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContactDetails_ApplicationUserId",
                table: "ContactDetails");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ContactDetails");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetails_ApplicationUserId",
                table: "ContactDetails",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");
        }
    }
}
