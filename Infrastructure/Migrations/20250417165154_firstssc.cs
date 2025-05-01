using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstssc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAuthor_AspNetUsers_AuthorId",
                table: "PostAuthor");

            migrationBuilder.DropForeignKey(
                name: "FK_PostAuthor_Posts_PostId",
                table: "PostAuthor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostAuthor",
                table: "PostAuthor");

            migrationBuilder.RenameTable(
                name: "PostAuthor",
                newName: "PostAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_PostAuthor_AuthorId",
                table: "PostAuthors",
                newName: "IX_PostAuthors_AuthorId");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostAuthors",
                table: "PostAuthors",
                columns: new[] { "PostId", "AuthorId" });

            migrationBuilder.CreateTable(
                name: "PostSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostTranslationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostSections_PostTranslations_PostTranslationId",
                        column: x => x.PostTranslationId,
                        principalTable: "PostTranslations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostSections_PostTranslationId",
                table: "PostSections",
                column: "PostTranslationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostAuthors_AspNetUsers_AuthorId",
                table: "PostAuthors",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostAuthors_Posts_PostId",
                table: "PostAuthors",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostAuthors_AspNetUsers_AuthorId",
                table: "PostAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_PostAuthors_Posts_PostId",
                table: "PostAuthors");

            migrationBuilder.DropTable(
                name: "PostSections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostAuthors",
                table: "PostAuthors");

            migrationBuilder.RenameTable(
                name: "PostAuthors",
                newName: "PostAuthor");

            migrationBuilder.RenameIndex(
                name: "IX_PostAuthors_AuthorId",
                table: "PostAuthor",
                newName: "IX_PostAuthor_AuthorId");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                table: "Posts",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostAuthor",
                table: "PostAuthor",
                columns: new[] { "PostId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostAuthor_AspNetUsers_AuthorId",
                table: "PostAuthor",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostAuthor_Posts_PostId",
                table: "PostAuthor",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
