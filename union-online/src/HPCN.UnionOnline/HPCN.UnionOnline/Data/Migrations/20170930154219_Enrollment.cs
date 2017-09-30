using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enrollments",
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
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enrollings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EmployeeNo = table.Column<string>(nullable: false),
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
                name: "FieldEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChoiceMode = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    EnrollmentId = table.Column<Guid>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RequiredMessage = table.Column<string>(maxLength: 200, nullable: true),
                    TypeOfValue = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldEntries_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollingId = table.Column<Guid>(nullable: false),
                    FieldEntryId = table.Column<Guid>(nullable: false),
                    Input = table.Column<string>(maxLength: 4000, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldInputs_Enrollings_EnrollingId",
                        column: x => x.EnrollingId,
                        principalTable: "Enrollings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldValueChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    DisplayText = table.Column<string>(maxLength: 200, nullable: false),
                    FieldId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldValueChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldValueChoices_FieldEntries_FieldId",
                        column: x => x.FieldId,
                        principalTable: "FieldEntries",
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
                name: "IX_FieldEntries_EnrollmentId",
                table: "FieldEntries",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldInputs_EnrollingId",
                table: "FieldInputs",
                column: "EnrollingId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldValueChoices_FieldId",
                table: "FieldValueChoices",
                column: "FieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FieldInputs");

            migrationBuilder.DropTable(
                name: "FieldValueChoices");

            migrationBuilder.DropTable(
                name: "Enrollings");

            migrationBuilder.DropTable(
                name: "FieldEntries");

            migrationBuilder.DropTable(
                name: "Enrollments");
        }
    }
}
