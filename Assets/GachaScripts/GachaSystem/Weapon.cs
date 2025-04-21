using UnityEngine;
using System.Collections.Generic;

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

    public int CalcDamage() => AttackPower;
}

