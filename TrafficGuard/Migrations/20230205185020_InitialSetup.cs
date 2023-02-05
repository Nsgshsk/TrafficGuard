using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrafficGuard.Migrations
{
    public partial class InitialSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accidents");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
