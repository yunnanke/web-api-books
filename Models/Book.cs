namespace API_Books.api.Models
{
    public class Book
    {
        public int ID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public int Pages { get; set; }
    }
}
