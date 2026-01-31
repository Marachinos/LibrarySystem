using System;
using System.Collections.Generic;
using System.Text;
using LibrarySystem.App;
using Xunit;

namespace LibrarySystem.Tests
{
    public class MemberTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange
            var since = new DateTime(2020, 1, 15);

            // Act
            var member = new Member("M001", "Test Person", "test@test.com", since);

            // Assert
            Assert.Equal("M001", member.MemberId);
            Assert.Equal("Test Person", member.Name);
            Assert.Equal("test@test.com", member.Email);
            Assert.Equal(since, member.MemberSince);
            Assert.Empty(member.BorrowedBooks);
        }

        [Fact]
        public void AddBorrowedBook_ShouldAddBookToBorrowedBooks()
        {
            // Arrange
            var member = new Member("M001", "Test Person", "test@test.com");
            var book = new Book("123", "Test", "Author", 2024);

            // Act
            member.AddBorrowedBook(book);

            // Assert
            Assert.Single(member.BorrowedBooks);
            Assert.Equal("123", member.BorrowedBooks[0].ISBN);
        }

        [Fact]
        public void RemoveBorrowedBook_ShouldRemoveBookFromBorrowedBooks()
        {
            // Arrange
            var member = new Member("M001", "Test Person", "test@test.com");
            var book = new Book("123", "Test", "Author", 2024);
            member.AddBorrowedBook(book);

            // Act
            member.RemoveBorrowedBook(book);

            // Assert
            Assert.Empty(member.BorrowedBooks);
        }
    }
}
