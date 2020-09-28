using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Architecture.Core.CompositionService.Migrations
{
    //If in need of creating migrations:
    //  * Have Microsoft.EntityFrameworkCore.Tools installed
    //somewhere (even though it seems, it isn't installed anywhere atm)
    //open Package Manager Console and execute
    //Add-Migration $Name -Project Architecture.Core.CompositionService
    //Update-Database



    //also: Auto-generated properties (not primary keys)
    //in fluent-api, do not add the generator to the SQL
    //database; so I add it manually:
    //defaultValueSql: "newsequentialid()"
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guid = table.Column<Guid>(nullable: false, defaultValueSql: "newsequentialid()"),
                    first_name = table.Column<string>(maxLength: 64, nullable: false),
                    initials = table.Column<string>(maxLength: 8, nullable: false),
                    last_name = table.Column<string>(maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_guid",
                table: "Customers",
                column: "guid",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
