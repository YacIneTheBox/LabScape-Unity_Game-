using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name + " a maintenant " + health + " PV");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Logique de mort (animation, destruction, etc.)
        Destroy(gameObject);
    }
}