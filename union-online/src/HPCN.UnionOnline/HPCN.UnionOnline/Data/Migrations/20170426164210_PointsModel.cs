using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HPCN.UnionOnline.Data.Migrations
{
    public partial class PointsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BeginTime = table.Column<DateTime>(nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    EndTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Status = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BaseCity = table.Column<string>(maxLength: 50, nullable: false),
                    ChineseName = table.Column<string>(maxLength: 50, nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CostCenter = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    DisplayName = table.Column<string>(maxLength: 50, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 200, nullable: false),
                    EmployeeStatus = table.Column<int>(nullable: false),
                    EmployeeType = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    IdCardNo = table.Column<string>(maxLength: 18, nullable: false),
                    No = table.Column<string>(maxLength: 50, nullable: false),
                    OnboardDate = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    WorkCity = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    PictureFileName = table.Column<string>(maxLength: 200, nullable: true),
                    PointsPayment = table.Column<double>(nullable: false),
                    SelfPayment = table.Column<double>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<Guid>(nullable: false),
                    MoneyAmount = table.Column<double>(nullable: false),
                    PointsAmount = table.Column<double>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    Disabled = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<Guid>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    Username = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ActivityId = table.Column<Guid>(nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    PointsPayment = table.Column<double>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    SelfPayment = table.Column<double>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityProducts_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    OrderId = table.Column<Guid>(nullable: false),
                    PointsPaymentAmount = table.Column<double>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    SelfPaymentAmount = table.Column<double>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConcurrencyTimestamp = table.Column<DateTime>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    UpdatedBy = table.Column<string>(maxLength: 200, nullable: true),
                    UpdatedTime = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartProducts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityProducts_ActivityId",
                table: "ActivityProducts",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityProducts_ProductId",
                table: "ActivityProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_ProductId",
                table: "CartProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_UserId",
                table: "CartProducts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ActivityId",
                table: "Orders",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_EmployeeId",
                table: "Orders",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_EmployeeId",
                table: "Users",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityProducts");

            migrationBuilder.DropTable(
                name: "CartProducts");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
