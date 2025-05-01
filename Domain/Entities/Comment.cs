namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; } // Bağlı olduğu blog yazısı
        public int? ParentCommentId { get; set; } // Cevap olarak yazılmışsa
        public string UserId { get; set; } // Yorumu yazan kullanıcı ID
        public string Content { get; set; } // Yorum içeriği
        public DateTime CreatedAt { get; set; } // Yorum tarihi
        public bool IsApproved { get; set; } // Admin onaylı mı?
        public virtual Comment? ParentComment { get; set; } // ✅ Yorumun üst yorumunu (ParentComment)

        public virtual Post Post { get; set; } // Post ile ilişki
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>(); // Cevaplar
    }

}
