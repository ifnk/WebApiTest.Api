using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiTest.Api.Migrations
{
    public partial class addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("d21af833-45a2-46ee-afe9-82886ee453bd"), new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"), new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "李", 1, "言红" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("857bb6b7-be73-4028-b5e9-bb70f155e8ad"), new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"), new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "徐", 1, "东东" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("ff5772d0-c235-4d19-84ef-b173c6d8f5f6"), new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"), new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "菜", 1, "吁困" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("15fd5ef5-9fd0-4dbe-a885-43d2e9285857"), new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"), new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "无", 1, "一番" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7f5fa708-6327-4baf-abab-576b09c8b8c2"), new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"), new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "雷", 1, "不死" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("a021b331-04cf-4d48-a5ce-0e38b0577f91"), new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"), new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "路", 1, "胃病" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password" },
                values: new object[] { new Guid("dc359997-b795-44df-9820-9516e60b8f9c"), "12", "12" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("15fd5ef5-9fd0-4dbe-a885-43d2e9285857"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7f5fa708-6327-4baf-abab-576b09c8b8c2"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("857bb6b7-be73-4028-b5e9-bb70f155e8ad"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("a021b331-04cf-4d48-a5ce-0e38b0577f91"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("d21af833-45a2-46ee-afe9-82886ee453bd"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("ff5772d0-c235-4d19-84ef-b173c6d8f5f6"));
        }
    }
}
