using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class muMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 16, 0, 7, 51, 246, DateTimeKind.Local).AddTicks(1992), new DateTime(2023, 6, 16, 0, 7, 51, 246, DateTimeKind.Local).AddTicks(2009) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 12, 2, 39, 1, 693, DateTimeKind.Local).AddTicks(1830), new DateTime(2023, 6, 12, 2, 39, 1, 693, DateTimeKind.Local).AddTicks(1841) });
        }
    }
}
