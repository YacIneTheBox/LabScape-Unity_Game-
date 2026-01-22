using UnityEngine;
using UnityEngine.SceneManagement; // Obligatoire pour charger des scènes

public class GameManager : MonoBehaviour
{
    public GameObject gameOverUI;

    public void EndGame()
    {
        // 1. Affiche le menu
        gameOverUI.SetActive(true);

        // 2. Arrête le temps dans le jeu
        Time.timeScale = 0f;

        // 3. Libère la souris (important pour un FPS)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        // Remet le temps à la normale avant de charger
        Time.timeScale = 1f;

        // Recharge la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}