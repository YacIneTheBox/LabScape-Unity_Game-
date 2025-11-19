using UnityEngine;

public class GlobalAmmo : MonoBehaviour
{
    public static int handgunAmmo = 10;
    [SerializeField] GameObject ammoDisplay;

    // Update is called once per frame
    void Update()
    {
        ammoDisplay.GetComponent<TMPro.TMP_Text>().text = "" + handgunAmmo;
    }
}
