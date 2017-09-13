using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxCountOfEnrolles",
                table: "EnrollmentActivities",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "SelfEnrollmentOnly",
                table: "EnrollmentActivities",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNo",
                table: "Enrollees",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxCountOfEnrolles",
                table: "EnrollmentActivities");

            migrationBuilder.DropColumn(
                name: "SelfEnrollmentOnly",
                table: "EnrollmentActivities");

            migrationBuilder.DropColumn(
                name: "EmployeeNo",
                table: "Enrollees");
        }
    }
}
