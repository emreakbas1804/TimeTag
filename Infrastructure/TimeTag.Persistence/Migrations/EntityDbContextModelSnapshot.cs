﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeTag.Persistence.Context;

#nullable disable

namespace TimeTag.Persistence.Migrations
{
    [DbContext(typeof(EntityDbContext))]
    partial class EntityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TimeTag.Domain.Entities.AppDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("AppDomains");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<string>("WebSite")
                        .HasColumnType("longtext");

                    b.Property<int?>("rlt_FileUpload_Id")
                        .HasColumnType("int");

                    b.Property<int>("rlt_Licance_Id")
                        .HasColumnType("int");

                    b.Property<int>("rlt_User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_FileUpload_Id");

                    b.HasIndex("rlt_Licance_Id")
                        .IsUnique();

                    b.HasIndex("rlt_User_Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("FinishJobTime")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("StartJobTime")
                        .HasColumnType("longtext");

                    b.Property<int>("rlt_Company_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Company_Id");

                    b.ToTable("Company_Departments");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NameSurname")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("StartedJobTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.Property<int>("rlt_Company_Id")
                        .HasColumnType("int");

                    b.Property<int>("rlt_Department_Id")
                        .HasColumnType("int");

                    b.Property<int?>("rlt_FileUpload_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Company_Id");

                    b.HasIndex("rlt_Department_Id");

                    b.HasIndex("rlt_FileUpload_Id");

                    b.ToTable("Company_Employees");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_EmployeeBank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Iban")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("OwnerName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("rlt_Employee_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Employee_Id");

                    b.ToTable("Company_EmployeeBanks");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_EmployeeLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("IpAddress")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("ProcessTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("rlt_Employee_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Employee_Id");

                    b.ToTable("Company_EmployeeLogs");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_EmployeeToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Token")
                        .HasColumnType("longtext");

                    b.Property<int>("rlt_Employee_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Employee_Id");

                    b.ToTable("Company_EmployeeTokens");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.FileUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FileSize")
                        .HasColumnType("longtext");

                    b.Property<string>("FileUrl")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("OriginalFileName")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("LangCode")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Licance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsAdded")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Licances");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Localization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TagName")
                        .HasColumnType("longtext");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.Property<int>("rlt_Language_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Language_Id");

                    b.ToTable("Localizations");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("IsSystemRole")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<int>("EmailConfirm")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("Phone")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.Property<int>("rlt_Role_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Role_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User_LoginLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("IpAddress")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsSuccessLogin")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ReferanceUrl")
                        .HasColumnType("longtext");

                    b.Property<int>("rlt_User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_User_Id");

                    b.ToTable("User_LoginLogs");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User_Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.Property<int>("rlt_User_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_User_Id");

                    b.ToTable("User_Tokens");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.FileUpload", "Logo")
                        .WithMany()
                        .HasForeignKey("rlt_FileUpload_Id");

                    b.HasOne("TimeTag.Domain.Entities.Licance", "Licance")
                        .WithOne("Company")
                        .HasForeignKey("TimeTag.Domain.Entities.Company", "rlt_Licance_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTag.Domain.Entities.User", "Owner")
                        .WithMany("Companies")
                        .HasForeignKey("rlt_User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Licance");

                    b.Navigation("Logo");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company", "Company")
                        .WithMany("Departments")
                        .HasForeignKey("rlt_Company_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Employee", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("rlt_Company_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTag.Domain.Entities.Company_Department", "Department")
                        .WithMany()
                        .HasForeignKey("rlt_Department_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTag.Domain.Entities.FileUpload", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("rlt_FileUpload_Id");

                    b.Navigation("Company");

                    b.Navigation("Department");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_EmployeeBank", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company_Employee", "Employee")
                        .WithMany("Banks")
                        .HasForeignKey("rlt_Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_EmployeeLog", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company_Employee", "Employee")
                        .WithMany("Logs")
                        .HasForeignKey("rlt_Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_EmployeeToken", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company_Employee", "Employee")
                        .WithMany("Tokens")
                        .HasForeignKey("rlt_Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Localization", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("rlt_Language_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("rlt_Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User_LoginLog", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.User", "User")
                        .WithMany("LoginLogs")
                        .HasForeignKey("rlt_User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User_Token", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.User", "User")
                        .WithMany("User_Tokens")
                        .HasForeignKey("rlt_User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company", b =>
                {
                    b.Navigation("Departments");

                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Employee", b =>
                {
                    b.Navigation("Banks");

                    b.Navigation("Logs");

                    b.Navigation("Tokens");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Licance", b =>
                {
                    b.Navigation("Company");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("LoginLogs");

                    b.Navigation("User_Tokens");
                });
#pragma warning restore 612, 618
        }
    }
}
