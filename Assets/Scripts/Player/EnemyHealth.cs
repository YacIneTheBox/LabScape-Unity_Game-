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

    // Dans ton script EnemyHealth, modifie la fonction Die() :
    void Die()
    {
        ScoreManager.AddPoints(10); // Ajoute 10 points

        // Ton code actuel pour détruire l'ennemi et gérer le spawn
        Destroy(gameObject);
    }
}