namespace Bibliotheek;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// De "een"-kant van de relatie: een auteur heeft meerdere boeken.
/// De data annotations sturen hoe de kolom in de database eruitziet.
/// </summary>
public class Auteur
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Naam { get; set; } = "";

    // Navigatie-property: de boeken van deze auteur. EF Core vult ze
    // voor je in wanneer je erom vraagt (zie Include in Program.cs).
    public List<Boek> Boeken { get; set; } = [];
}
