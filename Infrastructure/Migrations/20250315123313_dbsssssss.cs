using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbsssssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
       table: "PageTranslations",
       columns: new[] { "PageKey", "LanguageCode", "Title", "Subtitle" },
       values: new object[,]
       {
            { "categories", "en", "Categories", "Browse through different categories" },
            { "categories", "tr", "Kategoriler", "Farklı kategoriler arasında gezinin" },
            { "categories", "de", "Kategorien", "Durch verschiedene Kategorien stöbern" },

            { "post-detail", "en", "Post Detail", "Read the full article" },
            { "post-detail", "tr", "Makale Detayı", "Tam makaleyi okuyun" },
            { "post-detail", "de", "Beitragsdetails", "Lesen Sie den vollständigen Artikel" }
       });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
