using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySystem.App;

public class LoanManager
{
    private readonly List<Loan> _loans = new();

    public IReadOnlyList<Loan> Loans => _loans.AsReadOnly();

    public Loan CreateLoan(Book book, Member member, DateTime? loanDate = null, int days = 14)
    {
        if (book is null) throw new ArgumentNullException(nameof(book));
        if (member is null) throw new ArgumentNullException(nameof(member));
        if (days <= 0) throw new ArgumentOutOfRangeException(nameof(days), "Dagarna måste vara högre än 0.");

        var ld = loanDate ?? DateTime.UtcNow;
        var due = ld.AddDays(days);

        var loan = new Loan(book, member, ld, due);
        _loans.Add(loan);
        return loan;
    }

    public void ReturnLoan(Loan loan, DateTime? returnDate = null)
    {
        if (loan is null) throw new ArgumentNullException(nameof(loan));
        loan.Return(returnDate);
    }

    // Del 4: Statistik
    public int GetBorrowedBooksCount()
        => _loans.Count(l => !l.IsReturned);

    public Member? GetMostActiveBorrower()
    {
        // “Mest aktiva låntagaren” = flest lån (historiskt, inklusive återlämnade)
        return _loans
            .GroupBy(l => l.Member)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key.MemberId, StringComparer.OrdinalIgnoreCase)
            .Select(g => g.Key)
            .FirstOrDefault();
    }
}
