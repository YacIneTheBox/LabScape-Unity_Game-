using UnityEngine;
using TMPro; // Pour l'affichage

public class ScoreManager : MonoBehaviour
{
    public static int currentScore = 0;
    public static int highScore = 0;

    public TMP_Text scoreText;

    void Start()
    {
        currentScore = 0; // On remet le score à 0 au début de la partie
        // On charge le record sauvegardé sur l'ordinateur
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    void Update()
    {
        // Affiche le score en temps réel (comme pour tes munitions)
        scoreText.text = "Score: " + currentScore;
    }

    public static void AddPoints(int points)
    {
        currentScore += points;
    }
}