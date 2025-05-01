using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // 🌍 Genel Veriler
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Setting> Settings { get; set; }

        // 📌 Blog Veritabanı Tabloları
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostTranslation> PostTranslations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagTranslation> TagTranslations { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        // 🌎 Dil Yönetimi
        public DbSet<Language> Languages { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<PageTranslation> PageTranslations { get; set; }
        // ✅ **PostDownloadTracking Tablosunu Ekleyelim**
        public DbSet<PostDownloadTracking> PostDownloadTrackings { get; set; } // 🆕 **Eksik Olan Kısım**
        public DbSet<PostSection> PostSections { get; set; } // 🆕 **Eksik Olan Kısım**
        public DbSet<PostAuthor> PostAuthors { get; set; } // 🆕 **Eksik Olan Kısım**
        public DbSet<ApplicationUserTranslation> ApplicationUserTranslations { get; set; } // 🆕 **Eksik Olan Kısım**


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Identity tablolarını varsayılan haliyle bırakıyoruz.

            #region 🔹 Blog Veritabanı İlişkileri

            // 📌 Makale - Çeviri ilişkisi (1 Post -> Çok Dilli İçerik)
            builder.Entity<PostTranslation>()
                .HasOne(p => p.Post)
                .WithMany(p => p.PostTranslations)
                .HasForeignKey(p => p.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📌 Makale - Kategori ilişkisi (Çoka-Çok)
            builder.Entity<PostCategory>()
                .HasKey(pc => new { pc.PostId, pc.CategoryId });
            // 📌 PostCategory için Foreign Key ilişkisini açıkça tanımla

            builder.Entity<PostCategory>()
               .HasOne(pc => pc.Post)
               .WithMany(p => p.PostCategories)
               .HasForeignKey(pc => pc.PostId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PostCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📌 Makale - Etiket ilişkisi (Çoka-Çok)
            builder.Entity<PostTag>()
                .HasKey(pt => new { pt.PostId, pt.TagId });

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.Tags)
                .HasForeignKey(pt => pt.PostId);

            builder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany()
                .HasForeignKey(pt => pt.TagId);

            // 📌 Kategori - Çeviri ilişkisi (1 Kategori -> Çok Dilli Ad)
            builder.Entity<CategoryTranslation>()
                .HasOne(ct => ct.Category)
                .WithMany(c => c.Translations)
                .HasForeignKey(ct => ct.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📌 Etiket - Çeviri ilişkisi (1 Etiket -> Çok Dilli Ad)
            builder.Entity<TagTranslation>()
                .HasOne(tt => tt.Tag)
                .WithMany(t => t.Translations)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📌 Yorum - Makale ilişkisi (1 Makale -> Çok Yorum)
            builder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            // 📌 Yorum - Kullanıcı ilişkisi (1 Kullanıcı -> Çok Yorum)
            builder.Entity<Comment>()
                .HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
          
            builder.Entity<Comment>()
    .HasOne(c => c.ParentComment)
    .WithMany(c => c.Replies)
    .HasForeignKey(c => c.ParentCommentId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PostAuthor>()
    .HasKey(pa => new { pa.PostId, pa.AuthorId });

            builder.Entity<PostAuthor>()
                .HasOne(pa => pa.Post)
                .WithMany(p => p.Authors)
                .HasForeignKey(pa => pa.PostId);

            builder.Entity<PostAuthor>()
                .HasOne(pa => pa.Author)
                .WithMany(a => a.AuthoredPosts)
                .HasForeignKey(pa => pa.AuthorId);

            #endregion

            #region 🔹 Dil Yönetimi

            // 📌 Genel çeviri tabloları için ilişkiler
            builder.Entity<Translation>()
                .HasIndex(t => new { t.LanguageCode, t.Key }) // Aynı dilde aynı anahtar tekrar eklenemez
                .IsUnique();

            #endregion
        }
    }
}
