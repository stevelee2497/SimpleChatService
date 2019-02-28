using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ChatServer.DAL.Migrations
{
    public partial class AddConnectionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Connection",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityStatus = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTimeOffset>(nullable: false),
                    UpdatedTime = table.Column<DateTimeOffset>(nullable: false),
                    UserAgent = table.Column<string>(nullable: true),
                    Connected = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connection", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connection_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connection_UserId",
                table: "Connection",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connection");
        }
    }
}
