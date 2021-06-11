using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Back_EndTest.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "status",
                columns: table => new
                {
                    id_status = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_id_status", x => x.id_status);
                });

            migrationBuilder.CreateTable(
                name: "ticket",
                columns: table => new
                {
                    id_ticket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    creation_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    id_status_fk = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PrimaryKey_id_ticket", x => x.id_ticket);
                    table.ForeignKey(
                        name: "status_ibfk_1",
                        column: x => x.id_status_fk,
                        principalTable: "status",
                        principalColumn: "id_status",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "id_status", "status" },
                values: new object[] { 0, "Abierto" });

            migrationBuilder.InsertData(
                table: "status",
                columns: new[] { "id_status", "status" },
                values: new object[] { 1, "Cerrado" });

            migrationBuilder.CreateIndex(
                name: "IX_ticket_id_status_fk",
                table: "ticket",
                column: "id_status_fk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ticket");

            migrationBuilder.DropTable(
                name: "status");
        }
    }
}
