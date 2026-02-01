using System;
using System.Collections.Generic;
using System.Text;

namespace LibrarySystem.App
{
    public class Loan
    {
        public Book Book { get; }
        public Member Member { get; }

        public DateTime LoanDate { get; }
        public DateTime DueDate { get; }
        public DateTime? ReturnDate { get; private set; }

        //Beräknad property för att avgöra om lånet är försenat (utlånad men inte återlämnad och förfallodatum har passerat)
        public bool IsOverdue => !ReturnDate.HasValue && DateTime.UtcNow > DueDate;

        //Beräknad property för att avgöra om boken har återlämnats
        public bool IsReturned => ReturnDate.HasValue;

        public Loan(Book book, Member member, DateTime loanDate, DateTime duedate)
        { 
            Book = book ?? throw new ArgumentNullException(nameof(book));
            Member = member ?? throw new ArgumentNullException(nameof(member));

            if (duedate <= loanDate)
                throw new ArgumentException("Återlämningsdatum måste vara efter Utlåningsdatum.");

            LoanDate = loanDate;
            DueDate = duedate;

            if (!book.IsAvailable)
                throw new InvalidOperationException("Boken är inte tillgänglig för utlåning.");

            //koppla ihop boken med lånet
            Book.MarkAsBorrowed();
            Member.AddBorrowedBook(Book);
        }

        //Metod för att återlämna boken
        public void Return(DateTime? returnDate = null)
        {
            if (IsReturned)
                throw new InvalidOperationException("Boken har redan återlämnats.");

            ReturnDate = returnDate ?? DateTime.UtcNow;

            //koppla bort boken från lånet
            Book.MarkAsReturned();
            Member.RemoveBorrowedBook(Book);
        }
    }
}
