using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 📌 Languages tablosuna Türkçe, İngilizce ve Almanca ekleme
            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Code", "Name", "IsActive" },
                values: new object[,]
                {
            { "tr", "Türkçe", true },
            { "en", "English", true },
            { "de", "Deutsch", true }
                });

            // 📌 Categories tablosuna İngilizce isimleri ve ikonları ekleyelim
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Slug", "Icon" },
                values: new object[,]
                {
        { 1, "education-and-society", "school" },
        { 2, "environmental-movements-and-activism", "eco" },
        { 3, "human-rights-and-justice", "gavel" },
        { 4, "gender-and-feminism", "female" },
        { 5, "economy-and-international-trade", "monetization_on" },
        { 6, "politics-and-international-relations", "public" },
        { 7, "philosophy-individual-and-society", "psychology" },
        { 8, "sociology-individual-and-society", "groups" },
        { 9, "globalization-and-cultural-change", "language" }
                });

            // 📌 CategoryTranslations tablosuna Türkçe, Almanca ve İngilizce çevirileri ekleyelim
            migrationBuilder.InsertData(
                table: "CategoryTranslations",
                columns: new[] { "Id", "CategoryId", "LanguageCode", "Name" },
                values: new object[,]
                {
            // Türkçe Çeviriler
            { 1, 1, "tr", "Eğitim ve Toplum" },
            { 3, 2, "tr", "Çevreci Hareketler ve Aktivizm" },
            { 5, 3, "tr", "İnsan Hakları ve Adalet" },
            { 7, 4, "tr", "Toplumsal Cinsiyet ve Feminizm" },
            { 9, 5, "tr", "Ekonomi ve Uluslararası Ticaret" },
            { 11, 6, "tr", "Siyaset ve Uluslararası İlişkiler" },
            { 13, 7, "tr", "Felsefe, Birey ve Toplum" },
            { 15, 8, "tr", "Sosyoloji, Birey ve Toplum" },
            { 17, 9, "tr", "Küreselleşme ve Kültürel Değişim" },

            // Almanca Çeviriler
            { 2, 1, "de", "Bildung und Gesellschaft" },
            { 4, 2, "de", "Umweltbewegungen und Aktivismus" },
            { 6, 3, "de", "Menschenrechte und Gerechtigkeit" },
            { 8, 4, "de", "Geschlecht und Feminismus" },
            { 10, 5, "de", "Wirtschaft und internationaler Handel" },
            { 12, 6, "de", "Politik und internationale Beziehungen" },
            { 14, 7, "de", "Philosophie, Individuum und Gesellschaft" },
            { 16, 8, "de", "Soziologie, Individuum und Gesellschaft" },
            { 18, 9, "de", "Globalisierung und kultureller Wandel" },

            // İngilizce Çeviriler
            { 19, 1, "en", "Education and Society" },
            { 20, 2, "en", "Environmental Movements and Activism" },
            { 21, 3, "en", "Human Rights and Justice" },
            { 22, 4, "en", "Gender and Feminism" },
            { 23, 5, "en", "Economy and International Trade" },
            { 24, 6, "en", "Politics and International Relations" },
            { 25, 7, "en", "Philosophy, Individual and Society" },
            { 26, 8, "en", "Sociology, Individual and Society" },
            { 27, 9, "en", "Globalization and Cultural Change" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
