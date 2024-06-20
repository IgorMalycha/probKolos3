using Microsoft.EntityFrameworkCore;
using probkol3.Models;

namespace probkol3.Context;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<BackPack> BackPacks { get; set; }
    public DbSet<CharacterTitle> CharacterTitles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Item>().HasData(new List<Item>
            {
                new Item {
                    Id = 1,
                    Name = "Miecz",
                    Weight = 10
                }
            });

            modelBuilder.Entity<Title>().HasData(new List<Title>
            {
                new Title {
                    Id = 1,
                    Name = "wojownik"
                }
            });

            modelBuilder.Entity<Character>().HasData(new List<Character>
            {
                new Character
                {
                    Id = 1,
                    FirstName = "Igor",
                    LastName = "miekki",
                    CurrentWeight = 0,
                    MaxWeight = 50
                }
            });

            modelBuilder.Entity<CharacterTitle>().HasData(new List<CharacterTitle>
            {
                new CharacterTitle
                {
                    CharacterId = 1,
                    TitleId = 1,
                    AcquiredAt = DateTime.Parse("2024-05-31")
                }
                
            });

            modelBuilder.Entity<BackPack>().HasData(new List<BackPack>
            {
                new BackPack
                {
                    CharacterId = 1,
                    ItemId = 1,
                    Amount = 4
                }
            });
    }
}
    
    
