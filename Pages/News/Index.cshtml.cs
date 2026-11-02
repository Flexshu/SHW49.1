using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using razor_pages_app.Models;
namespace razor_pages_app.Pages.News
{
    public class IndexModel : PageModel
    {
        private static List<News_>? storage;
        public List<News_> My_News { get; set; } = new();
        [BindProperty]
        public int NewsId { get; set; }
        [BindProperty]
        public string? NewsTitle { get; set; }
        [BindProperty]
        public string? NewsText { get; set; }
        [BindProperty]
        public string? CommentAuthor { get; set; }
        [BindProperty]
        public string? CommentText { get; set; }

        public void OnGet()
        {
            LoadNews();
        }
        public IActionResult OnPostAddNews()
        {
            LoadNews();
            if (string.IsNullOrWhiteSpace(NewsTitle) || string.IsNullOrWhiteSpace(NewsText))
            {
                return Page();
            }
            var new_news = new News_
            {
                Id = My_News.Count == 0 ? 1 : My_News.Max(n => n.Id) + 1,
                Title = NewsTitle.Trim(),
                Text = NewsText.Trim(),
                CreatedAt = DateTime.Now,
                Comments = new List<Comment>()
            };
            My_News.Insert(0, new_news);
            return RedirectToPage();
        }
        public IActionResult OnPostAddComment()
        {
            LoadNews();
            var news = My_News.FirstOrDefault(n => n.Id == NewsId);
            if (news == null || string.IsNullOrWhiteSpace(CommentAuthor) || string.IsNullOrWhiteSpace(CommentText))
            {
                return Page();
            }
            news.Comments ??= new List<Comment>();
            var new_comment = new Comment
            {
                Id = My_News.SelectMany(n => n.Comments ?? new List<Comment>()).Select(c => c.Id).DefaultIfEmpty(0).Max() + 1,
                Author = CommentAuthor.Trim(),
                Text = CommentText.Trim(),
                CreatedAt = DateTime.Now
            };
            news.Comments.Add(new_comment);
            return RedirectToPage();
        }
        private void LoadNews()
        {
            if (storage != null)
            {
                My_News = storage;
                return;
            }
            if(My_News.Count == 0)
            {
                // Створення списку унікальних новин
                My_News = new List<News_>
                {
                    new News_
                    {
                        Id = 1,
                        Title = "Штучний інтелект навчився розуміти емоції тварин",
                        Text = "Дослідники створили нову нейромережу, яка аналізує звуки та міміку домашніх улюбленців, визначаючи їхній точний настрій.",
                        CreatedAt = new DateTime(2026, 10, 2, 10, 0, 0),
                        Comments = new List<Comment>
                        {
                            new Comment { Id = 1, Author = "Олексій", Text = "Неймовірно! Тепер я нарешті дізнаюся, чому мій кіт постійно кричить зранку.", CreatedAt = new DateTime(2026, 10, 2, 10, 15, 0) },
                            new Comment { Id = 2, Author = "Марія", Text = "Головне, щоб вони не навчилися нас критикувати )", CreatedAt = new DateTime(2026, 10, 2, 11, 0, 0) }
                        }
                    },
                    new News_
                    {
                        Id = 2,
                        Title = "Презентовано перший екологічний квантовий комп'ютер",
                        Text = "Технологічний стартап представив обчислювальний процесор наступного покоління, який працює при кімнатній температурі та споживає в 10 разів менше енергії.",
                        CreatedAt = new DateTime(2026, 10, 1, 14, 30, 0),
                        Comments = new List<Comment>
                        {
                            new Comment { Id = 3, Author = "ТехноГік", Text = "Це справжній прорив для криптографії та зеленої енергетики.", CreatedAt = new DateTime(2026, 10, 1, 15, 0, 0) }
                        }
                    },
                    new News_
                    {
                        Id = 3,
                        Title = "В українських Карпатах відкрили новий туристичний еко-маршрут",
                        Text = "Маршрут обладнаний сонячними панелями для зарядки гаджетів, інтерактивними мапами та повністю інклюзивний для всіх категорій мандрівників.",
                        CreatedAt = new DateTime(2026, 9, 30, 9, 15, 0),
                        Comments = new List<Comment>() // Новина без коментарів
                    }
                };
            }
            storage = My_News;
        }
    }
}
