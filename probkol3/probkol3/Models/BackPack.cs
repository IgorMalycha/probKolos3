using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace probkol3.Models;

[Table("BackPacks")]
[PrimaryKey(nameof(CharacterId), nameof(ItemId))]
public class BackPack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }

    [ForeignKey(nameof(ItemId))]
    public Item Item { get; set; }

    [ForeignKey(nameof(CharacterId))]
    public Character Character { get; set; }
}