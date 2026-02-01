using System;
using System.Collections.Generic;

namespace LibrarySystem.App;

public class Library
{
    public BookCatalog Catalog { get; } = new();
    public MemberRegistry Members { get; } = new();
    public LoanManager Loans { get; } = new();

    // Del 4: Statistik
    public int GetTotalBooks() => Catalog.Books.Count;

    public int GetBorrowedBooksCount() => Loans.GetBorrowedBooksCount();

    public Member? GetMostActiveBorrower() => Loans.GetMostActiveBorrower();

    // Del 4: Sök
    public List<Book> SearchBooks(string searchTerm) => Catalog.Search(searchTerm);

    // Del 4: Sortering
    public List<Book> SortBooksByTitle() => Catalog.SortByTitle();

    public List<Book> SortBooksByPublishedYear(bool ascending = true) => Catalog.SortByPublishedYear(ascending);
}
