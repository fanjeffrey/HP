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
    [Migration("20170911072018_Enrollment14")]
    partial class Enrollment14
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

            modelBuilder.Entity("HPCN.UnionOnline.Models.Enrollee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("EmployeeNo")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<Guid?>("EnrollingId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(50);

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("EnrollingId");

                    b.ToTable("Enrollees");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Enrolling", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<Guid>("EnrollmentId");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EnrollmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Enrollings");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.EnrollingInput", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<Guid>("EnrollingId");

                    b.Property<string>("Input")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<Guid>("PropertyId");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.HasIndex("EnrollingId");

                    b.HasIndex("PropertyId");

                    b.ToTable("EnrollingInputs");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("BeginTime");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(2000);

                    b.Property<DateTime>("EndTime");

                    b.Property<int>("MaxCountOfEnrolles");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<bool>("SelfEnrollmentOnly");

                    b.Property<int>("Status");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.EntityProperty", b =>
                {
                    b.Property<Guid>("PropertyEntryId");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<Guid>("EntityId");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("PropertyEntryId");

                    b.ToTable("EntityProperties");
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

            modelBuilder.Entity("HPCN.UnionOnline.Models.PropertyEntry", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChoiceMode");

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("DisplayOrder");

                    b.Property<bool>("IsRequired");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("RequiredMessage")
                        .HasMaxLength(200);

                    b.Property<int>("TypeOfValue");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.HasKey("Id");

                    b.ToTable("PropertyEntries");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.PropertyValueChoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CreatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("CreatedTime");

                    b.Property<string>("Description")
                        .HasMaxLength(200);

                    b.Property<int>("DisplayOrder");

                    b.Property<string>("DisplayText")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<Guid>("PropertyId");

                    b.Property<string>("UpdatedBy")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("UpdatedTime");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.HasKey("Id");

                    b.HasIndex("PropertyId");

                    b.ToTable("PropertyValueChoices");
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

            modelBuilder.Entity("HPCN.UnionOnline.Models.Enrollee", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.Enrolling")
                        .WithMany("Enrollee")
                        .HasForeignKey("EnrollingId");
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.Enrolling", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.Enrollment", "Enrollment")
                        .WithMany()
                        .HasForeignKey("EnrollmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.User", "User")
                        .WithMany("Enrollments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.EnrollingInput", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.Enrolling", "Enrolling")
                        .WithMany("PropertyInputs")
                        .HasForeignKey("EnrollingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HPCN.UnionOnline.Models.PropertyEntry", "Property")
                        .WithMany()
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HPCN.UnionOnline.Models.EntityProperty", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.PropertyEntry", "PropertyEntry")
                        .WithOne("Entity")
                        .HasForeignKey("HPCN.UnionOnline.Models.EntityProperty", "PropertyEntryId")
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

            modelBuilder.Entity("HPCN.UnionOnline.Models.PropertyValueChoice", b =>
                {
                    b.HasOne("HPCN.UnionOnline.Models.PropertyEntry", "Property")
                        .WithMany("ValueChoices")
                        .HasForeignKey("PropertyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
