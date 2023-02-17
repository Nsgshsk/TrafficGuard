using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafficGuard.Migrations
{
    public partial class UpdatedDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Cities",
                table: "Districts");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Cities",
                table: "Districts",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_Cities",
                table: "Districts");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_Cities",
                table: "Districts",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id");
        }
    }
}
