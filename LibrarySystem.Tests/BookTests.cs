using LibrarySystem.App;
using Xunit;

namespace LibrarySystem.Tests
{
    public class BookTests
    {
        [Fact]
        public void Constructor_ShouldSetPropertiesCorrectly()
        {
            // Arrange & Act
            var book = new Book("978-91-0-012345-6", "Testbok", "Testförfattare", 2024);

            // Assert
            Assert.Equal("978-91-0-012345-6", book.ISBN);
            Assert.Equal("Testbok", book.Title);
            Assert.Equal("Testförfattare", book.Author);
            Assert.Equal(2024, book.PublishedYear);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void IsAvailable_ShouldBeTrueForNewBook()
        {
            // Arrange
            var book = new Book("123", "Ny bok", "Någon", 2020);

            // Act & Assert
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void GetInfo_ShouldReturnFormattedString()
        {
            // Arrange
            var book = new Book("123", "Vindsträdgården", "Virginia C. Andrews", 1981);

            // Act
            var info = book.GetInfo();

            // Assert
            Assert.Contains("Vindsträdgården", info);
            Assert.Contains("Virginia C. Andrews", info);
            Assert.Contains("1981", info);
            Assert.Contains("ISBN: 123", info);
            Assert.Contains("Available", info);
        }
    }
}
