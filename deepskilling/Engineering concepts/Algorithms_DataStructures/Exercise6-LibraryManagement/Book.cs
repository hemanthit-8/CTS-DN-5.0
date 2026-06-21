namespace LibraryManagement;

/// <summary>
/// Represents a library book.
/// </summary>
public class Book
{
    public string BookId { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }

    public Book(string bookId, string title, string author)
    {
        BookId = bookId;
        Title = title;
        Author = author;
    }

    public override string ToString() => $"[{BookId}] \"{Title}\" by {Author}";
}
