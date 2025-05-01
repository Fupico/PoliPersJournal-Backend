using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dbsssssssssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Tags",
    columns: new[] { "Id", "Slug" },
    values: new object[,]
    {
        { 1, "technology" },
        { 2, "education" },
        { 3, "health" },
        { 4, "science" },
        { 5, "economy" },
        { 6, "environment" },
        { 7, "politics" },
        { 8, "society" },
        { 9, "culture" },
        { 10, "law" }
    });

            migrationBuilder.InsertData(
    table: "TagTranslations",
    columns: new[] { "Id", "TagId", "LanguageCode", "Name" },
    values: new object[,]
    {
        // 📌 Technology
        { 1, 1, "en", "Technology" },
        { 2, 1, "tr", "Teknoloji" },
        { 3, 1, "de", "Technologie" },

        // 📌 Education
        { 4, 2, "en", "Education" },
        { 5, 2, "tr", "Eğitim" },
        { 6, 2, "de", "Bildung" },

        // 📌 Health
        { 7, 3, "en", "Health" },
        { 8, 3, "tr", "Sağlık" },
        { 9, 3, "de", "Gesundheit" },

        // 📌 Science
        { 10, 4, "en", "Science" },
        { 11, 4, "tr", "Bilim" },
        { 12, 4, "de", "Wissenschaft" },

        // 📌 Economy
        { 13, 5, "en", "Economy" },
        { 14, 5, "tr", "Ekonomi" },
        { 15, 5, "de", "Wirtschaft" },

        // 📌 Environment
        { 16, 6, "en", "Environment" },
        { 17, 6, "tr", "Çevre" },
        { 18, 6, "de", "Umwelt" },

        // 📌 Politics
        { 19, 7, "en", "Politics" },
        { 20, 7, "tr", "Siyaset" },
        { 21, 7, "de", "Politik" },

        // 📌 Society
        { 22, 8, "en", "Society" },
        { 23, 8, "tr", "Toplum" },
        { 24, 8, "de", "Gesellschaft" },

        // 📌 Culture
        { 25, 9, "en", "Culture" },
        { 26, 9, "tr", "Kültür" },
        { 27, 9, "de", "Kultur" },

        // 📌 Law
        { 28, 10, "en", "Law" },
        { 29, 10, "tr", "Hukuk" },
        { 30, 10, "de", "Recht" }
    });

            // 📌 Post Verilerini Ekleyelim
            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Slug", "IsPublished", "CreatedAt", "CreatedBy", "MetaTitle", "MetaDescription", "MetaKeywords", "CoverImageUrl", "ViewCount", "AllowComments" },
                values: new object[,]
                {
            { 1, "future-of-education", true, DateTime.UtcNow.AddDays(-10), 1, "The Future of Education", "An analysis of future trends in education.", "education, future, learning", "/images/education.jpg", 50, true },
            { 2, "philosophy-of-mind", true, DateTime.UtcNow.AddDays(-5), 1, "Philosophy of Mind", "Exploring fundamental questions of consciousness.", "philosophy, mind, consciousness", "/images/philosophy.jpg", 35, true }
                });

            // 📌 Post Çeviri Verilerini Ekleyelim
            migrationBuilder.InsertData(
                table: "PostTranslations",
                columns: new[] { "Id", "PostId", "LanguageCode", "Title", "Content", "Summary", "MetaTitle", "MetaDescription", "MetaKeywords", "PdfUrl", "IsPublished", "CreatedAt" },
                values: new object[,]
                {
            { 1, 1, "en", "The Future of Education", "Education is evolving with technology.", "A look into the future of education.", "The Future of Education", "An in-depth analysis of education.", "education, future, learning", "/pdfs/future-of-education-en.pdf", true, DateTime.UtcNow },
            { 2, 1, "tr", "Eğitimin Geleceği", "Eğitim teknolojilerle değişiyor.", "Eğitimin geleceğine bakış.", "Eğitimin Geleceği", "Eğitimde gelecekteki eğilimler.", "eğitim, gelecek, öğrenme", "/pdfs/future-of-education-tr.pdf", true, DateTime.UtcNow },
            { 3, 2, "en", "Philosophy of Mind", "Exploring questions of consciousness.", "Understanding the mind.", "Philosophy of Mind", "Exploring human consciousness.", "philosophy, mind, consciousness", "/pdfs/philosophy-of-mind-en.pdf", true, DateTime.UtcNow },
            { 4, 2, "tr", "Zihin Felsefesi", "Bilinç sorularını keşfetmek.", "Zihin nasıl çalışır?", "Zihin Felsefesi", "İnsan bilincini anlamak.", "felsefe, zihin, bilinç", "/pdfs/philosophy-of-mind-tr.pdf", true, DateTime.UtcNow }
                });

            // 📌 Post Kategori İlişkileri Ekleyelim
            migrationBuilder.InsertData(
                table: "PostCategories",
                columns: new[] { "PostId", "CategoryId" },
                values: new object[,]
                {
            { 1, 1 }, // 📌 Future of Education -> Education & Society
            { 2, 7 }  // 📌 Philosophy of Mind -> Philosophy, Individual and Society
                });

            // 📌 Post Etiket İlişkileri Ekleyelim
            migrationBuilder.InsertData(
                table: "PostTags",
                columns: new[] { "PostId", "TagId" },
                values: new object[,]
                {
            { 1, 1 }, // 📌 Future of Education -> Tag 1
            { 1, 2 }, // 📌 Future of Education -> Tag 2
            { 2, 3 }  // 📌 Philosophy of Mind -> Tag 3
                });

            // 📌 PDF İndirme Takibi İçin Örnek Veri Ekleyelim
            migrationBuilder.InsertData(
                table: "PostDownloadTrackings",
                columns: new[] { "Id", "PostTranslationId", "DownloadedAt", "UserIp", "UserAgent" },
                values: new object[,]
                {
            { 1, 1, DateTime.UtcNow, "192.168.1.10", "Mozilla/5.0" },
            { 2, 2, DateTime.UtcNow, "192.168.1.15", "Chrome/120.0" },
            { 3, 3, DateTime.UtcNow, "192.168.1.20", "Safari/537.36" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
