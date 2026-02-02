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

- **Komposition (Del 2)**
  - Library innehåller:
    - BookCatalog (böcker)
    - MemberRegistry (medlemmar)
    - LoanManager (utlåning + historik)

- **Algoritmer (Del 4)**
  - Sök (titel/författare/ISBN)
  - Sortering (titel eller utgivningsår)
  - Statistik: totalt antal böcker, antal utlånade, mest aktiv låntagare

## Projektstruktur

- LibrarySystem.App – konsolapp och domänklasser
- LibrarySystem.Tests – xUnit-tester

## Köra programmet

```bash
dotnet run --project LibrarySystem.App

## Köra tester

dotnet test

## Designval


