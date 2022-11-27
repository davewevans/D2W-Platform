using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.MigrationsProduction
{
    public partial class Migration1005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DraperyCalculations_DesignConceptId",
                table: "DraperyCalculations");

            migrationBuilder.AddColumn<Guid>(
                name: "DesignConceptId",
                table: "WorkOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "WorkroomId",
                table: "WorkOrders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_DesignConceptId",
                table: "WorkOrders",
                column: "DesignConceptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_WorkroomId",
                table: "WorkOrders",
                column: "WorkroomId");

            migrationBuilder.CreateIndex(
                name: "IX_DraperyCalculations_DesignConceptId",
                table: "DraperyCalculations",
                column: "DesignConceptId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_AspNetUsers_WorkroomId",
                table: "WorkOrders",
                column: "WorkroomId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkOrders_DesignConcepts_DesignConceptId",
                table: "WorkOrders",
                column: "DesignConceptId",
                principalTable: "DesignConcepts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_AspNetUsers_WorkroomId",
                table: "WorkOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkOrders_DesignConcepts_DesignConceptId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_DesignConceptId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_WorkroomId",
                table: "WorkOrders");

            migrationBuilder.DropIndex(
                name: "IX_DraperyCalculations_DesignConceptId",
                table: "DraperyCalculations");

            migrationBuilder.DropColumn(
                name: "DesignConceptId",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "WorkroomId",
                table: "WorkOrders");

            migrationBuilder.CreateIndex(
                name: "IX_DraperyCalculations_DesignConceptId",
                table: "DraperyCalculations",
                column: "DesignConceptId");
        }
    }
}
