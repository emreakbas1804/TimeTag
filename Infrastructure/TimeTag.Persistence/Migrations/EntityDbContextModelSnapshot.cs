﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TimeTag.Domain.Entities.AppDomain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppDomains");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebSite")
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinishJobTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("StartJobTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rlt_Company_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Company_Id");

                    b.ToTable("Company_Departments");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("StartedJobTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rlt_Department_Id")
                        .HasColumnType("int");

                    b.Property<int?>("rlt_FileUpload_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Department_Id");

                    b.HasIndex("rlt_FileUpload_Id");

                    b.ToTable("Department_Employees");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee_Bank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Iban")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("rlt_Employee_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Employee_Id");

                    b.ToTable("Employee_Banks");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee_Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ProcessTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<int>("rlt_Employee_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Employee_Id");

                    b.ToTable("Employee_Logs");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee_Token", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("rlt_Employee_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("rlt_Employee_Id");

                    b.ToTable("Employee_Tokens");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameSurname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.FileUpload", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("OriginalFileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FileUploads");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("LangCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Licance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsAdded")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Licances");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Localization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TagName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsSystemRole")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmailConfirm")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsSuccessLogin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReferanceUrl")
                        .HasColumnType("nvarchar(max)");

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

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("LastUpdateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

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

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company_Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("rlt_Department_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TimeTag.Domain.Entities.FileUpload", "ProfileImage")
                        .WithMany()
                        .HasForeignKey("rlt_FileUpload_Id");

                    b.Navigation("Department");

                    b.Navigation("ProfileImage");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee_Bank", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company_Department_Employee", "Employee")
                        .WithMany("Banks")
                        .HasForeignKey("rlt_Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee_Log", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company_Department_Employee", "Employee")
                        .WithMany("Logs")
                        .HasForeignKey("rlt_Employee_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee_Token", b =>
                {
                    b.HasOne("TimeTag.Domain.Entities.Company_Department_Employee", "Employee")
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
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TimeTag.Domain.Entities.Company_Department_Employee", b =>
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
