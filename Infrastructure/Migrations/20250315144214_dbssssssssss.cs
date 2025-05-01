using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbssssssssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostDownloadTracking_PostTranslations_PostTranslationId",
                table: "PostDownloadTracking");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDownloadTracking_Posts_PostId",
                table: "PostDownloadTracking");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostDownloadTracking",
                table: "PostDownloadTracking");

            migrationBuilder.RenameTable(
                name: "PostDownloadTracking",
                newName: "PostDownloadTrackings");

            migrationBuilder.RenameIndex(
                name: "IX_PostDownloadTracking_PostTranslationId",
                table: "PostDownloadTrackings",
                newName: "IX_PostDownloadTrackings_PostTranslationId");

            migrationBuilder.RenameIndex(
                name: "IX_PostDownloadTracking_PostId",
                table: "PostDownloadTrackings",
                newName: "IX_PostDownloadTrackings_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostDownloadTrackings",
                table: "PostDownloadTrackings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDownloadTrackings_PostTranslations_PostTranslationId",
                table: "PostDownloadTrackings",
                column: "PostTranslationId",
                principalTable: "PostTranslations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostDownloadTrackings_Posts_PostId",
                table: "PostDownloadTrackings",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostDownloadTrackings_PostTranslations_PostTranslationId",
                table: "PostDownloadTrackings");

            migrationBuilder.DropForeignKey(
                name: "FK_PostDownloadTrackings_Posts_PostId",
                table: "PostDownloadTrackings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostDownloadTrackings",
                table: "PostDownloadTrackings");

            migrationBuilder.RenameTable(
                name: "PostDownloadTrackings",
                newName: "PostDownloadTracking");

            migrationBuilder.RenameIndex(
                name: "IX_PostDownloadTrackings_PostTranslationId",
                table: "PostDownloadTracking",
                newName: "IX_PostDownloadTracking_PostTranslationId");

            migrationBuilder.RenameIndex(
                name: "IX_PostDownloadTrackings_PostId",
                table: "PostDownloadTracking",
                newName: "IX_PostDownloadTracking_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostDownloadTracking",
                table: "PostDownloadTracking",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostDownloadTracking_PostTranslations_PostTranslationId",
                table: "PostDownloadTracking",
                column: "PostTranslationId",
                principalTable: "PostTranslations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostDownloadTracking_Posts_PostId",
                table: "PostDownloadTracking",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
