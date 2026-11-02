namespace razor_pages_app.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        public string? Text { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
