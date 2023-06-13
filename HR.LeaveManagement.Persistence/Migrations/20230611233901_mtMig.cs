using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mtMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 12, 2, 39, 1, 693, DateTimeKind.Local).AddTicks(1830), new DateTime(2023, 6, 12, 2, 39, 1, 693, DateTimeKind.Local).AddTicks(1841) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 9, 12, 39, 43, 189, DateTimeKind.Local).AddTicks(4239), new DateTime(2023, 6, 9, 12, 39, 43, 189, DateTimeKind.Local).AddTicks(4252) });
        }
    }
}
