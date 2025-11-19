using UnityEngine;

public class PistolAmmoCollect : MonoBehaviour
{
    [SerializeField] AudioSource ammoCollect;
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        ammoCollect.Play();
        GlobalAmmo.handgunAmmo += 10;
        Destroy(gameObject);
    }
}
