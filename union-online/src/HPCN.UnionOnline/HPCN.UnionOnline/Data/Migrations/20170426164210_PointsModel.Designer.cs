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
    [Migration("20170426164210_PointsModel")]
    partial class PointsModel
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

                    b.Property<DateTime?>("ConcurrencyTimestamp");

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

                    b.Property<DateTime?>("ConcurrencyTimestamp");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<double>("PointsPayment");

                    b.Property<Guid>("ProductId");

                    b.Property<double>("SelfPayment");

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

                    b.Property<DateTime?>("ConcurrencyTimestamp");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("CartProducts");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BaseCity")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("ChineseName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("ConcurrencyTimestamp");

                    b.Property<string>("CostCenter")
                        .IsRequired()
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
                        .IsRequired()
                        .HasMaxLength(18);

                    b.Property<string>("No")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("OnboardDate");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<string>("WorkCity")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ActivityId");

                    b.Property<DateTime?>("ConcurrencyTimestamp");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<Guid>("EmployeeId");

                    b.Property<double>("MoneyAmount");

                    b.Property<double>("PointsAmount");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ConcurrencyTimestamp");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<Guid>("OrderId");

                    b.Property<double>("PointsPaymentAmount");

                    b.Property<Guid>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<double>("SelfPaymentAmount");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ConcurrencyTimestamp");

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

                    b.Property<DateTime?>("ConcurrencyTimestamp");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<bool>("Disabled");

                    b.Property<Guid?>("EmployeeId");

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

                    b.HasIndex("EmployeeId");

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
                    b.HasOne("HPCN.UnionOnline.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Order", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.Activity", "Activity")
                        .WithMany("Orders")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.OrderDetail", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.Order", "Order")
                        .WithMany("Details")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.Product", "Product")
                        .WithMany("InvolvedOrders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.User", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });
        }
    }
}
