using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
      table: "PageTranslations",
      columns: new[] { "PageKey", "LanguageCode", "Title", "Subtitle" },
      values: new object[,]
      {
            { "education-and-society", "en", "Education Category", "Articles in the Education Category" },
            { "education-and-society", "tr", "Eğitim Kategorisi", "Eğitim Kategorisine Ait Yazılar" },
            { "education-and-society", "de", "Kategorie Bildung", "Artikel in der Kategorie Bildung" },

            { "philosophy-individual-and-society", "en", "On Philosophy, Individual and Society", "Essays on Philosophy, Individual and Society" },
            { "philosophy-individual-and-society", "tr", "Felsefe, Birey Ve Toplum Üzerine", "Felsefe, Birey Ve Toplum Üzerine Yazılar" },
            { "philosophy-individual-and-society", "de", "Über Philosophie, Individuum und Gesellschaft", "Essays zu Philosophie, Individuum und Gesellschaft" }
      });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
