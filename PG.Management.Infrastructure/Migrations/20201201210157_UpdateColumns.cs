using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PG.Management.Infrastructure.Migrations
{
    public partial class UpdateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Countries_CountryId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectManagers_ProjectManagerId",
                table: "Projects");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectManagerId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                table: "Projects",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Countries_CountryId",
                table: "Projects",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectManagers_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Countries_CountryId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectManagers_ProjectManagerId",
                table: "Projects");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProjectManagerId",
                table: "Projects",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "CountryId",
                table: "Projects",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Countries_CountryId",
                table: "Projects",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectManagers_ProjectManagerId",
                table: "Projects",
                column: "ProjectManagerId",
                principalTable: "ProjectManagers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
