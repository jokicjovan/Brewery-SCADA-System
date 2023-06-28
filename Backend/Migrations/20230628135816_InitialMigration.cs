using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Brewery_SCADA_System.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalogInput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Driver = table.Column<string>(type: "TEXT", nullable: false),
                    IOAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ScanTime = table.Column<int>(type: "INTEGER", nullable: false),
                    ScanOn = table.Column<bool>(type: "INTEGER", nullable: false),
                    LowLimit = table.Column<double>(type: "REAL", nullable: false),
                    HighLimit = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogInput", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnalogOutput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IOAddress = table.Column<string>(type: "TEXT", nullable: false),
                    InitialValue = table.Column<double>(type: "REAL", nullable: false),
                    LowLimit = table.Column<double>(type: "REAL", nullable: false),
                    HighLimit = table.Column<double>(type: "REAL", nullable: false),
                    Unit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalogOutput", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DigitalInput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Driver = table.Column<string>(type: "TEXT", nullable: false),
                    IOAddress = table.Column<string>(type: "TEXT", nullable: false),
                    ScanTime = table.Column<int>(type: "INTEGER", nullable: false),
                    ScanOn = table.Column<bool>(type: "INTEGER", nullable: false),
                    Value = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalInput", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DigitalOutput",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    IOAddress = table.Column<string>(type: "TEXT", nullable: false),
                    InitialValue = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DigitalOutput", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Alarm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AnalogInputId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Alarm_AnalogInput_AnalogInputId",
                        column: x => x.AnalogInputId,
                        principalTable: "AnalogInput",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Alarm_AnalogInputId",
                table: "Alarm",
                column: "AnalogInputId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alarm");

            migrationBuilder.DropTable(
                name: "AnalogOutput");

            migrationBuilder.DropTable(
                name: "DigitalInput");

            migrationBuilder.DropTable(
                name: "DigitalOutput");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AnalogInput");
        }
    }
}
