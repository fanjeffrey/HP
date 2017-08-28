using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyEntries_EnrollmentActivities_EnrollmentActivityId",
                table: "PropertyEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValues_PropertyEntries_PropertyId",
                table: "PropertyValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyValues",
                table: "PropertyValues");

            migrationBuilder.DropIndex(
                name: "IX_PropertyEntries_EnrollmentActivityId",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "EnrollmentActivityId",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "HasMultiValues",
                table: "PropertyEntries");

            migrationBuilder.RenameTable(
                name: "PropertyValues",
                newName: "PropertyValueChoices");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyValues_PropertyId",
                table: "PropertyValueChoices",
                newName: "IX_PropertyValueChoices_PropertyId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "PropertyValueChoices",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "PropertyValueChoices",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "PropertyValueChoices",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PropertyValueChoices",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequiredMessage",
                table: "PropertyEntries",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyEntries",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "PropertyEntries",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PropertyEntries",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ChoiceMode",
                table: "PropertyEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyValueChoices",
                table: "PropertyValueChoices",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 200, nullable: false),
                    EnrollmentActivityId = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: true),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    Who = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enrollments_EnrollmentActivities_EnrollmentActivityId",
                        column: x => x.EnrollmentActivityId,
                        principalTable: "EnrollmentActivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "UserInputs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EnrollmentId = table.Column<Guid>(nullable: true),
                    Input = table.Column<string>(maxLength: 4000, nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInputs_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserInputs_PropertyEntries_PropertyId",
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
                name: "IX_EnrollmentActivityExtendedProperties_ActivityId",
                table: "EnrollmentActivityExtendedProperties",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollmentActivityExtendedProperties_PropertyId",
                table: "EnrollmentActivityExtendedProperties",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInputs_EnrollmentId",
                table: "UserInputs",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInputs_PropertyId",
                table: "UserInputs",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValueChoices_PropertyEntries_PropertyId",
                table: "PropertyValueChoices",
                column: "PropertyId",
                principalTable: "PropertyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValueChoices_PropertyEntries_PropertyId",
                table: "PropertyValueChoices");

            migrationBuilder.DropTable(
                name: "EnrollmentActivityExtendedProperties");

            migrationBuilder.DropTable(
                name: "UserInputs");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyValueChoices",
                table: "PropertyValueChoices");

            migrationBuilder.DropColumn(
                name: "ChoiceMode",
                table: "PropertyEntries");

            migrationBuilder.RenameTable(
                name: "PropertyValueChoices",
                newName: "PropertyValues");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyValueChoices_PropertyId",
                table: "PropertyValues",
                newName: "IX_PropertyValues_PropertyId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "PropertyValues",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 4000);

            migrationBuilder.AlterColumn<Guid>(
                name: "PropertyId",
                table: "PropertyValues",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "DisplayText",
                table: "PropertyValues",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PropertyValues",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequiredMessage",
                table: "PropertyEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "PropertyEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "DisplayName",
                table: "PropertyEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "PropertyEntries",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EnrollmentActivityId",
                table: "PropertyEntries",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasMultiValues",
                table: "PropertyEntries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyValues",
                table: "PropertyValues",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyEntries_EnrollmentActivityId",
                table: "PropertyEntries",
                column: "EnrollmentActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyEntries_EnrollmentActivities_EnrollmentActivityId",
                table: "PropertyEntries",
                column: "EnrollmentActivityId",
                principalTable: "EnrollmentActivities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValues_PropertyEntries_PropertyId",
                table: "PropertyValues",
                column: "PropertyId",
                principalTable: "PropertyEntries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
