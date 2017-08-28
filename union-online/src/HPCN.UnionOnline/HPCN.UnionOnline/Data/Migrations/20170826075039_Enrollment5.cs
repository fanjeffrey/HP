using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInputs_Enrollments_EnrollmentId",
                table: "UserInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInputs_PropertyEntries_PropertyId",
                table: "UserInputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserInputs",
                table: "UserInputs");

            migrationBuilder.RenameTable(
                name: "UserInputs",
                newName: "ExtendedPropertyInputs");

            migrationBuilder.RenameIndex(
                name: "IX_UserInputs_PropertyId",
                table: "ExtendedPropertyInputs",
                newName: "IX_ExtendedPropertyInputs_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserInputs_EnrollmentId",
                table: "ExtendedPropertyInputs",
                newName: "IX_ExtendedPropertyInputs_EnrollmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnrollmentId",
                table: "ExtendedPropertyInputs",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExtendedPropertyInputs",
                table: "ExtendedPropertyInputs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExtendedPropertyInputs_Enrollments_EnrollmentId",
                table: "ExtendedPropertyInputs",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExtendedPropertyInputs_PropertyEntries_PropertyId",
                table: "ExtendedPropertyInputs",
                column: "PropertyId",
                principalTable: "PropertyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExtendedPropertyInputs_Enrollments_EnrollmentId",
                table: "ExtendedPropertyInputs");

            migrationBuilder.DropForeignKey(
                name: "FK_ExtendedPropertyInputs_PropertyEntries_PropertyId",
                table: "ExtendedPropertyInputs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExtendedPropertyInputs",
                table: "ExtendedPropertyInputs");

            migrationBuilder.RenameTable(
                name: "ExtendedPropertyInputs",
                newName: "UserInputs");

            migrationBuilder.RenameIndex(
                name: "IX_ExtendedPropertyInputs_PropertyId",
                table: "UserInputs",
                newName: "IX_UserInputs_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExtendedPropertyInputs_EnrollmentId",
                table: "UserInputs",
                newName: "IX_UserInputs_EnrollmentId");

            migrationBuilder.AlterColumn<Guid>(
                name: "EnrollmentId",
                table: "UserInputs",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserInputs",
                table: "UserInputs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInputs_Enrollments_EnrollmentId",
                table: "UserInputs",
                column: "EnrollmentId",
                principalTable: "Enrollments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInputs_PropertyEntries_PropertyId",
                table: "UserInputs",
                column: "PropertyId",
                principalTable: "PropertyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
