using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
    table: "Translations",
    columns: new[] { "Id", "LanguageCode", "Key", "Value" },
    values: new object[,]
    {
        // 🏳️‍🌈 English Translations
        { 1001, "en", "about.title", "About Us" },
        { 1002, "en", "about.subtitle", "Learn more about our mission and vision." },
        { 1003, "en", "about.companyTitle", "Company" },
        { 1004, "en", "about.companyDesc", "We are a company dedicated to innovation." },
        { 1005, "en", "about.missionTitle", "Our Mission" },
        { 1006, "en", "about.missionDesc", "To deliver the best services to our customers." },
        { 1007, "en", "about.visionTitle", "Our Vision" },
        { 1008, "en", "about.visionDesc", "To be the leading company in the industry." },
        { 1009, "en", "about.contactUs", "Contact Us" },

        // 🇹🇷 Turkish Translations
        { 1010, "tr", "about.title", "Hakkımızda" },
        { 1011, "tr", "about.subtitle", "Misyonumuz ve vizyonumuz hakkında daha fazla bilgi edinin." },
        { 1012, "tr", "about.companyTitle", "Şirket" },
        { 1013, "tr", "about.companyDesc", "Biz yenilikçi çözümler üreten bir firmayız." },
        { 1014, "tr", "about.missionTitle", "Misyonumuz" },
        { 1015, "tr", "about.missionDesc", "Müşterilerimize en iyi hizmeti sunmak." },
        { 1016, "tr", "about.visionTitle", "Vizyonumuz" },
        { 1017, "tr", "about.visionDesc", "Sektörde lider bir firma olmak." },
        { 1018, "tr", "about.contactUs", "Bize Ulaşın" },

        // 🇩🇪 German Translations
        { 1019, "de", "about.title", "Über uns" },
        { 1020, "de", "about.subtitle", "Erfahren Sie mehr über unsere Mission und Vision." },
        { 1021, "de", "about.companyTitle", "Unternehmen" },
        { 1022, "de", "about.companyDesc", "Wir sind ein innovatives Unternehmen." },
        { 1023, "de", "about.missionTitle", "Unsere Mission" },
        { 1024, "de", "about.missionDesc", "Wir bieten die besten Dienstleistungen für unsere Kunden." },
        { 1025, "de", "about.visionTitle", "Unsere Vision" },
        { 1026, "de", "about.visionDesc", "Wir wollen führend in der Branche sein." },
        { 1027, "de", "about.contactUs", "Kontaktieren Sie uns" }
    });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
