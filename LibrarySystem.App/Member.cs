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
            if (string.IsNullOrWhiteSpace(memberId)) throw new ArgumentException("MedlemsId är obligatoriskt.", nameof(memberId));
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Namn är obligatoriskt.", nameof(name));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("E-mail är obligatoriskt.", nameof(email));
            if (!email.Contains('@')) throw new ArgumentException("E-mail måste innehålla '@'.", nameof(email));

            MemberId = memberId;
            Name = name;
            Email = email;
            MemberSince = memberSince ?? DateTime.UtcNow; //sätts vid skapandet
        }

        public string GetInfo() //Metod för medlemsinfo
        => $"{Name} ({MemberId}) - {Email} - Medlem sedan: {MemberSince:yyyy-MM-dd} - Lånat: {_borrowedBooks.Count}";

        //Dessa används av LoanManager senare (inte publikt "fritt fram" att ändra listan)
        public void AddBorrowedBook(Book book)
        {
            if (book is null) throw new ArgumentNullException(nameof(book));
            if (_borrowedBooks.Any(b => b.ISBN == book.ISBN))
                throw new InvalidOperationException("Boken är redan lånad av den här medlemmen.");

            _borrowedBooks.Add(book);
        }

        public void RemoveBorrowedBook(Book book)
        {
            if (book is null) throw new ArgumentNullException(nameof(book));

            var removed = _borrowedBooks.Remove(book);
            if (!removed)
                throw new InvalidOperationException("Boken var inte lånad av den här medlemmen.");
        }
    }
}
