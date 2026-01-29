using UnityEngine;

public class GlobalAmmo : MonoBehaviour
{
    [SerializeField] public static int handgunAmmo = 15;
    [SerializeField] GameObject ammoDisplay;

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.GetComponent<TMPro.TMP_Text>().text = "" + handgunAmmo;
    }
}
