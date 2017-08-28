using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class Enrollment3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ValueType",
                table: "PropertyEntries",
                newName: "DisplayOrder");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PropertyEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "PropertyEntries",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasMultiValues",
                table: "PropertyEntries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRequired",
                table: "PropertyEntries",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RequiredMessage",
                table: "PropertyEntries",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfValue",
                table: "PropertyEntries",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "PropertyValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DisplayOrder",
                table: "PropertyValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DisplayText",
                table: "PropertyValues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "HasMultiValues",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "IsRequired",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "RequiredMessage",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "TypeOfValue",
                table: "PropertyEntries");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "PropertyValues");

            migrationBuilder.DropColumn(
                name: "DisplayOrder",
                table: "PropertyValues");

            migrationBuilder.DropColumn(
                name: "DisplayText",
                table: "PropertyValues");

            migrationBuilder.RenameColumn(
                name: "DisplayOrder",
                table: "PropertyEntries",
                newName: "ValueType");
        }
    }
}
