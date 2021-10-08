using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ClientDirectory.DataAccess.Migrations
{
    public partial class InitDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    Identification = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<int>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    ClientId = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_ClientId",
                table: "Addresses",
                column: "ClientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
