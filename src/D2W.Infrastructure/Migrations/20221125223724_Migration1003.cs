using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.Migrations
{
    public partial class Migration1003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraperyCalculations");

            migrationBuilder.CreateTable(
                name: "DraperyCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementSystem = table.Column<int>(type: "int", nullable: false),
                    FinishedLength = table.Column<float>(type: "real", nullable: false),
                    TrimOff = table.Column<float>(type: "real", nullable: false),
                    Hems = table.Column<float>(type: "real", nullable: false),
                    Headings = table.Column<float>(type: "real", nullable: false),
                    Puddling = table.Column<float>(type: "real", nullable: false),
                    PatternRepeatLength = table.Column<float>(type: "real", nullable: false),
                    Fullness = table.Column<float>(type: "real", nullable: false),
                    FabricWidth = table.Column<float>(type: "real", nullable: false),
                    RodFaceWidth = table.Column<float>(type: "real", nullable: false),
                    Overhang = table.Column<float>(type: "real", nullable: false),
                    Overlap = table.Column<float>(type: "real", nullable: false),
                    Return = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DesignConceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FabricId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DraperyCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DraperyCalculations_DesignConcepts_DesignConceptId",
                        column: x => x.DesignConceptId,
                        principalTable: "DesignConcepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DraperyCalculations_DesignConceptId",
                table: "DraperyCalculations",
                column: "DesignConceptId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraperyCalculations");

            migrationBuilder.CreateTable(
                name: "DraperyCalculations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DesignConceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FabricId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FabricWidth = table.Column<float>(type: "real", nullable: false),
                    FinishedLength = table.Column<float>(type: "real", nullable: false),
                    Fullness = table.Column<float>(type: "real", nullable: false),
                    Headings = table.Column<float>(type: "real", nullable: false),
                    Hems = table.Column<float>(type: "real", nullable: false),
                    MeasurementSystem = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Overhang = table.Column<float>(type: "real", nullable: false),
                    Overlap = table.Column<float>(type: "real", nullable: false),
                    PatternRepeatLength = table.Column<float>(type: "real", nullable: false),
                    Puddling = table.Column<float>(type: "real", nullable: false),
                    Return = table.Column<float>(type: "real", nullable: false),
                    RodFaceWidth = table.Column<float>(type: "real", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrimOff = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FabricCalculations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FabricCalculations_DesignConcepts_DesignConceptId",
                        column: x => x.DesignConceptId,
                        principalTable: "DesignConcepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FabricCalculations_DesignConceptId",
                table: "DraperyCalculations",
                column: "DesignConceptId");
        }
    }
}
