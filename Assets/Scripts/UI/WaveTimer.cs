using UnityEngine;
using TMPro;   // si tu utilises TextMeshPro

public class WaveTimer : MonoBehaviour
{
    public float timeRemaining = 90f;   // durée en secondes
    public TMP_Text timerText;         // texte à mettre à jour
    public bool isCountingDown = true; // true = compte à rebours

    void Update()
    {
        if (!isCountingDown) return;
        if (timeRemaining <= 0f) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining < 0f) timeRemaining = 0f;

        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
