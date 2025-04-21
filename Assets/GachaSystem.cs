using System;
using System.Collections.Generic;

public class SummonPool
{
    private List<SummonableItem> availableSummons;
    private Dictionary<string, float> dropRate;

    public SummonPool()
    {
        availableSummons = new List<SummonableItem>();
        dropRate = new Dictionary<string, float>();
    }

    // Method to add Summonable Items to the pool
    public void AddSummonableItem(SummonableItem item)
    {
        availableSummons.Add(item);
        if (!dropRate.ContainsKey(item.Rarity))
        {
            dropRate[item.Rarity] = 0f;
        }
    }
    public void setRarityWeight(string rarity, float weight)
    {
        if (rarityWeights.Contains(rarity))
        {
            rarityWeights[rarity] = weight;
        }
        else
        {
            Console.Writeline("Rarity not found in summoning pool.");
        }
    }
    // Method to pull from the summon pool
    public void Pull()
    {
        SummonableItem reward = GenerateReward();
        Console.WriteLine("You have summoned: " + reward.Name);
    }

    // Method to get probability of each rarity
    public float GetProbability(string rarity)
    {
        if (dropRate.ContainsKey(rarity))
        {
            return dropRate[rarity];
        }
        return 0f;
    }

    // Method to generate a random summonable item
    public SummonableItem GenerateReward()
    {
        Random rand = new Random();
        int index = rand.Next(availableSummons.Count);
        return availableSummons[index];
    }

    // Method to sum the probabilities of all items in the summon pool
    public float SummableItem()
    {
        float total = 0f;
        foreach (var rate in dropRate.Values)
        {
            total += rate;
        }
        return total;
    }
}

public class Character
{
    private int health;
    private int defense;
    private List<string> skills;
    private int attack;

    public Character(int health, int defense, int attack)
    {
        this.health = health;
        this.defense = defense;
        this.attack = attack;
        this.skills = new List<string>();
    }

    // Getter methods
    public int GetHealth() => health;
    public int GetDefense() => defense;

    public List<string> GetSkills() => new List<string>(skills);

    // Methods to interact with the character
    public void UseSkill(string skill)
    {
        if (skills.Contains(skill))
        {
            Console.WriteLine($"Used skill: {skill}");
        }
        else
        {
            Console.WriteLine("Skill not available.");
        }
    }

    public void LevelUp()
    {
        health += 10;
        defense += 5;
        attack += 3;
        Console.WriteLine("Character leveled up!");
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"Health: {health}, Defense: {defense}, Attack: {attack}");
        Console.WriteLine("Skills: " + string.Join(", ", skills));
    }

    // Add skill to character
    public void AddSkill(string skill)
    {
        skills.Add(skill);
    }
}

public class SummonableItem
{
    public string Name { get; set; }
    public string Rarity { get; set; }
    public Dictionary<string, int> Stats { get; set; }

    public SummonableItem(string name, string rarity, Dictionary<string, int> stats)
    {
        Name = name;
        Rarity = rarity;
        Stats = stats;
    }

    // Getters for item properties
    public string GetName() => Name;
    public string GetRarity() => Rarity;

    // Display the information of the summonable item
    public void DisplayInfo()
    {
        Console.WriteLine($"Name: {Name}, Rarity: {Rarity}");
        Console.WriteLine("Stats: ");
        foreach (var stat in Stats)
        {
            Console.WriteLine($"{stat.Key}: {stat.Value}");
        }
    }
}

public class Weapon : SummonableItem
{
    public int AttackPower { get; set; }
    public string ElementType { get; set; }

    public Weapon(string name, string rarity, Dictionary<string, int> stats, int attackPower, string elementType)
        : base(name, rarity, stats)
    {
        AttackPower = attackPower;
        ElementType = elementType;
    }

    // Getter for Attack Power
    public int GetAttackPower() => AttackPower;

    // Getter for Element Type
    public string GetElementType() => ElementType;

    // Display weapon info
    public new void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine($"Attack Power: {AttackPower}, Element Type: {ElementType}");
    }

    // Method to calculate damage based on element type and attack power
    public int CalcDamage()
    {
        // Simple calculation: for now, just return attack power
        // You could add element-based damage calculation logic here
        return AttackPower;
    }
}

// Example usage
public class Program
{
    public static void Main()
    {
        // Creating some summonable items
        var swordStats = new Dictionary<string, int> { { "Strength", 10 } };
        Weapon sword = new Weapon("Flame Sword", "Epic", swordStats, 30, "Fire");

        var bowStats = new Dictionary<string, int> { { "Strength", 8 } };
        Weapon bow = new Weapon("Wind Bow", "Rare", bowStats, 20, "Wind");

        var staffStats = new Dictionary<string, int> { { "Strength", 8 } };
        Weapon staff = new Weapon("Wind Staff", "Rare", bowStats, 20, "Wind");

        var knivesStats = new Dictionary<string, int> { { "Strength", 5 } };
        Weapon knives = new Weapon("Knives", "Common", kniveStats, 20, "Normal");

        var gunStats = new Dictionary<string, int> { { "Strength", 20 } };
        Weapon gun = new Weapon("Gun", "Rare", gunStats, 20, "Bullet");

        var machineGunStats = new Dictionary<string, int> { { "Strength", 40 } };
        Weapon machineGun = new Weapon("Machine Gun", "Legendary", machineGunStats, 40, "Bullet");

        // Creating a character
        Character hero = new Character(100, 10, 25);
        hero.AddSkill("Fireball");
        hero.LevelUp();
        hero.DisplayInfo();

        // Creating summon pool and adding items
        SummonPool pool = new SummonPool();
        pool.AddSummonableItem(sword);
        pool.AddSummonableItem(bow);

        // summoning rarity chances 
        pool.setRarityWeight("Common", 50f);
        pool.setRarityWeight("Rare", 30f);
        pool.setRarityWeight("Epic", 15f);

        // Pull a reward from the summon pool
        pool.Pull();
    }
}