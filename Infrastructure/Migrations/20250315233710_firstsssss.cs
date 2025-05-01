using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class firstsssss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
    table: "Translations",
    columns: new[] { "Id", "LanguageCode", "Key", "Value" },
    values: new object[,]
    {
        // **İngilizce (English)**
        { 52, "en", "contact.title", "Contact Us" },
        { 53, "en", "contact.subtitle", "Get in touch with us!" },
        { 54, "en", "contact.address", "Address" },
        { 55, "en", "contact.addressDetail", "Istanbul, Türkiye" },
        { 56, "en", "contact.email", "Email" },
        { 57, "en", "contact.emailAddress", "melis@polipersjournal.com" },
        { 58, "en", "contact.viewOnMap", "View on Google Maps" },
        { 59, "en", "contact.sendMessage", "Send a Message" },
        { 60, "en", "contact.messageSubject", "Subject" },
        { 61, "en", "contact.senderName", "Your Name" },
        { 62, "en", "contact.senderContact", "Your Email or Phone" },
        { 63, "en", "contact.messageContent", "Your Message" },
        { 64, "en", "contact.cancel", "Cancel" },
        { 65, "en", "contact.send", "Send" },
        { 66, "en", "contact.fillAllFields", "Please fill in all fields." },
        { 67, "en", "contact.messageSent", "Your message has been sent successfully!" },

        // **Türkçe (Turkish)**
        { 68, "tr", "contact.title", "Bize Ulaşın" },
        { 69, "tr", "contact.subtitle", "Bizimle iletişime geçin!" },
        { 70, "tr", "contact.address", "Adres" },
        { 71, "tr", "contact.addressDetail", "İstanbul, Türkiye" },
        { 72, "tr", "contact.email", "E-Posta" },
        { 73, "tr", "contact.emailAddress", "melis@polipersjournal.com" },
        { 74, "tr", "contact.viewOnMap", "Google Haritalar'da Görüntüle" },
        { 75, "tr", "contact.sendMessage", "Mesaj Gönder" },
        { 76, "tr", "contact.messageSubject", "Konu" },
        { 77, "tr", "contact.senderName", "Adınız" },
        { 78, "tr", "contact.senderContact", "E-Posta veya Telefon" },
        { 79, "tr", "contact.messageContent", "Mesajınız" },
        { 80, "tr", "contact.cancel", "İptal" },
        { 81, "tr", "contact.send", "Gönder" },
        { 82, "tr", "contact.fillAllFields", "Lütfen tüm alanları doldurun." },
        { 83, "tr", "contact.messageSent", "Mesajınız başarıyla gönderildi!" },

        // **Almanca (German)**
        { 84, "de", "contact.title", "Kontakt" },
        { 85, "de", "contact.subtitle", "Kontaktieren Sie uns!" },
        { 86, "de", "contact.address", "Adresse" },
        { 87, "de", "contact.addressDetail", "Istanbul, Türkei" },
        { 88, "de", "contact.email", "E-Mail" },
        { 89, "de", "contact.emailAddress", "melis@polipersjournal.com" },
        { 90, "de", "contact.viewOnMap", "Auf Google Maps anzeigen" },
        { 91, "de", "contact.sendMessage", "Nachricht senden" },
        { 92, "de", "contact.messageSubject", "Betreff" },
        { 93, "de", "contact.senderName", "Ihr Name" },
        { 94, "de", "contact.senderContact", "Ihre E-Mail oder Telefonnummer" },
        { 95, "de", "contact.messageContent", "Ihre Nachricht" },
        { 96, "de", "contact.cancel", "Abbrechen" },
        { 97, "de", "contact.send", "Senden" },
        { 98, "de", "contact.fillAllFields", "Bitte füllen Sie alle Felder aus." },
        { 99, "de", "contact.messageSent", "Ihre Nachricht wurde erfolgreich gesendet!" }
    }
);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
