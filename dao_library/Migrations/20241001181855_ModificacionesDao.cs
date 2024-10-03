using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace daolibrary.Migrations
{
    /// <inheritdoc />
    public partial class ModificacionesDao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileTypes_FileTypeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Lockeds_Persons_UserFromId",
                table: "Lockeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Lockeds_Persons_UserToId",
                table: "Lockeds");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Post_PublishedReportId",
                table: "Report");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Post",
                table: "Post");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lockeds",
                table: "Lockeds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes");

            migrationBuilder.RenameTable(
                name: "Post",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "Lockeds",
                newName: "Locked");

            migrationBuilder.RenameTable(
                name: "FileTypes",
                newName: "FileType");

            migrationBuilder.RenameIndex(
                name: "IX_Lockeds_UserToId",
                table: "Locked",
                newName: "IX_Locked_UserToId");

            migrationBuilder.RenameIndex(
                name: "IX_Lockeds_UserFromId",
                table: "Locked",
                newName: "IX_Locked_UserFromId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locked",
                table: "Locked",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileType",
                table: "FileType",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserBans",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StartDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    EndDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Reason = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBans_Persons_UserId",
                        column: x => x.UserId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserBans_UserId",
                table: "UserBans",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileType_FileTypeId",
                table: "Files",
                column: "FileTypeId",
                principalTable: "FileType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Locked_Persons_UserFromId",
                table: "Locked",
                column: "UserFromId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locked_Persons_UserToId",
                table: "Locked",
                column: "UserToId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Posts_PublishedReportId",
                table: "Report",
                column: "PublishedReportId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileType_FileTypeId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Locked_Persons_UserFromId",
                table: "Locked");

            migrationBuilder.DropForeignKey(
                name: "FK_Locked_Persons_UserToId",
                table: "Locked");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_Posts_PublishedReportId",
                table: "Report");

            migrationBuilder.DropTable(
                name: "UserBans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locked",
                table: "Locked");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileType",
                table: "FileType");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "Post");

            migrationBuilder.RenameTable(
                name: "Locked",
                newName: "Lockeds");

            migrationBuilder.RenameTable(
                name: "FileType",
                newName: "FileTypes");

            migrationBuilder.RenameIndex(
                name: "IX_Locked_UserToId",
                table: "Lockeds",
                newName: "IX_Lockeds_UserToId");

            migrationBuilder.RenameIndex(
                name: "IX_Locked_UserFromId",
                table: "Lockeds",
                newName: "IX_Lockeds_UserFromId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Post",
                table: "Post",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lockeds",
                table: "Lockeds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileTypes",
                table: "FileTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileTypes_FileTypeId",
                table: "Files",
                column: "FileTypeId",
                principalTable: "FileTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lockeds_Persons_UserFromId",
                table: "Lockeds",
                column: "UserFromId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lockeds_Persons_UserToId",
                table: "Lockeds",
                column: "UserToId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_Post_PublishedReportId",
                table: "Report",
                column: "PublishedReportId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
