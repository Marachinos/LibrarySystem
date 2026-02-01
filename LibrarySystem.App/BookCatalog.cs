using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.App;

public class BookCatalog
{
    private readonly List<Book> _books = new();

    public IReadOnlyList<Book> Books => _books.AsReadOnly();

    public void Add(Book book)
    {
        if (book is null) throw new ArgumentNullException(nameof(book));
        if (_books.Any(b => b.ISBN.Equals(book.ISBN, StringComparison.OrdinalIgnoreCase)))
            throw new InvalidOperationException("En bok med samma ISBN finns redan i katalogen.");

        _books.Add(book);
    }

    public Book? FindByIsbn(string isbn)
    {
        if (string.IsNullOrWhiteSpace(isbn)) return null;
        return _books.FirstOrDefault(b => b.ISBN.Equals(isbn, StringComparison.OrdinalIgnoreCase));
    }

    // Del 4: Sök via ISearchable
    public List<Book> Search(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm)) return new List<Book>();
        return _books.Where(b => b.Matches(searchTerm)).ToList();
    }

    // Del 4: Sortering
    public List<Book> SortByTitle()
        => _books.OrderBy(b => b.Title, StringComparer.OrdinalIgnoreCase).ToList();

    public List<Book> SortByPublishedYear(bool ascending = true)
        => ascending
            ? _books.OrderBy(b => b.PublishedYear).ToList()
            : _books.OrderByDescending(b => b.PublishedYear).ToList();
}
