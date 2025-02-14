using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public Button attackButton; // Reference to UI Button
    public Enemy targetEnemy;   // The enemy to attack
    public float attackDamage = 10f; // Damage per attack

    void Start()
    {
        if (attackButton != null)
        {
            attackButton.onClick.AddListener(AttackEnemy);
        }
        else
        {
            Debug.LogError("Attack button not assigned in the Inspector.");
        }
    }

    void AttackEnemy()
    {
        if (targetEnemy != null)
        {
            Debug.Log("Attack button pressed!"); // <-- Debug message to ensure function runs
            targetEnemy.TakeDamage(attackDamage);
        }
        else
        {
            Debug.Log("No target enemy assigned."); // <-- Check if targetEnemy is missing
        }
    }
}
