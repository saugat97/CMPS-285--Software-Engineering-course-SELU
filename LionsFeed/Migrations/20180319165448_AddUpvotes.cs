using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LionsFeed.Migrations
{
    public partial class AddUpvotes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Upvotes",
                columns: table => new
                {
                    PostId = table.Column<int>(nullable: false),
                    UpvoterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upvotes", x => new { x.PostId, x.UpvoterId });
                    table.ForeignKey(
                        name: "FK_Upvotes_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Upvotes_Users_UpvoterId",
                        column: x => x.UpvoterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Upvotes_UpvoterId",
                table: "Upvotes",
                column: "UpvoterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Upvotes");
        }
    }
}
