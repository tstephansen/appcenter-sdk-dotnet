using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppCenterApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SdkName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SdkVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Model = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OemName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OsName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OsVersion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OsBuild = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Locale = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TimeZoneOffset = table.Column<int>(type: "int", nullable: false),
                    ScreenSize = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AppVersion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AppBuild = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AppNamespace = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Devices__3214EC07B8F1C2B7", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExceptionDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StackTrace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentExceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Exceptio__3214EC07CB45A765", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Exception__Paren__30F848ED",
                        column: x => x.ParentExceptionId,
                        principalTable: "ExceptionDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventLog__3214EC074FAE5E2B", x => x.Id);
                    table.ForeignKey(
                        name: "FK__EventLogs__Devic__2E1BDC42",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HandledErrorLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HandledE__3214EC07753C19D9", x => x.Id);
                    table.ForeignKey(
                        name: "FK__HandledEr__Devic__31EC6D26",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__HandledEr__Excep__32E0915F",
                        column: x => x.ExceptionId,
                        principalTable: "ExceptionDetails",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ManagedErrorLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExceptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcessId = table.Column<int>(type: "int", nullable: false),
                    ProcessName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Fatal = table.Column<bool>(type: "bit", nullable: false),
                    AppLaunchTimestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Architecture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime", nullable: false),
                    Sid = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ManagedE__3214EC077437F4FD", x => x.Id);
                    table.ForeignKey(
                        name: "FK__ManagedEr__Devic__33D4B598",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__ManagedEr__Excep__34C8D9D1",
                        column: x => x.ExceptionId,
                        principalTable: "ExceptionDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EventProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PropertyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    PropertyValue = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HandledErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EventLogId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EventPro__3214EC07CA1148E6", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventProperties_EventLogs",
                        column: x => x.EventLogId,
                        principalTable: "EventLogs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventProperties_HandledErrorLogs",
                        column: x => x.HandledErrorId,
                        principalTable: "HandledErrorLogs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventLogs_DeviceId",
                table: "EventLogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_EventProperties_EventLogId",
                table: "EventProperties",
                column: "EventLogId");

            migrationBuilder.CreateIndex(
                name: "IX_EventProperties_HandledErrorId",
                table: "EventProperties",
                column: "HandledErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExceptionDetails_ParentExceptionId",
                table: "ExceptionDetails",
                column: "ParentExceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_HandledErrorLogs_DeviceId",
                table: "HandledErrorLogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_HandledErrorLogs_ExceptionId",
                table: "HandledErrorLogs",
                column: "ExceptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedErrorLogs_DeviceId",
                table: "ManagedErrorLogs",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ManagedErrorLogs_ExceptionId",
                table: "ManagedErrorLogs",
                column: "ExceptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventProperties");

            migrationBuilder.DropTable(
                name: "ManagedErrorLogs");

            migrationBuilder.DropTable(
                name: "EventLogs");

            migrationBuilder.DropTable(
                name: "HandledErrorLogs");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "ExceptionDetails");
        }
    }
}
