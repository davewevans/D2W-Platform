using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.Migrations
{
    public partial class Migration1000 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignConcepts_AspNetUsers_ApplicationUserId",
                table: "DesignConcepts");

            migrationBuilder.DropIndex(
                name: "IX_DesignConcepts_ApplicationUserId",
                table: "DesignConcepts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "DesignConcepts");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "DesignConcepts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_ClientId",
                table: "DesignConcepts",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignConcepts_AspNetUsers_ClientId",
                table: "DesignConcepts",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DesignConcepts_AspNetUsers_ClientId",
                table: "DesignConcepts");

            migrationBuilder.DropIndex(
                name: "IX_DesignConcepts_ClientId",
                table: "DesignConcepts");

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "DesignConcepts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "DesignConcepts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesignConcepts_ApplicationUserId",
                table: "DesignConcepts",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignConcepts_AspNetUsers_ApplicationUserId",
                table: "DesignConcepts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
