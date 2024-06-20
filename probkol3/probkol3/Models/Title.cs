using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace probkol3.Models;

[Table("Titles")]
public class Title
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<CharacterTitle> CharacterTitles { get; set; }
    
}