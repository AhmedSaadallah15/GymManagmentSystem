using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GymManagementDAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreateFixd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "SessionEndtDate",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "EndtDate",
                table: "Sessions",
                newName: "EndDate");

            migrationBuilder.AddCheckConstraint(
                name: "SessionEndDate",
                table: "Sessions",
                sql: "EndDate > StartDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "SessionEndDate",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Sessions",
                newName: "EndtDate");

            migrationBuilder.AddCheckConstraint(
                name: "SessionEndtDate",
                table: "Sessions",
                sql: "EndtDate > StartDate");
        }
    }
}
