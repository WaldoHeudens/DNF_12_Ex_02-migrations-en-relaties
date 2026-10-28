namespace Bibliotheek;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// De "veel"-kant: elk boek hoort bij een auteur. De property AuteurId
/// is de foreign key; Auteur is de navigatie naar het bijhorende object.
/// </summary>
public class Boek
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Titel { get; set; } = "";

    public int Jaar { get; set; }

    // Foreign key naar de auteur, plus de navigatie-property.
    public int AuteurId { get; set; }
    public Auteur? Auteur { get; set; }
}
