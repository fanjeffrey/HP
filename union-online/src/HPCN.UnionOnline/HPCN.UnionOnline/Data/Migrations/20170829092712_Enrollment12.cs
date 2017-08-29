using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntityProperties_EnrollmentActivities_EnrollmentActivityId",
                table: "EntityProperties");

            migrationBuilder.DropIndex(
                name: "IX_EntityProperties_EnrollmentActivityId",
                table: "EntityProperties");

            migrationBuilder.DropColumn(
                name: "EnrollmentActivityId",
                table: "EntityProperties");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EnrollmentActivityId",
                table: "EntityProperties",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EntityProperties_EnrollmentActivityId",
                table: "EntityProperties",
                column: "EnrollmentActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntityProperties_EnrollmentActivities_EnrollmentActivityId",
                table: "EntityProperties",
                column: "EnrollmentActivityId",
                principalTable: "EnrollmentActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
