using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskRequestApplication.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerMailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeMailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkingHour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    TicketID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TicketSubject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketAssigneeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketClosedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketCompletedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketStatus = table.Column<int>(type: "int", nullable: false),
                    TicketCustomerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketPriority = table.Column<int>(type: "int", nullable: false),
                    TicketDifficultyLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.TicketID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Tickets");
        }
    }
}
