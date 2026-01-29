using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;

    public AudioClip damageSound;
    [Range(0, 1)] public float volume = 1.0f;

    public GameManager gameManager; // Glissez l'objet GameManager ici
    [SerializeField] GameObject healthDisplay;

    [Header("Effet damage")]
    public RawImage damageImage;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.2f);

    private bool isDamaged = false;
    private int maxhealth = 50;

    private float timer = 0f;

    void Start()
    {
        health = maxhealth;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            health -= 1;
            timer = 0f;
        }

        if (isDamaged)
        {
            damageImage.color = flashColor;
        }
        // Sinon, on fait revenir l'image vers la transparence totale (Lerp)
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        isDamaged = false; // On remet à faux pour la prochaine frame
        
    healthDisplay.GetComponent<TMPro.TMP_Text>().text = "" + health;
    }

    public void TakeDamage(int amount)
    {

        isDamaged = true;
        health -= amount;
        if (damageSound != null)
        {
            AudioSource.PlayClipAtPoint(damageSound, transform.position, volume);
        }

        if (health <= 0)
        {
            health = 0;
            if (gameManager != null) gameManager.EndGame();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > maxhealth)
        {
            health = maxhealth;
        }
    }
}