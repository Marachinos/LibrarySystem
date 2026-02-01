using System;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.App;
using Xunit;

namespace LibrarySystem.Tests
{
    public class LoanTests
    {
        [Fact]
        public void IsOverdue_ShouldReturnFalse_WhenDueDateIsInFuture()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");

            var loanDate = DateTime.UtcNow;
            var dueDate = loanDate.AddDays(14);

            // Act
            var loan = new Loan(book, member, loanDate, dueDate);

            // Assert
            Assert.False(loan.IsOverdue);
        }

        [Fact]
        public void IsOverdue_ShouldReturnTrue_WhenDueDateHasPassed_AndNotReturned()
        {

            //Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");

            var loanDate = DateTime.UtcNow.AddDays(-20);
            var dueDate = DateTime.UtcNow.AddDays(-1);

            //Act
            var loan = new Loan(book, member, loanDate, dueDate);

            //Assert
            Assert.True(loan.IsOverdue);
            Assert.False(loan.IsReturned);
        }

        [Fact]
        public void IsReturned_ShouldReturnTrue_WhenReturnDateIsSet()
        {
            // Arrange
            var book = new Book("123", "Test", "Author", 2024);
            var member = new Member("M001", "Test Person", "test@test.com");

            var loanDate = DateTime.UtcNow;
            var dueDate = loanDate.AddDays(14);
            var loan = new Loan(book, member, loanDate, dueDate);

            // Act
            loan.Return();

            // Assert
            Assert.True(loan.IsReturned);
            Assert.NotNull(loan.ReturnDate);
            Assert.True(book.IsAvailable);
            Assert.Empty(member.BorrowedBooks);
        }
    }
}
