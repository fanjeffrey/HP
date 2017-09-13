using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollingInputs");

            migrationBuilder.DropTable(
                name: "EntityProperties");

            migrationBuilder.DropTable(
                name: "PropertyValueChoices");

            migrationBuilder.DropTable(
                name: "PropertyEntries");

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
                name: "FieldEntries");

            migrationBuilder.CreateTable(
                name: "PropertyEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChoiceMode = table.Column<int>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    IsRequired = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RequiredMessage = table.Column<string>(maxLength: 200, nullable: true),
                    TypeOfValue = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyEntries", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "EntityProperties",
                columns: table => new
                {
                    PropertyEntryId = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EntityId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityProperties", x => x.PropertyEntryId);
                    table.ForeignKey(
                        name: "FK_EntityProperties_PropertyEntries_PropertyEntryId",
                        column: x => x.PropertyEntryId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValueChoices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    DisplayText = table.Column<string>(maxLength: 200, nullable: false),
                    PropertyId = table.Column<Guid>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValueChoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyValueChoices_PropertyEntries_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "PropertyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrollingInputs_EnrollingId",
                table: "EnrollingInputs",
                column: "EnrollingId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollingInputs_PropertyId",
                table: "EnrollingInputs",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValueChoices_PropertyId",
                table: "PropertyValueChoices",
                column: "PropertyId");
        }
    }
}
