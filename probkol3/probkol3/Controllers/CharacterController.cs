using System.Transactions;
using Microsoft.AspNetCore.Mvc;
using probkol3.DTOs;
using probkol3.Services;

namespace probkol3.Controllers;

[Route("api/characters")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly IDbService _dbService;
    public CharacterController(IDbService dbService)
    {
        _dbService = dbService;
    }


    [HttpGet("{characterId}")]
    public async Task<IActionResult> GetCharacterInfo(int characterId)
    {
        var allInfo = await _dbService.GetCharacterInfo(characterId);

        GetCharacterInfoDTO getCharacterInfoDto = new GetCharacterInfoDTO()
        {
            FirstName = allInfo.FirstName,
            LastName = allInfo.LastName,
            CurrentWeight = allInfo.CurrentWeight,
            MaxWeight = allInfo.MaxWeight,
            BackPackItems = allInfo.BackPacks.Select(e => new BackPackItemDTO()
            {
                ItemName = e.Item.Name,
                ItemWeight = e.Item.Weight,
                Amount = e.Amount
            }).ToList(),
            Titles = allInfo.CharacterTitles.Select(e => new TitleDTO()
            {
                Title = e.Title.Name,
                AcquiredAt = e.AcquiredAt
            }).ToList()
        };

        return Ok(getCharacterInfoDto);
    }

    [HttpPost("{characterId}/backpacks")]
    public async Task<IActionResult> AddItemToBackpack(int characterId, [FromBody] List<int> items)
    {
        if (!await _dbService.DoesItemsExist(items))
        {
            return NotFound("Items with given Id does not exist");
        }

        if (!await _dbService.IsThereEnoughWeight(characterId, items))
        {
            return NotFound("There is not enought weight for these items");
        }
        
        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            await _dbService.AddItemsToBackPack(characterId, items);
    
            scope.Complete();
        }

        return Created("api/characters", _dbService.AddItemsToBackPackResult(characterId, items));
    }
}