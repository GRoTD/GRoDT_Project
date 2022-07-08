using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SlippAPI.Migrations
{
    public partial class secondupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Tickets_TicketId",
                table: "Image");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Image",
                table: "Image");

            migrationBuilder.RenameTable(
                name: "Image",
                newName: "Images");

            migrationBuilder.RenameIndex(
                name: "IX_Image_TicketId",
                table: "Images",
                newName: "IX_Images_TicketId");

            migrationBuilder.AddColumn<string>(
                name: "Website",
                table: "Clubs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "ClubId",
                table: "Images",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AppUserTicket",
                columns: table => new
                {
                    FavouriteTicketsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedByUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTicket", x => new { x.FavouriteTicketsId, x.SavedByUsersId });
                    table.ForeignKey(
                        name: "FK_AppUserTicket_AppUsers_SavedByUsersId",
                        column: x => x.SavedByUsersId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserTicket_Tickets_FavouriteTicketsId",
                        column: x => x.FavouriteTicketsId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClubId",
                table: "Images",
                column: "ClubId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserTicket_SavedByUsersId",
                table: "AppUserTicket",
                column: "SavedByUsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Clubs_ClubId",
                table: "Images",
                column: "ClubId",
                principalTable: "Clubs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Tickets_TicketId",
                table: "Images",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Clubs_ClubId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Tickets_TicketId",
                table: "Images");

            migrationBuilder.DropTable(
                name: "AppUserTicket");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_ClubId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Website",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "ClubId",
                table: "Images");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Images_TicketId",
                table: "Image",
                newName: "IX_Image_TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Image",
                table: "Image",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Tickets_TicketId",
                table: "Image",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
