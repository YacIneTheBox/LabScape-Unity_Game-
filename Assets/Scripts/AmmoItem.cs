using UnityEngine;
using System.Collections;

public class AmmoItem : MonoBehaviour
{
    public int ammoToGive = 15;
    public float respawnTime = 30f;
    public AudioClip pickupSound;
    [Range(0, 1)] public float volume = 1.0f;

    private MeshRenderer visual;
    private Collider col;

    void Start()
    {
        visual = GetComponentInChildren<MeshRenderer>();
        col = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // On ajoute directement au stock global
            GlobalAmmo.handgunAmmo += ammoToGive;

            if (pickupSound != null)
            {
                AudioSource.PlayClipAtPoint(pickupSound, transform.position, volume);
            }

            // On cache la boîte
            StartCoroutine(HideAndRespawn());
        }
    }

    IEnumerator HideAndRespawn()
    {
        visual.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(respawnTime);

        visual.enabled = true;
        col.enabled = true;
    }
}