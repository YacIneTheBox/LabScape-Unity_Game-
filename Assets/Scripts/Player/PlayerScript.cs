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

}
