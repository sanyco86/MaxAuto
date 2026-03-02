using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaxAuto.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersPosition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WorkshopId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_WorkshopId",
                table: "AspNetUsers",
                column: "WorkshopId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_workshops_WorkshopId",
                table: "AspNetUsers",
                column: "WorkshopId",
                principalTable: "workshops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_workshops_WorkshopId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_WorkshopId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WorkshopId",
                table: "AspNetUsers");
        }
    }
}
