using Microsoft.EntityFrameworkCore.Migrations;

namespace AeroCar.Migrations
{
    public partial class addedfriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friend_UserProfiles_UserProfileId",
                table: "Friend");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friend",
                table: "Friend");

            migrationBuilder.RenameTable(
                name: "Friend",
                newName: "Friends");

            migrationBuilder.RenameIndex(
                name: "IX_Friend_UserProfileId",
                table: "Friends",
                newName: "IX_Friends_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friends",
                table: "Friends",
                column: "RelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_UserProfiles_UserProfileId",
                table: "Friends",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_UserProfiles_UserProfileId",
                table: "Friends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Friends",
                table: "Friends");

            migrationBuilder.RenameTable(
                name: "Friends",
                newName: "Friend");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_UserProfileId",
                table: "Friend",
                newName: "IX_Friend_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Friend",
                table: "Friend",
                column: "RelationshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friend_UserProfiles_UserProfileId",
                table: "Friend",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "UserProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
