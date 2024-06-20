using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace probkol3.Models;

[Table("Items")]
public class Item
{
    [Key]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; }

    public int Weight { get; set; }

    public ICollection<BackPack> backPacks { get; set; } = new HashSet<BackPack>();
    
}