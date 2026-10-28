using Bibliotheek;
using Microsoft.EntityFrameworkCore;

using BibliotheekContext context = new();

// MigrateAsync voert alle migrations uit die nog niet toegepast zijn en maakt
// zo de database op schema. De seed-gegevens uit OnModelCreating komen
// hierbij mee in de tabellen.
await context.Database.MigrateAsync();

// Include laadt de navigatie-property mee: bij elke auteur meteen zijn
// boeken. Zonder Include zou Boeken leeg blijven (lazy loading staat uit).
List<Auteur> auteurs = await context.Auteurs
    .Include(auteur => auteur.Boeken)
    .OrderBy(auteur => auteur.Naam)
    .ToListAsync();

Console.WriteLine("Auteurs en hun boeken:");
foreach (Auteur auteur in auteurs)
{
    Console.WriteLine($"\n{auteur.Naam}:");
    foreach (Boek boek in auteur.Boeken.OrderBy(boek => boek.Jaar))
    {
        Console.WriteLine($"  - {boek.Titel} ({boek.Jaar})");
    }
}

// Een query over de relatie heen: tel de boeken per auteur. EF Core
// vertaalt dit naar een SQL-join met GROUP BY.
Console.WriteLine("\nAantal boeken per auteur:");
var tellingen = await context.Boeken
    .GroupBy(boek => boek.Auteur!.Naam)
    .Select(groep => new { Auteur = groep.Key, Aantal = groep.Count() })
    .OrderByDescending(rij => rij.Aantal)
    .ToListAsync();

foreach (var rij in tellingen)
{
    Console.WriteLine($"  {rij.Auteur}: {rij.Aantal}");
}
