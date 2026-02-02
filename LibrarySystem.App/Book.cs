using System;

namespace LibrarySystem.App;

public class Book : ISearchable
{
    public string ISBN { get; }                 // endast sättas vid skapande
    public string Title { get; private set; }
    public string Author { get; private set; }
    public int PublishedYear { get; private set; }
    public bool IsAvailable { get; private set; }

    public Book(string isbn, string title, string author, int publishedYear)
    {
        if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentException("ISBN är obligatorisk.", nameof(isbn));
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Titel är obligatorisk.", nameof(title));
        if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Författare är obligatorisk.", nameof(author));
        if (publishedYear <= 0) throw new ArgumentOutOfRangeException(nameof(publishedYear), "Publiceringsåret måste vara ett år högre än 0.");

        ISBN = isbn;
        Title = title;
        Author = author;
        PublishedYear = publishedYear;
        IsAvailable = true; // ny bok är tillgänglig
    }

    public string GetInfo()
        => $"{Title} by {Author} ({PublishedYear}) - ISBN: {ISBN} - {(IsAvailable ? "Available" : "Borrowed")}";

    // För LoanManager:
    public void MarkAsBorrowed() => IsAvailable = false;
    public void MarkAsReturned() => IsAvailable = true;

    public bool Matches(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return false;

        return ISBN.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
            || Author.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
    }
}
