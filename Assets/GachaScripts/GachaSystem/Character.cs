using UnityEngine;
using System.Collections.Generic;

public class Character
{
    private int health;
    private int defense;
    private int attack;
    private List<string> skills = new();

    public Character(int health, int defense, int attack)
    {
        this.health = health;
        this.defense = defense;
        this.attack = attack;
    }

    public void AddSkill(string skill) => skills.Add(skill);

    public void LevelUp()
    {
        health += 10;
        defense += 5;
        attack += 3;
        Debug.Log("Character leveled up!");
    }

    public void DisplayInfo()
    {
        Debug.Log($"Health: {health}, Defense: {defense}, Attack: {attack}");
        Debug.Log("Skills: " + string.Join(", ", skills));
    }
}
