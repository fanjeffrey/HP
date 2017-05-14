using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HPCN.UnionOnline.Data;
using HPCN.UnionOnline.Models;

namespace HPCN.UnionOnline.Data.Migrations
{
    [DbContext(typeof(HPCNUnionOnlineDbContext))]
    [Migration("20170514091739_PointsModels")]
    partial class PointsModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HPCN.UnionOnline.Models.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginTime");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("EndTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.ActivityProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<double>("PointsPayment");

                    b.Property<Guid>("ProductId");

                    b.Property<double>("SelfPayment");

                    b.Property<double>("Stock");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("ProductId");

                    b.ToTable("ActivityProducts");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.CartProduct", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityProductId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<int>("Quantity");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ActivityProductId");

                    b.HasIndex("UserId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Employee", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("BaseCity")
                        .HasMaxLength(50);

                    b.Property<string>("ChineseName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("CostCenter")
                        .HasMaxLength(50);

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<int>("EmployeeStatus");

                    b.Property<int>("EmployeeType");

                    b.Property<int>("Gender");

                    b.Property<string>("IdCardNo")
                        .HasMaxLength(18);

                    b.Property<string>("ManagerEmail")
                        .HasMaxLength(200);

                    b.Property<string>("No")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("OnboardDate");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50);

                    b.Property<string>("TeamAdminAssistant")
                        .HasMaxLength(200);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<string>("WorkCity")
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<double>("MoneyAmount");

                    b.Property<double>("PointsAmount");

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcitivityName");

                    b.Property<Guid>("ActivityId");

                    b.Property<Guid>("ActivityProductId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<Guid>("OrderId");

                    b.Property<double>("PointsPayment");

                    b.Property<double>("PointsPaymentAmount");

                    b.Property<string>("ProductName");

                    b.Property<int>("Quantity");

                    b.Property<double>("SelfPayment");

                    b.Property<double>("SelfPaymentAmount");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("ActivityProductId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("PictureFileName")
                        .HasMaxLength(200);

                    b.Property<double>("PointsPayment");

                    b.Property<double>("SelfPayment");

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<bool>("Disabled");

                    b.Property<bool>("IsAdmin");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.ActivityProduct", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.Activity", "Activity")
                        .WithMany("ActivityProducts")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.Product", "Product")
                        .WithMany("InvolvedActivities")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.CartProduct", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.ActivityProduct", "ActivityProduct")
                        .WithMany("CartProducts")
                        .HasForeignKey("ActivityProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.User", "User")
                        .WithMany("CartPoducts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Employee", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("HPCN.UnionOnline.Models.Employee", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Order", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.OrderDetail", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.ActivityProduct", "ActivityProduct")
                        .WithMany()
                        .HasForeignKey("ActivityProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.Order", "Order")
                        .WithMany("Details")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
