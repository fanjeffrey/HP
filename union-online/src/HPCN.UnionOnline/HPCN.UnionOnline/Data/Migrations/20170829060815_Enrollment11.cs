using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentActivityProperties");

            migrationBuilder.CreateTable(
                name: "EntityProperties",
                columns: table => new
                {
                    PropertyEntryId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollmentActivityId = table.Column<Guid>(nullable: true),
                    EntityId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProperties", x => x.PropertyEntryId);
                    table.ForeignKey(
                        name: "FK_EntityProperties_EnrollmentActivities_EnrollmentActivityId",
                        column: x => x.EnrollmentActivityId,
                        principalTable: "EnrollmentActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EntityProperties_PropertyEntries_PropertyEntryId",
                        column: x => x.PropertyEntryId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperties_EnrollmentActivityId",
                table: "EntityProperties",
                column: "EnrollmentActivityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityProperties");

            migrationBuilder.CreateTable(
                name: "EnrollmentActivityProperties",
                columns: table => new
                {
                    PropertyEntryId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollmentActivityId = table.Column<Guid>(nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentActivityProperties", x => x.PropertyEntryId);
                    table.ForeignKey(
                        name: "FK_EnrollmentActivityProperties_EnrollmentActivities_EnrollmentActivityId",
                        column: x => x.EnrollmentActivityId,
                        principalTable: "EnrollmentActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentActivityProperties_PropertyEntries_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentActivityProperties_EnrollmentActivityId",
                table: "EnrollmentActivityProperties",
                column: "EnrollmentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentActivityProperties_PropertyId",
                table: "EnrollmentActivityProperties",
                column: "PropertyId");
        }
    }
}
