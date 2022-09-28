using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RabbitApplication.Migrations
{
    public partial class RabbitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateFiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandiateFileId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Createddate = table.Column<DateTime>(nullable: false),
                    Updateddate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateFiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateFiles");
        }
    }
}
