using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public float attackRange = 2f; // Distance for melee attack
    public float attackDamage = 25f;
    public LayerMask enemyLayer; // What counts as an enemy

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            MeleeAttack();
        }
    }

    void MeleeAttack()
    {
        // Check for enemies within range using a sphere cast
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position + transform.forward, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            Debug.Log("Hit " + enemy.name);

            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage);
            }
        }
    }

    // Draw the attack range in the Scene view for testing
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, attackRange);
    }
}
