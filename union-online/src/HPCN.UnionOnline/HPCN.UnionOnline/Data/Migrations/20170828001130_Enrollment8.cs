using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentActivityPropertyInputs_Enrollments_EnrollmentId",
                table: "EnrollmentActivityPropertyInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentActivityPropertyInputs_PropertyEntries_PropertyId",
                table: "EnrollmentActivityPropertyInputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentActivityPropertyInputs",
                table: "EnrollmentActivityPropertyInputs");

            migrationBuilder.DropColumn(
                name: "EnrolleeEmailAddress",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EnrolleeName",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "EnrolleePhoneNumber",
                table: "Enrollments");

            migrationBuilder.RenameTable(
                name: "EnrollmentActivityPropertyInputs",
                newName: "EnrollmentInputs");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentActivityPropertyInputs_PropertyId",
                table: "EnrollmentInputs",
                newName: "IX_EnrollmentInputs_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentActivityPropertyInputs_EnrollmentId",
                table: "EnrollmentInputs",
                newName: "IX_EnrollmentInputs_EnrollmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentInputs",
                table: "EnrollmentInputs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Enrollees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 200, nullable: false),
                    EnrollmentId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollees_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enrollees_EnrollmentId",
                table: "Enrollees",
                column: "EnrollmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentInputs_Enrollments_EnrollmentId",
                table: "EnrollmentInputs",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentInputs_PropertyEntries_PropertyId",
                table: "EnrollmentInputs",
                column: "PropertyId",
                principalTable: "PropertyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentInputs_Enrollments_EnrollmentId",
                table: "EnrollmentInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentInputs_PropertyEntries_PropertyId",
                table: "EnrollmentInputs");

            migrationBuilder.DropTable(
                name: "Enrollees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnrollmentInputs",
                table: "EnrollmentInputs");

            migrationBuilder.RenameTable(
                name: "EnrollmentInputs",
                newName: "EnrollmentActivityPropertyInputs");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentInputs_PropertyId",
                table: "EnrollmentActivityPropertyInputs",
                newName: "IX_EnrollmentActivityPropertyInputs_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_EnrollmentInputs_EnrollmentId",
                table: "EnrollmentActivityPropertyInputs",
                newName: "IX_EnrollmentActivityPropertyInputs_EnrollmentId");

            migrationBuilder.AddColumn<string>(
                name: "EnrolleeEmailAddress",
                table: "Enrollments",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnrolleeName",
                table: "Enrollments",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EnrolleePhoneNumber",
                table: "Enrollments",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnrollmentActivityPropertyInputs",
                table: "EnrollmentActivityPropertyInputs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentActivityPropertyInputs_Enrollments_EnrollmentId",
                table: "EnrollmentActivityPropertyInputs",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentActivityPropertyInputs_PropertyEntries_PropertyId",
                table: "EnrollmentActivityPropertyInputs",
                column: "PropertyId",
                principalTable: "PropertyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
