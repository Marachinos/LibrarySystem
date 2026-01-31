using System;
using System.Collections.Generic;
using System.Text;

//required betyder nödvändig = Not to my self 

namespace LibrarySystem.App
{
    public class Book : ISearchable
    {
        public string ISBN { get; }                 // endast sättas vid skapande
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int PublishedYear { get; private set; }
        public bool IsAvailable { get; private set; }

        public Book(string isbn, string title, string author, int publishedYear)
        {
            if (string.IsNullOrWhiteSpace(isbn)) throw new ArgumentException("ISBN is required.", nameof(isbn));
            if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title is required.", nameof(title));
            if (string.IsNullOrWhiteSpace(author)) throw new ArgumentException("Author is required.", nameof(author));
            if (publishedYear <= 0) throw new ArgumentOutOfRangeException(nameof(publishedYear), "PublishedYear must be positive.");

            ISBN = isbn;
            Title = title;
            Author = author;
            PublishedYear = publishedYear;
            IsAvailable = true; // ny bok är tillgänglig
        }

        public string GetInfo()
            => $"{Title} by {Author} ({PublishedYear}) - ISBN: {ISBN} - {(IsAvailable ? "Available" : "On loan")}";

        // För LoanManager senare:
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
}
