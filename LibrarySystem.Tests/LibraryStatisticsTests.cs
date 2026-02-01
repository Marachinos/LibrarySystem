using System;
using System.Linq;
using LibrarySystem.App;
using Xunit;

namespace LibrarySystem.Tests;

public class LibraryStatisticsTests
{
    [Fact]
    public void GetTotalBooks_ShouldReturnCorrectCount()
    {
        // Arrange
        var library = new Library();
        library.Catalog.Add(new Book("1", "A", "Auth", 2000));
        library.Catalog.Add(new Book("2", "B", "Auth", 2001));

        // Act
        var total = library.GetTotalBooks();

        // Assert
        Assert.Equal(2, total);
    }

    [Fact]
    public void GetBorrowedBooksCount_ShouldReturnCorrectCount()
    {
        // Arrange
        var library = new Library();
        var member = new Member("M1", "Member", "m@test.com");
        library.Members.Add(member);

        var b1 = new Book("1", "A", "Auth", 2000);
        var b2 = new Book("2", "B", "Auth", 2001);
        library.Catalog.Add(b1);
        library.Catalog.Add(b2);

        library.Loans.CreateLoan(b1, member); // 1 aktivt lån

        // Act
        var borrowed = library.GetBorrowedBooksCount();

        // Assert
        Assert.Equal(1, borrowed);
    }

    [Fact]
    public void GetMostActiveBorrower_ShouldReturnMemberWithMostLoans()
    {
        // Arrange
        var library = new Library();

        var m1 = new Member("M1", "A", "a@test.com");
        var m2 = new Member("M2", "B", "b@test.com");
        library.Members.Add(m1);
        library.Members.Add(m2);

        var b1 = new Book("1", "Bok1", "Auth", 2000);
        var b2 = new Book("2", "Bok2", "Auth", 2001);
        var b3 = new Book("3", "Bok3", "Auth", 2002);
        library.Catalog.Add(b1);
        library.Catalog.Add(b2);
        library.Catalog.Add(b3);

        var l1 = library.Loans.CreateLoan(b1, m1);
        library.Loans.ReturnLoan(l1);

        var l2 = library.Loans.CreateLoan(b2, m1);
        library.Loans.ReturnLoan(l2);

        library.Loans.CreateLoan(b3, m2); // m2 har 1 lån

        // Act
        var mostActive = library.GetMostActiveBorrower();

        // Assert
        Assert.NotNull(mostActive);
        Assert.Equal("M1", mostActive!.MemberId);
    }

    [Fact]
    public void SortBooksByTitle_ShouldReturnAlphabeticalOrder()
    {
        // Arrange
        var library = new Library();
        library.Catalog.Add(new Book("1", "Örn", "Auth", 2000));
        library.Catalog.Add(new Book("2", "Alpha", "Auth", 2000));
        library.Catalog.Add(new Book("3", "beta", "Auth", 2000));

        // Act
        var sorted = library.SortBooksByTitle();

        // Assert (case-insensitive sort)
        Assert.Equal(new[] { "Alpha", "beta", "Örn" }, sorted.Select(b => b.Title).ToArray());
    }
}
