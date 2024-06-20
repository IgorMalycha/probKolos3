using probkol3.Models;

namespace probkol3.DTOs;

public class GetCharacterInfoDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public ICollection<BackPackItemDTO> BackPackItems { get; set; } = new HashSet<BackPackItemDTO>();
    public ICollection<TitleDTO> Titles { get; set; }

}