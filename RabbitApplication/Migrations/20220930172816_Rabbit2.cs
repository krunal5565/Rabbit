using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RabbitApplication.Migrations
{
    public partial class Rabbit2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EducationalDetails",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidateId = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    YearOfPassing = table.Column<string>(nullable: true),
                    Percentage = table.Column<string>(nullable: true),
                    Board = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    createddate = table.Column<DateTime>(nullable: false),
                    updateddate = table.Column<DateTime>(nullable: false),
                    isactive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalDetails", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationalDetails");
        }
    }
}
