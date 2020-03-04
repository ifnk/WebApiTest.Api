using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiTest.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Introduction = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeNo = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    Body = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coordinates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    IsRemoved = table.Column<bool>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coordinates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Coordinates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateTime", "Introduction", "IsRemoved", "Name" },
                values: new object[] { new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"), new DateTime(2020, 2, 20, 19, 56, 49, 801, DateTimeKind.Local).AddTicks(1900), "不好的公司", false, "百度" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateTime", "Introduction", "IsRemoved", "Name" },
                values: new object[] { new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"), new DateTime(2020, 2, 20, 19, 56, 49, 801, DateTimeKind.Local).AddTicks(9816), "微薄", false, "新浪" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "CreateTime", "Introduction", "IsRemoved", "Name" },
                values: new object[] { new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"), new DateTime(2020, 2, 20, 19, 56, 49, 801, DateTimeKind.Local).AddTicks(9875), "买手机 的", false, "小米" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreateTime", "IsRemoved", "Name", "Password", "Status" },
                values: new object[] { new Guid("dc359997-b795-44df-9820-9516e60b8f9c"), new DateTime(2020, 2, 20, 19, 56, 49, 803, DateTimeKind.Local).AddTicks(3580), false, "12", "12", true });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "CreateTime", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "IsRemoved", "LastName" },
                values: new object[] { new Guid("d21af833-45a2-46ee-afe9-82886ee453bd"), new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"), new DateTime(2020, 2, 20, 19, 56, 49, 803, DateTimeKind.Local).AddTicks(5321), new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "李", 1, false, "言红" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "CreateTime", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "IsRemoved", "LastName" },
                values: new object[] { new Guid("857bb6b7-be73-4028-b5e9-bb70f155e8ad"), new Guid("64c208b0-e801-41c9-92f4-cdb76c5f3f1c"), new DateTime(2020, 2, 20, 19, 56, 49, 803, DateTimeKind.Local).AddTicks(7538), new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "徐", 2, false, "东东" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "CreateTime", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "IsRemoved", "LastName" },
                values: new object[] { new Guid("ff5772d0-c235-4d19-84ef-b173c6d8f5f6"), new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"), new DateTime(2020, 2, 20, 19, 56, 49, 803, DateTimeKind.Local).AddTicks(7818), new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "菜", 1, false, "吁困" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "CreateTime", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "IsRemoved", "LastName" },
                values: new object[] { new Guid("15fd5ef5-9fd0-4dbe-a885-43d2e9285857"), new Guid("b9179c40-52f9-42ce-8f67-6972bd297e59"), new DateTime(2020, 2, 20, 19, 56, 49, 803, DateTimeKind.Local).AddTicks(7827), new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "无", 2, false, "一番" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "CreateTime", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "IsRemoved", "LastName" },
                values: new object[] { new Guid("7f5fa708-6327-4baf-abab-576b09c8b8c2"), new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"), new DateTime(2020, 2, 20, 19, 56, 49, 803, DateTimeKind.Local).AddTicks(7836), new DateTime(1984, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "雷", 1, false, "不死" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "CreateTime", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "IsRemoved", "LastName" },
                values: new object[] { new Guid("a021b331-04cf-4d48-a5ce-0e38b0577f91"), new Guid("489748f7-db29-4e9b-8a60-4c655bf7b9a4"), new DateTime(2020, 2, 20, 19, 56, 49, 803, DateTimeKind.Local).AddTicks(7841), new DateTime(1991, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "9527", "路", 2, false, "胃病" });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Coordinates_UserId",
                table: "Coordinates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Coordinates");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
