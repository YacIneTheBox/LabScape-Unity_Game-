using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int playerHealth = 100;
    [SerializeField] AudioSource hurtSound;
    [SerializeField] GameObject healthDisplay;
    void Update()
    {
        healthDisplay.GetComponent<TMPro.TMP_Text>().text = "" + playerHealth;
    }
    public void TakeDamage(int damageAmount)
    {
        playerHealth -= damageAmount;
        hurtSound.Play();
        if (playerHealth <= 0)
        {
            Debug.Log("Player Dead");
        }
    }

}
