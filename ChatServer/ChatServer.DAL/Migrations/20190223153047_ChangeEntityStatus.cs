using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatServer.DAL.Migrations
{
    public partial class ChangeEntityStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "UserConversation");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "Conversation");

            migrationBuilder.AddColumn<int>(
                name: "EntityStatus",
                table: "UserConversation",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityStatus",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityStatus",
                table: "Message",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EntityStatus",
                table: "Conversation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "UserConversation");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "User");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "EntityStatus",
                table: "Conversation");

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "UserConversation",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "Message",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "Conversation",
                nullable: false,
                defaultValue: false);
        }
    }
}
