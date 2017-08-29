using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PropertyId",
                table: "EnrollmentActivityProperties",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentActivityProperties_PropertyId",
                table: "EnrollmentActivityProperties",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_EnrollmentActivityProperties_PropertyEntries_PropertyId",
                table: "EnrollmentActivityProperties",
                column: "PropertyId",
                principalTable: "PropertyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EnrollmentActivityProperties_PropertyEntries_PropertyId",
                table: "EnrollmentActivityProperties");

            migrationBuilder.DropIndex(
                name: "IX_EnrollmentActivityProperties_PropertyId",
                table: "EnrollmentActivityProperties");

            migrationBuilder.DropColumn(
                name: "PropertyId",
                table: "EnrollmentActivityProperties");
        }
    }
}
