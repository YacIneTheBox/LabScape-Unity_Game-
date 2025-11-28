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

    [SerializeField] private GameObject bulletImpactPrefab;


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
    // Appliquer une force physique uniquement
    if (hit.rigidbody != null)
    {
        hit.rigidbody.AddForce(-hit.normal * hitForce);
    }
    
    // Calculer la position avec un petit décalage pour éviter le z-fighting
    Vector3 impactPosition = hit.point + hit.normal * 0.001f;
    
    // Instancier l'impact avec la bonne rotation
    // Quaternion.LookRotation pointe le "forward" du plane vers la normale
    GameObject impact = Instantiate(bulletImpactPrefab, impactPosition, Quaternion.LookRotation(hit.normal));
    
    // Faire de l'objet touché le parent de l'impact (optionnel mais recommandé)
    impact.transform.SetParent(hit.transform);
    
    // Détruire l'impact après quelques secondes
    Destroy(impact, 2f);
    
    Debug.Log("Touché: " + hit.collider.name);
}
    }
}
