using System;
using LibrarySystem.App;
using Xunit;

namespace LibrarySystem.Tests
{
    public class NegativeAndEdgeCaseTests
    {
        [Theory]
        [InlineData("", "Title", "Author", 2020)]
        [InlineData("123", "", "Author", 2020)]
        [InlineData("123", "Title", "", 2020)]

        public void Book_Constructor_ShouldThrow_WhenRequiredFieldsMissing(string isbn, string title, string author, int year)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() => new Book(isbn, title, author, year));
        }

        [Fact]
        public void Catalog_Add_ShouldThrow_WhenDuplicateIsbn()
        {
            //Arrange
            var catalog = new BookCatalog();
            catalog.Add(new Book("ABC", "One", "A", 2000));

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            catalog.Add(new Book("ABC", "Two", "B", 2001)));
        }

        [Fact]
        public void Loan_ShouldThrow_WhenBookIsNotAvailable()
        {
            //Arrange
            var book = new Book("1", "T", "A", 2000);
            var m1 = new Member("M1", "A", "a@test.com");
            var m2 = new Member("M2", "B", "b@test.com");

            //lånar ut boken
            _ = new Loan(book, m1, DateTime.UtcNow, DateTime.UtcNow.AddDays(7));

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
            new Loan(book, m2, DateTime.UtcNow, DateTime.UtcNow.AddDays(7)));
        }

        [Fact]
        public void Loan_Return_ShouldThrow_WhenAlreadyReturned()
        {
            //Arrange
            var book = new Book("1", "T", "A", 2000);
            var member = new Member("M1", "A", "a@test.com");
            var loan = new Loan(book, member, DateTime.UtcNow, DateTime.UtcNow.AddDays(7));
            loan.Return();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => loan.Return());
        }

        [Fact]
        public void SearchBooks_ShouldReturnEmpty_WhenSearchTermIsWhitespace()
        {
            // Arrange
            var library = new Library();
            library.Catalog.Add(new Book("1", "Alpha", "Author", 2000));

            // Act
            var result = library.SearchBooks("   ");

            // Assert
            Assert.Empty(result);
        }
    }
}
