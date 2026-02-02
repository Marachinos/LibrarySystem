# Bibliotekssystem (C#) – OOP, Komposition, Algoritmer & xUnit
Ett konsolbaserat bibliotekssystem som hanterar böcker, medlemmar och utlåning.
Fokus: inkapsling, komposition, interface (ISearchable), sök/sortering/statistik och enhetstester med xUnit.

## Funktioner

- **Book**
  - ISBN sätts endast vid skapande (get-only)
  - Tillgänglighet (IsAvailable)
  - GetInfo() för formaterad info
  - Implementerar `ISearchable` via `Matches(...)`

- **Member**
  - MemberId sätts endast vid skapande (get-only)
  - Read-only lista över lånade böcker (inkapsling)
  - GetInfo()

- **Loan**
  - Kopplar Book + Member
  - Beräknade properties: `IsOverdue`, `IsReturned`
  - Return() för återlämning

- **Komposition**
  - Library innehåller:
    - BookCatalog (böcker)
    - MemberRegistry (medlemmar)
    - LoanManager (utlåning + historik)

- **Algoritmer**
  - Sök (titel/författare/ISBN)
  - Sortering (titel eller utgivningsår)
  - Statistik: totalt antal böcker, antal utlånade, mest aktiv låntagare

## Projektstruktur

- LibrarySystem.App – konsolapp och domänklasser
- LibrarySystem.Tests – xUnit-tester

## Designval 

- Inkapsling: interna listor exponeras som IReadOnlyList och förändras via metoder.
- Komposition: Library fungerar som en facade för katalog/medlemsregister/utlåning.
- Robusthet: valideringar och undantag vid fel (t.ex. dubbla ISBN, låna otillgänglig bok).
- Valde alternativ B.
- Teststrategi (NegativeAndEdgeCaseTests): AAA-mönster, både happy path och negativa/edge-case tester, samt [Theory] för parametriserade testfall.

## Köra programmet

dotnet run --project LibrarySystem.App

## Köra tester:

dotnet test



