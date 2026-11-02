namespace razor_pages_app.Models
{
    public class News_
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
