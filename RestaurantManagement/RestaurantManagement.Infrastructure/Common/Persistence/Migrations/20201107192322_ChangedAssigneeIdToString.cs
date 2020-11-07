using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantManagement.Infrastructure.Common.Persistence.Migrations
{
    public partial class ChangedAssigneeIdToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AssigneeId",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AssigneeId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
