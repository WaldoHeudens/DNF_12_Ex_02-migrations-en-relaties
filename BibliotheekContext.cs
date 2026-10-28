namespace Bibliotheek;

using Microsoft.EntityFrameworkCore;

public class BibliotheekContext : DbContext
{
    public DbSet<Auteur> Auteurs => Set<Auteur>();
    public DbSet<Boek> Boeken => Set<Boek>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=bibliotheek.db");
    }

    // Wat je niet met data annotations regelt, doe je hier met de fluent API.
    // Beide manieren zijn geldig; de fluent API is krachtiger voor relaties.
    protected override void OnModelCreating(ModelBuilder model)
    {
        // De een-op-veel relatie expliciet: een auteur heeft veel boeken,
        // elk boek heeft een auteur, gekoppeld via de foreign key AuteurId.
        model.Entity<Auteur>()
            .HasMany(auteur => auteur.Boeken)
            .WithOne(boek => boek.Auteur)
            .HasForeignKey(boek => boek.AuteurId);

        // Seeding: startgegevens die in de migration terechtkomen en bij
        // "database update" mee in de database worden gezet.
        model.Entity<Auteur>().HasData(
            new Auteur { Id = 1, Naam = "Robert C. Martin" },
            new Auteur { Id = 2, Naam = "Martin Fowler" });

        model.Entity<Boek>().HasData(
            new Boek { Id = 1, Titel = "Clean Code", Jaar = 2008, AuteurId = 1 },
            new Boek { Id = 2, Titel = "Clean Architecture", Jaar = 2017, AuteurId = 1 },
            new Boek { Id = 3, Titel = "Refactoring", Jaar = 1999, AuteurId = 2 });
    }
}
