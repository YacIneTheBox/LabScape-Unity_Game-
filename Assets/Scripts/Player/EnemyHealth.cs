using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHealth : MonoBehaviour
{
    public int health = 10;
    public AudioClip deathSound;
    [Range(0, 1)] public float volume = 1.0f;
    public int HealAmountOnDeath = 5;
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
        PlayerHealth player = Object.FindFirstObjectByType<PlayerHealth>();

        if (player != null)
        {
            player.Heal(HealAmountOnDeath); // Soigne le joueur de 5 PV
        }

        ScoreManager.AddPoints(10); // Ajoute 10 points
        if (deathSound != null)
        {
            AudioSource.PlayClipAtPoint(deathSound, transform.position, volume);
        }

        // Ton code actuel pour détruire l'ennemi et gérer le spawn
        Destroy(gameObject);
    }
}