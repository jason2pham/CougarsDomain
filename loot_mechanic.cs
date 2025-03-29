using System;
using System.Collections.Generic;

public class Boss
{
    public string Name { get; set; }
    public int Difficulty { get; set; } // Determines loot quality

    public Boss(string name, int difficulty)
    {
        Name = name;
        Difficulty = difficulty;
    }

    public Loot DropLoot()
    {
        int lootAmount = Difficulty * 10; // Harder bosses give more loot
        return new Loot(lootAmount);
    }
}

public class Loot
{
    public int Amount { get; set; }

    public Loot(int amount)
    {
        Amount = amount;
    }
}

public class Character
{
    public string Name { get; set; }
    public int LootPoints { get; private set; }

    public Character(string name)
    {
        Name = name;
        LootPoints = 0;
    }

    public void DefeatBoss(Boss boss)
    {
        Loot loot = boss.DropLoot();
        LootPoints += loot.Amount;
        Console.WriteLine($"{Name} defeated {boss.Name} and earned {loot.Amount} loot points! Total: {LootPoints}");
    }

    public void Summon(SummonPool pool)
    {
        if (LootPoints >= 10)
        {
            LootPoints -= 10; // Cost to summon
            SummonableItem item = pool.Pull();
            Console.WriteLine($"{Name} summoned: {item.Name} ({item.Rarity})");
        }
        else
        {
            Console.WriteLine("Not enough loot points to summon!");
        }
    }
}

public class SummonPool
{
    private List<SummonableItem> availableSummons;

    public SummonPool()
    {
        availableSummons = new List<SummonableItem>();
    }

    public void AddSummonableItem(SummonableItem item)
    {
        availableSummons.Add(item);
    }

    public SummonableItem Pull()
    {
        Random rand = new Random();
        int index = rand.Next(availableSummons.Count);
        return availableSummons[index];
    }
}

public class SummonableItem
{
    public string Name { get; set; }
    public string Rarity { get; set; }

    public SummonableItem(string name, string rarity)
    {
        Name = name;
        Rarity = rarity;
    }
}

public class Program
{
    public static void Main()
    {
        Character hero = new Character("Hero");
        Boss easyBoss = new Boss("Goblin King", 1);
        Boss hardBoss = new Boss("Dragon", 5);

        SummonPool pool = new SummonPool();
        pool.AddSummonableItem(new SummonableItem("Flame Sword", "Epic"));
        pool.AddSummonableItem(new SummonableItem("Wind Bow", "Rare"));
        pool.AddSummonableItem(new SummonableItem("Legendary Machine Gun", "Legendary"));

        hero.DefeatBoss(easyBoss);
        hero.DefeatBoss(hardBoss);
        hero.Summon(pool);
        hero.Summon(pool);
    }
}
