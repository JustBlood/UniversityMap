using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UniversityMap.Migrations
{
    public partial class InitialMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Building = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    SvgMap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JQueryScript = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Panoramas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tag = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    Left = table.Column<int>(type: "int", nullable: false),
                    Top = table.Column<int>(type: "int", nullable: false),
                    Right = table.Column<int>(type: "int", nullable: false),
                    Bottom = table.Column<int>(type: "int", nullable: false),
                    MapId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panoramas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Panoramas_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Panoramas_MapId",
                table: "Panoramas",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "Tag_Index",
                table: "Panoramas",
                column: "Tag",
                unique: true,
                filter: "[Tag] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Panoramas");

            migrationBuilder.DropTable(
                name: "Maps");
        }
    }
}
