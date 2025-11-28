using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour
{
    // Variables publiques (à configurer dans l'Inspector)
    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    // Variables privées
    private Camera fpsCam;
    private LineRenderer laserLine;
    private AudioSource gunAudio;
    private float nextFire;

    [SerializeField] private GameObject hitParticlesPrefab;


    void Start()
    {
        // Essayer plusieurs méthodes pour trouver la caméra
        fpsCam = GetComponentInParent<Camera>();

        if (fpsCam == null)
        {
            fpsCam = Camera.main;
        }

        if (fpsCam == null)
        {
            Debug.LogError("Aucune caméra trouvée!");
        }

        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            // Appliquer une force physique
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            // Position avec offset pour éviter le z-fighting
            Vector3 impactPosition = hit.point + hit.normal * 0.001f;

            // Instancier les particules d'impact
            GameObject particles = Instantiate(hitParticlesPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(particles, 10f);
            // Pas besoin de Destroy si "Stop Action" est sur "Destroy" dans le Particle System

            Debug.Log("Touché: " + hit.collider.name);
        }
    }

}
