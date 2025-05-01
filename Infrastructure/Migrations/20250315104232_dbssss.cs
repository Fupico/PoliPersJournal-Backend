using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Translations",
    columns: new[] { "Id", "LanguageCode", "Key", "Value" },
    values: new object[,]
    {
        { 1, "en", "sidebarMenu.home", "Home" },
        { 2, "tr", "sidebarMenu.home", "Ana Sayfa" },
        { 3, "de", "sidebarMenu.home", "Startseite" },

        { 4, "en", "sidebarMenu.about", "About" },
        { 5, "tr", "sidebarMenu.about", "Hakkımızda" },
        { 6, "de", "sidebarMenu.about", "Über uns" },

        { 7, "en", "sidebarMenu.submitArticle", "Submit Article" },
        { 8, "tr", "sidebarMenu.submitArticle", "Makale Gönder" },
        { 9, "de", "sidebarMenu.submitArticle", "Artikel Einreichen" },

        { 10, "en", "sidebarMenu.articles", "Articles" },
        { 11, "tr", "sidebarMenu.articles", "Makaleler" },
        { 12, "de", "sidebarMenu.articles", "Artikel" },

        { 13, "en", "sidebarMenu.categories", "Categories" },
        { 14, "tr", "sidebarMenu.categories", "Kategoriler" },
        { 15, "de", "sidebarMenu.categories", "Kategorien" },

        { 16, "en", "sidebarMenu.contact", "Contact" },
        { 17, "tr", "sidebarMenu.contact", "İletişim" },
        { 18, "de", "sidebarMenu.contact", "Kontakt" }
    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
