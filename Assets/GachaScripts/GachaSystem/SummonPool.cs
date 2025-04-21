using UnityEngine;
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

    // Method to set the weight for a specific rarity
    public void SetRarityWeight(string rarity, float weight)
    {
        if (dropRate.ContainsKey(rarity))
        {
            dropRate[rarity] = weight;
        }
        else
        {
            Debug.LogWarning("Rarity not found in summoning pool: " + rarity);
        }
    }

    // Method to pull from the summon pool
    public SummonableItem Pull()
    {
        SummonableItem reward = GenerateReward();
        Debug.Log("You have summoned: " + reward.Name);
        return reward;
    }

    // Method to get the probability of each rarity
    public float GetProbability(string rarity)
    {
        if (dropRate.ContainsKey(rarity))
        {
            return dropRate[rarity];
        }
        return 0f;
    }

    // Method to generate a random summonable item based on available items
    public SummonableItem GenerateReward()
    {
        // For now, just randomly select an item from availableSummons
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
