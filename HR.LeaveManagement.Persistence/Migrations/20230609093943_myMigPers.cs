using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class myMigPers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 9, 12, 39, 43, 189, DateTimeKind.Local).AddTicks(4239), new DateTime(2023, 6, 9, 12, 39, 43, 189, DateTimeKind.Local).AddTicks(4252) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2023, 6, 4, 19, 22, 57, 39, DateTimeKind.Local).AddTicks(1702), new DateTime(2023, 6, 4, 19, 22, 57, 39, DateTimeKind.Local).AddTicks(1714) });
        }
    }
}
