﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApiTest.Api.Data;

namespace WebApiTest.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200115014734_modifyUser")]
    partial class modifyUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("WebApiTest.Api.Entities.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Introduction")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"),
                            Introduction = "不好的公司",
                            Name = "百度"
                        },
                        new
                        {
                            Id = new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"),
                            Introduction = "微薄",
                            Name = "新浪"
                        },
                        new
                        {
                            Id = new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"),
                            Introduction = "买手机 的",
                            Name = "小米"
                        });
                });

            modelBuilder.Entity("WebApiTest.Api.Entities.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmployeeNo")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d21af833-45a2-46ee-afe9-82886ee453bd"),
                            CompanyId = new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"),
                            DateOfBirth = new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "9527",
                            FirstName = "李",
                            Gender = 1,
                            LastName = "言红"
                        },
                        new
                        {
                            Id = new Guid("857bb6b7-be73-4028-b5e9-bb70f155e8ad"),
                            CompanyId = new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"),
                            DateOfBirth = new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "9527",
                            FirstName = "徐",
                            Gender = 1,
                            LastName = "东东"
                        },
                        new
                        {
                            Id = new Guid("ff5772d0-c235-4d19-84ef-b173c6d8f5f6"),
                            CompanyId = new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"),
                            DateOfBirth = new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "9527",
                            FirstName = "菜",
                            Gender = 1,
                            LastName = "吁困"
                        },
                        new
                        {
                            Id = new Guid("15fd5ef5-9fd0-4dbe-a885-43d2e9285857"),
                            CompanyId = new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"),
                            DateOfBirth = new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "9527",
                            FirstName = "无",
                            Gender = 1,
                            LastName = "一番"
                        },
                        new
                        {
                            Id = new Guid("7f5fa708-6327-4baf-abab-576b09c8b8c2"),
                            CompanyId = new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"),
                            DateOfBirth = new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "9527",
                            FirstName = "雷",
                            Gender = 1,
                            LastName = "不死"
                        },
                        new
                        {
                            Id = new Guid("a021b331-04cf-4d48-a5ce-0e38b0577f91"),
                            CompanyId = new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"),
                            DateOfBirth = new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmployeeNo = "9527",
                            FirstName = "路",
                            Gender = 1,
                            LastName = "胃病"
                        });
                });

            modelBuilder.Entity("WebApiTest.Api.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("dc359997-b795-44df-9820-9516e60b8f9c"),
                            Name = "12",
                            Password = "12",
                            Status = true
                        });
                });

            modelBuilder.Entity("WebApiTest.Api.Entities.Employee", b =>
                {
                    b.HasOne("WebApiTest.Api.Entities.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
