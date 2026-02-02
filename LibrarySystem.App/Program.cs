using System;
using System.Globalization;
using LibrarySystem.App;

namespace LibrarySystem.App;

internal class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        var library = Seed();

        while (true)
        {
            PrintMenu();
            Console.Write("Välj: ");
            var choice = Console.ReadLine()?.Trim();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ShowAllBooks(library);
                    break;
                case "2":
                    SearchBook(library);
                    break;
                case "3":
                    BorrowBook(library);
                    break;
                case "4":
                    ReturnBook(library);
                    break;
                case "5":
                    ShowMembers(library);
                    break;
                case "6":
                    ShowStatistics(library);
                    break;
                case "0":
                    Console.WriteLine("Tack för Ditt besök! Hoppas Du kommer tillbaka snart igen.");
                    return;
                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
            
            Console.WriteLine();
        }
    }

    private static Library Seed()
    {
        var library = new Library();

        // Böcker
        library.Catalog.Add(new Book("123-1", "Sagan om ringen", "J.R.R. Tolkien", 1954));
        library.Catalog.Add(new Book("123-2", "Hobbiten", "J.R.R. Tolkien", 1937));
        library.Catalog.Add(new Book("123-3", "Clean Code", "Robert C. Martin", 2008));

        // Medlemmar
        library.Members.Add(new Member("M001", "Anna Andersson", "anna@test.com"));
        library.Members.Add(new Member("M002", "Bertil Berg", "bertil@test.com"));

        // För att visa en utlånad bok i exempel: låna Hobbiten till M002
        var hobbit = library.Catalog.FindByIsbn("123-2")!;
        var bertil = library.Members.FindById("M002")!;
        library.Loans.CreateLoan(hobbit, bertil);

        return library;
    }

    private static void PrintMenu()
    {
        Console.WriteLine("=== Bibliotekssystem ===");
        Console.WriteLine();
        Console.WriteLine("1. Visa alla böcker");
        Console.WriteLine("2. Sök bok");
        Console.WriteLine("3. Låna bok");
        Console.WriteLine("4. Returnera bok");
        Console.WriteLine("5. Visa medlemmar");
        Console.WriteLine("6. Statistik");
        Console.WriteLine("0. Avsluta");
        Console.WriteLine();
    }

    private static void ShowAllBooks(Library library)
    {
        if (library.Catalog.Books.Count == 0)
        {
            Console.WriteLine("Inga böcker i katalogen.");
            return;
        }

        Console.WriteLine("Alla böcker:");
        int i = 1;
        foreach (var b in library.Catalog.Books)
        {
            Console.WriteLine($"{i}. {b.GetInfo()}");
            i++;
        }
    }

    private static void SearchBook(Library library)
    {
        Console.Write("Bokens ISBN: ");
        var term = Console.ReadLine() ?? "";

        var results = library.SearchBooks(term);

        Console.WriteLine();
        if (results.Count == 0)
        {
            Console.WriteLine("Inga träffar.");
            return;
        }

        Console.WriteLine("Sökresultat:");
        for (int i = 0; i < results.Count; i++)
        {
            var b = results[i];
            var status = b.IsAvailable ? "Tillgänglig" : "Utlånad";
            Console.WriteLine($"{i + 1}. \"{b.Title}\" av {b.Author} ({b.PublishedYear}) - {status}");
        }
    }

    private static void BorrowBook(Library library)
    {
        Console.Write("Ange ISBN: ");
        var isbn = (Console.ReadLine() ?? "").Trim();

        Console.Write("Ange medlems-ID: ");
        var memberId = (Console.ReadLine() ?? "").Trim();

        var book = library.Catalog.FindByIsbn(isbn);
        if (book is null)
        {
            Console.WriteLine("Hittar ingen bok med det ISBN-numret.");
            return;
        }

        var member = library.Members.FindById(memberId);
        if (member is null)
        {
            Console.WriteLine("Hittar ingen medlem med det medlems-ID:t.");
            return;
        }

        try
        {
            var loan = library.Loans.CreateLoan(book, member);
            Console.WriteLine($"Boken \"{book.Title}\" har lånats ut till {member.Name}.");
            Console.WriteLine($"Återlämningsdatum: {loan.DueDate:yyyy-MM-dd}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte låna ut boken: {ex.Message}");
        }
    }

    private static void ReturnBook(Library library)
    {
        Console.Write("Ange ISBN: ");
        var isbn = (Console.ReadLine() ?? "").Trim();

        var book = library.Catalog.FindByIsbn(isbn);
        if (book is null)
        {
            Console.WriteLine("Hittar ingen bok med det ISBN-numret.");
            return;
        }

        // Leta aktivt lån för boken (som inte är returnerat)
        var activeLoan = library.Loans.Loans
            .FirstOrDefault(l => l.Book.ISBN.Equals(book.ISBN, StringComparison.OrdinalIgnoreCase) && !l.IsReturned);

        if (activeLoan is null)
        {
            Console.WriteLine("Boken verkar inte vara utlånad just nu.");
            return;
        }

        try
        {
            library.Loans.ReturnLoan(activeLoan);
            Console.WriteLine($"Boken \"{book.Title}\" är nu returnerad.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Kunde inte returnera: {ex.Message}");
        }
    }

    private static void ShowMembers(Library library)
    {
        if (library.Members.Members.Count == 0)
        {
            Console.WriteLine("Inga medlemmar registrerade.");
            return;
        }

        Console.WriteLine("Medlemmar:");
        foreach (var m in library.Members.Members)
        {
            Console.WriteLine(m.GetInfo());
        }
    }

    private static void ShowStatistics(Library library)
    {
        Console.WriteLine("Statistik:");
        Console.WriteLine($"Totalt antal böcker: {library.GetTotalBooks()}");
        Console.WriteLine($"Antal utlånade böcker: {library.GetBorrowedBooksCount()}");

        var mostActive = library.GetMostActiveBorrower();
        Console.WriteLine($"Mest aktiv låntagare: {(mostActive is null ? "Ingen" : $"{mostActive.Name} ({mostActive.MemberId})")}");
    }
}

