using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement; // Obligatoire pour charger des scènes

public class GameManager : MonoBehaviour
{
    void Start()
    {
        // Au lancement (ou au redémarrage), on cache et on bloque la souris
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public GameObject gameOverUI;

    // Dans GameManager.cs
    public TMP_Text finalScoreText;
    public TMP_Text highScoreText;

    public void EndGame()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Vérifier si c'est un nouveau record
        if (ScoreManager.currentScore > ScoreManager.highScore)
        {
            ScoreManager.highScore = ScoreManager.currentScore;
            // Sauvegarde physiquement le record sur le PC
            PlayerPrefs.SetInt("HighScore", ScoreManager.highScore);
        }

        // Afficher les scores sur le menu
        finalScoreText.text = "Score Final: " + ScoreManager.currentScore;
        highScoreText.text = "Record: " + ScoreManager.highScore;
    }

    public void RestartGame()
    {
        // Remet le temps à la normale avant de charger
        Time.timeScale = 1f;

        // Recharge la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}