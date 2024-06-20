using probkol3.DTOs;
using probkol3.Models;

namespace probkol3.Services;

public interface IDbService
{
    Task<Character> GetCharacterInfo(int characterId);

    Task<bool> DoesItemsExist(List<int> items);
    Task<bool> IsThereEnoughWeight(int characterId, List<int> items);
    Task AddItemsToBackPack(int characterId, List<int> items);
    Task<List<AddItemsResponseDTO>> AddItemsToBackPackResult(int characterId, List<int> items);
}