// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskRequestApplication.Models;

namespace TaskRequestApplication.Migrations
{
    [DbContext(typeof(TaskRequestApplicationDBContext))]
    [Migration("20211129144106_1")]
    partial class _1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("TaskRequestApplication.Models.Customer", b =>
                {
                    b.Property<string>("CustomerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CustomerMailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerID");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TaskRequestApplication.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeMailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkingHour")
                        .HasColumnType("int");

                    b.Property<string>("managerId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmployeeID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("TaskRequestApplication.Models.Manager", b =>
                {
                    b.Property<string>("ManagerID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ManagerMailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ManagerID");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("TaskRequestApplication.Models.Ticket", b =>
                {
                    b.Property<string>("TicketID")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EmployeeID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TicketAssigneeDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TicketClosedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TicketCompletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TicketCreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TicketCustomerID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketDifficultyLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TicketPriority")
                        .HasColumnType("int");

                    b.Property<DateTime>("TicketReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TicketStatus")
                        .HasColumnType("int");

                    b.Property<string>("TicketSubject")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TicketID");

                    b.ToTable("Tickets");
                });
#pragma warning restore 612, 618
        }
    }
}
