using Microsoft.EntityFrameworkCore;
using probkol3.Context;
using probkol3.DTOs;
using probkol3.Models;

namespace probkol3.Services;

public class DbService : IDbService
{
    
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<Character> GetCharacterInfo(int characterId)
    {
        return await _context.Characters.Include(e => e.CharacterTitles)
            .ThenInclude(e => e.Title)
            .Include(e => e.BackPacks)
            .ThenInclude(e => e.Item).Where(e => e.Id == characterId)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> DoesItemsExist(List<int> items)
    {
        foreach (var item in items)
        {
            if (await _context.Items.AnyAsync(e => e.Id == item))
            {
                return false;
            }
        }
        return true;
    }

    public async Task<bool> IsThereEnoughWeight(int characterId, List<int> items)
    {
        var maxWeight = await _context.Characters.Where(e => e.Id == characterId)
            .Select(e => e.MaxWeight)
            .FirstOrDefaultAsync();
        
        var currentWeight = await _context.Characters.Where(e => e.Id == characterId)
            .Select(e => e.CurrentWeight)
            .FirstOrDefaultAsync();

        var actualWeight = maxWeight - currentWeight;

        var itemsWeight = 0;
        
        foreach (var item in items)
        {
            var itemWeight = await _context.Items.Where(e => e.Id == item)
                .Select(e => e.Weight).FirstOrDefaultAsync();
            itemsWeight += itemWeight;
        }

        return itemsWeight <= actualWeight;
    }

    public async Task AddItemsToBackPack(int characterId, List<int> items)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(e => e.Id == characterId);
        int newWeight = 0;
        var backpacks = new List<BackPack>();
        foreach (var itemId in items)
        {
            var item = await _context.Items.FirstOrDefaultAsync(e => e.Id == itemId);
            var itemExist = await 
                _context.BackPacks.FirstOrDefaultAsync(e => e.CharacterId == characterId && e.ItemId == itemId);

            if (itemExist != null)
            {
                itemExist.Amount += 1;
            }
            else
            {
                backpacks.Add(new BackPack()
                {
                    ItemId = itemId,
                    CharacterId = characterId,
                    Amount = 1
                });
            }
            newWeight += item.Weight;
        }

        character.CurrentWeight += newWeight;
        await _context.BackPacks.AddRangeAsync(backpacks);
        await _context.SaveChangesAsync();
    }

    public async Task<List<AddItemsResponseDTO>> AddItemsToBackPackResult(int characterId, List<int> items)
    {
        var list = new List<AddItemsResponseDTO>();
        foreach (var item in items)
        {
            list.Add(new AddItemsResponseDTO()
            {
                Amount = await _context.BackPacks.Where(e => e.ItemId == item).Select(e => e.Amount)
                    .FirstOrDefaultAsync(),
                ItemId = item,
                characterId = characterId
            });
        }

        return list;
    }
}