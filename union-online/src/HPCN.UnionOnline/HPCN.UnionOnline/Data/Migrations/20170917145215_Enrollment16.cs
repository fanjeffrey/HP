using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollees_Enrollings_EnrollingId",
                table: "Enrollees");

            migrationBuilder.DropIndex(
                name: "IX_Enrollees_EnrollingId",
                table: "Enrollees");

            migrationBuilder.DropColumn(
                name: "EnrollingId",
                table: "Enrollees");

            migrationBuilder.AddColumn<Guid>(
                name: "EnrolleeId",
                table: "Enrollings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Enrollings_EnrolleeId",
                table: "Enrollings",
                column: "EnrolleeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollings_Enrollees_EnrolleeId",
                table: "Enrollings",
                column: "EnrolleeId",
                principalTable: "Enrollees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollings_Enrollees_EnrolleeId",
                table: "Enrollings");

            migrationBuilder.DropIndex(
                name: "IX_Enrollings_EnrolleeId",
                table: "Enrollings");

            migrationBuilder.DropColumn(
                name: "EnrolleeId",
                table: "Enrollings");

            migrationBuilder.AddColumn<Guid>(
                name: "EnrollingId",
                table: "Enrollees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enrollees_EnrollingId",
                table: "Enrollees",
                column: "EnrollingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollees_Enrollings_EnrollingId",
                table: "Enrollees",
                column: "EnrollingId",
                principalTable: "Enrollings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
