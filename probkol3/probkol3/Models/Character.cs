using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace probkol3.Models;


[Table("Characters")]
public class Character
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]
    public string FirstName { get; set; }
    [MaxLength(120)]
    public string LastName { get; set; }

    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }

    public ICollection<BackPack> BackPacks { get; set; } = new HashSet<BackPack>();

    public ICollection<CharacterTitle> CharacterTitles { get; set; } = new HashSet<CharacterTitle>();
    
}