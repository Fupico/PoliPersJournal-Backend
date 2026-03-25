namespace Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostId { get; set; } // Bağlı olduğu blog yazısı
        public int? ParentCommentId { get; set; } // Cevap olarak yazılmışsa
        public string UserId { get; set; } = string.Empty; // Yorumu yazan kullanıcı ID
        public string Content { get; set; } = string.Empty; // Yorum içeriği
        public DateTime CreatedAt { get; set; } // Yorum tarihi
        public bool IsApproved { get; set; } // Admin onaylı mı?
        public virtual Comment? ParentComment { get; set; } // ✅ Yorumun üst yorumunu (ParentComment)

        public virtual Post Post { get; set; } = null!; // Post ile ilişki
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>(); // Cevaplar
    }

}
