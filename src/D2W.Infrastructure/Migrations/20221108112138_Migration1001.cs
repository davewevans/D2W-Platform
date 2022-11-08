using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace D2W.Infrastructure.Migrations
{
    public partial class Migration1001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesignConcepts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedByClient = table.Column<bool>(type: "bit", nullable: false),
                    ClientNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainFabric = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccentFabric1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccentFabric2 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccentFabric3 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccentFabric4 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    AccentFabric5 = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignConcepts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fabrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ManufacturerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pattern = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostPerYard = table.Column<float>(type: "real", nullable: false),
                    CostPerMeter = table.Column<float>(type: "real", nullable: false),
                    IsRepeating = table.Column<bool>(type: "bit", nullable: false),
                    RepeatingLengthInInches = table.Column<float>(type: "real", nullable: false),
                    RepeatingLengthInCentimeters = table.Column<float>(type: "real", nullable: false),
                    SwatchImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fabrics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkOrderNumber = table.Column<int>(type: "int", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.Id);
                });

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
                    DesignConceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "WindowMeasurements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeasurementSystem = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Window = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutsideLeftToRight = table.Column<float>(type: "real", nullable: false),
                    OutsideTopToBottom = table.Column<float>(type: "real", nullable: false),
                    InsideLeftToRight = table.Column<float>(type: "real", nullable: false),
                    InsideTopToBottom = table.Column<float>(type: "real", nullable: false),
                    TopFrameToCeilingOrCrown = table.Column<float>(type: "real", nullable: false),
                    BottomFrameToFloor = table.Column<float>(type: "real", nullable: false),
                    FloorToCeilingOrCrown = table.Column<float>(type: "real", nullable: false),
                    TopFrameToFloor = table.Column<float>(type: "real", nullable: false),
                    LeftCasingToWallOrObstruction = table.Column<float>(type: "real", nullable: false),
                    RightCasingToWallOrObstruction = table.Column<float>(type: "real", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DesignConceptId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WindowMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WindowMeasurements_DesignConcepts_DesignConceptId",
                        column: x => x.DesignConceptId,
                        principalTable: "DesignConcepts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DraperyCalculations_DesignConceptId",
                table: "DraperyCalculations",
                column: "DesignConceptId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WindowMeasurements_DesignConceptId",
                table: "WindowMeasurements",
                column: "DesignConceptId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DraperyCalculations");

            migrationBuilder.DropTable(
                name: "Fabrics");

            migrationBuilder.DropTable(
                name: "WindowMeasurements");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.DropTable(
                name: "DesignConcepts");
        }
    }
}
