using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public GameManager gameManager; // Glissez l'objet GameManager ici

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            gameManager.EndGame();
        }
    }
}