using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        // On s'assure que la souris est visible et libre dans le menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlayGame()
    {
        // Charge la scène suivante dans la liste (votre jeu)
        // Note : handgunAmmo repassera à sa valeur initiale de 30 au chargement
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Le jeu se ferme...");
        Application.Quit(); // Ferme le jeu (ne fonctionne qu'une fois le jeu exporté en .exe)
    }
}