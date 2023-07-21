using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Web.Migrations
{
    public partial class HistoryNoteChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserGuid",
                table: "HistoryNotes");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "HistoryNotes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "HistoryNotes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserGuid",
                table: "HistoryNotes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
