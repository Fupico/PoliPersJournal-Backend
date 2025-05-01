using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstsss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Translations",
    columns: new[] { "Id", "LanguageCode", "Key", "Value" },
    values: new object[,]
    {
        { 19, "en", "categories.articles_in_category", "Articles in this Category:" },
        { 20, "en", "categories.related_articles", "Related Articles:" },
        { 21, "tr", "categories.articles_in_category", "Bu Kategorideki Makaleler:" },
        { 22, "tr", "categories.related_articles", "İlgili Makaleler:" },
        { 23, "de", "categories.articles_in_category", "Artikel in dieser Kategorie:" },
        { 24, "de", "categories.related_articles", "Verwandte Artikel:" }
    });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
