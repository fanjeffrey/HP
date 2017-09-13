using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollees_Enrollments_EnrollmentId",
                table: "Enrollees");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_EnrollmentActivities_EnrollmentActivityId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments");

            migrationBuilder.DropTable(
                name: "EnrollmentActivities");

            migrationBuilder.DropTable(
                name: "EnrollmentInputs");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_EnrollmentActivityId",
                table: "Enrollments");

            migrationBuilder.DropIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EnrollmentActivityId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Enrollees",
                newName: "EnrollingId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollees_EnrollmentId",
                table: "Enrollees",
                newName: "IX_Enrollees_EnrollingId");

            migrationBuilder.AddColumn<DateTime>(
                name: "BeginTime",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Enrollments",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Enrollments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MaxCountOfEnrolles",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Enrollments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "SelfEnrollmentOnly",
                table: "Enrollments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Enrollments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enrollings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollings_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrollingInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollingId = table.Column<Guid>(nullable: false),
                    Input = table.Column<string>(maxLength: 4000, nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollingInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollingInputs_Enrollings_EnrollingId",
                        column: x => x.EnrollingId,
                        principalTable: "Enrollings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollingInputs_PropertyEntries_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollings_EnrollmentId",
                table: "Enrollings",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollings_UserId",
                table: "Enrollings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollingInputs_EnrollingId",
                table: "EnrollingInputs",
                column: "EnrollingId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollingInputs_PropertyId",
                table: "EnrollingInputs",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollees_Enrollings_EnrollingId",
                table: "Enrollees",
                column: "EnrollingId",
                principalTable: "Enrollings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollees_Enrollings_EnrollingId",
                table: "Enrollees");

            migrationBuilder.DropTable(
                name: "EnrollingInputs");

            migrationBuilder.DropTable(
                name: "Enrollings");

            migrationBuilder.DropColumn(
                name: "BeginTime",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "MaxCountOfEnrolles",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "SelfEnrollmentOnly",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "EnrollingId",
                table: "Enrollees",
                newName: "EnrollmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollees_EnrollingId",
                table: "Enrollees",
                newName: "IX_Enrollees_EnrollmentId");

            migrationBuilder.AddColumn<Guid>(
                name: "EnrollmentActivityId",
                table: "Enrollments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Enrollments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "EnrollmentActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    MaxCountOfEnrolles = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    SelfEnrollmentOnly = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollmentActivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EnrollmentInputs",
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
                    table.PrimaryKey("PK_EnrollmentInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EnrollmentInputs_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollmentInputs_PropertyEntries_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_EnrollmentActivityId",
                table: "Enrollments",
                column: "EnrollmentActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_UserId",
                table: "Enrollments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentInputs_EnrollmentId",
                table: "EnrollmentInputs",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentInputs_PropertyId",
                table: "EnrollmentInputs",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollees_Enrollments_EnrollmentId",
                table: "Enrollees",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_EnrollmentActivities_EnrollmentActivityId",
                table: "Enrollments",
                column: "EnrollmentActivityId",
                principalTable: "EnrollmentActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Users_UserId",
                table: "Enrollments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
