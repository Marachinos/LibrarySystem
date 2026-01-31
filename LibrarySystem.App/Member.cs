using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LibrarySystem.App
{
    public class Member
    {
        private readonly List<Book> _borrowedBooks = new(); // privat lista för inkapsling

        public string MemberId { get; }                 // endast vid skapande
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime MemberSince { get; }

        // Read-only vy utåt (inkapsling)
        public IReadOnlyList<Book> BorrowedBooks => _borrowedBooks.AsReadOnly();

        public Member(string memberId, string name, string email, DateTime? memberSince = null)
        {
            if (string.IsNullOrWhiteSpace(memberId)) throw new ArgumentException("MemberId is required.", nameof(memberId));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email is required.", nameof(email));
            if (!email.Contains('@')) throw new ArgumentException("Email must contain '@'.", nameof(email));

            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = memberSince ?? DateTime.UtcNow; //sätts vid skapandet
        }

        public string GetInfo() //Metod för medlemsinfo
        => $"{Name} ({MemberId}) - {Email} - Member since: {MemberSince:yyyy-MM-dd} - Borrowed: {_borrowedBooks.Count}";

        // Dessa används av LoanManager senare (inte publikt "fritt fram" att ändra listan)
        public void AddBorrowedBook(Book book)
        {
            if (book is null) throw new ArgumentNullException(nameof(book));
            if (_borrowedBooks.Any(b => b.ISBN == book.ISBN))
                throw new InvalidOperationException("Book already borrowed by this member.");

            _borrowedBooks.Add(book);
        }

        public void RemoveBorrowedBook(Book book)
        {
            if (book is null) throw new ArgumentNullException(nameof(book));

            var removed = _borrowedBooks.Remove(book);
            if (!removed)
                throw new InvalidOperationException("Book was not borrowed by this member.");
        }
    }
}
