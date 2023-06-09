using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HR.LeaveManagement.Identity.Migrations
{
    /// <inheritdoc />
    public partial class myMigPers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "35e25899-cf96-4286-bb25-d1eec740a21b", "AQAAAAIAAYagAAAAEMgRnixWbZ8hC3ww1n0pI9KrBoY2NNvVH0fxP0+gt4pUcmOtLLX+5ZpZsSYH58KHeQ==", "159960e7-5d4b-448e-997c-380ce18d1d96" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "09ba1d64-5ac7-4284-9c97-283642e4e157", "AQAAAAIAAYagAAAAEKcayaVcYBesgjh7zpIbjY0EIWi7s3mwC6KB1JbzG37yuncCmBr0PzYC+UrYuxE7Ug==", "f3a53ab3-a02f-4e6b-9817-e80581ffd519" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0efdffe7-829c-4725-ab69-72c66a0673d3", "AQAAAAIAAYagAAAAENwuGmPyHeercVFP+MRW3h2GVC4qkpOlzdHzvbAr5lJwfvpOyYZuHFvoggYId7M2Fg==", "57f0f148-898e-4473-a219-a4abe1778d76" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "83adcbae-657c-4b51-9b89-5d34b049b881", "AQAAAAIAAYagAAAAELRCwPjUXiFAXN9o2uNmdiSu5C18OHxF2D5pDJHuEzQsSTowhG9NxWyQJLX/sVsNlg==", "bf64938d-9191-439d-977e-77b1fe79ddc6" });
        }
    }
}
