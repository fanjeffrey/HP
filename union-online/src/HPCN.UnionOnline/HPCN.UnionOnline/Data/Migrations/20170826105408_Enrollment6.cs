using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentActivityExtendedProperties");

            migrationBuilder.DropTable(
                name: "ExtendedPropertyInputs");

            migrationBuilder.RenameColumn(
                name: "Who",
                table: "Enrollments",
                newName: "EnrolleeName");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Enrollments",
                newName: "EnrolleePhoneNumber");

            migrationBuilder.RenameColumn(
                name: "EmailAddress",
                table: "Enrollments",
                newName: "EnrolleeEmailAddress");

            migrationBuilder.AlterColumn<int>(
                name: "ChoiceMode",
                table: "PropertyEntries",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "EnrollmentActivityProperties",
                columns: table => new
                {
                    PropertyEntryId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollmentActivityId = table.Column<Guid>(nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentActivityPropertyInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    Input = table.Column<string>(maxLength: 4000, nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentActivityPropertyInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentActivityPropertyInputs_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentActivityPropertyInputs_PropertyEntries_PropertyId",
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
                name: "IX_EnrollmentActivityPropertyInputs_EnrollmentId",
                table: "EnrollmentActivityPropertyInputs",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentActivityPropertyInputs_PropertyId",
                table: "EnrollmentActivityPropertyInputs",
                column: "PropertyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollmentActivityProperties");

            migrationBuilder.DropTable(
                name: "EnrollmentActivityPropertyInputs");

            migrationBuilder.RenameColumn(
                name: "EnrolleePhoneNumber",
                table: "Enrollments",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "EnrolleeName",
                table: "Enrollments",
                newName: "Who");

            migrationBuilder.RenameColumn(
                name: "EnrolleeEmailAddress",
                table: "Enrollments",
                newName: "EmailAddress");

            migrationBuilder.AlterColumn<int>(
                name: "ChoiceMode",
                table: "PropertyEntries",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "EnrollmentActivityExtendedProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    PropertyId = table.Column<Guid>(nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentActivityExtendedProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentActivityExtendedProperties_EnrollmentActivities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "EnrollmentActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EnrollmentActivityExtendedProperties_PropertyEntries_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExtendedPropertyInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    Input = table.Column<string>(maxLength: 4000, nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExtendedPropertyInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExtendedPropertyInputs_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExtendedPropertyInputs_PropertyEntries_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentActivityExtendedProperties_ActivityId",
                table: "EnrollmentActivityExtendedProperties",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentActivityExtendedProperties_PropertyId",
                table: "EnrollmentActivityExtendedProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendedPropertyInputs_EnrollmentId",
                table: "ExtendedPropertyInputs",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ExtendedPropertyInputs_PropertyId",
                table: "ExtendedPropertyInputs",
                column: "PropertyId");
        }
    }
}
