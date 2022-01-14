using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SwayApi.Migrations
{
    public partial class userTdt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "ToDoTasks",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoTasks_userId",
                table: "ToDoTasks",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoTasks_Users_userId",
                table: "ToDoTasks",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoTasks_Users_userId",
                table: "ToDoTasks");

            migrationBuilder.DropIndex(
                name: "IX_ToDoTasks_userId",
                table: "ToDoTasks");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "ToDoTasks");
        }
    }
}
