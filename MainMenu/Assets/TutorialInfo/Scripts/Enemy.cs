using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 100f; // Enemy's starting health

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Enemy Health: " + health); // <-- This will print health to Console

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemy Defeated!"); // <-- This will print when the enemy dies
        Destroy(gameObject);
    }
}
