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
    private float nextFire;


    [SerializeField] private GameObject hitParticlesPrefab;

    void Start()
    {
        // Essayer plusieurs méthodes pour trouver la caméra
        fpsCam = GetComponentInParent<Camera>();
        laserLine = GetComponent<LineRenderer>();
        if (fpsCam == null)
        {
            fpsCam = Camera.main;
        }

        if (fpsCam == null)
        {
            Debug.LogError("Aucune caméra trouvée!");
        }

        // Configurer le LineRenderer
        if (laserLine != null)
        {
            laserLine.positionCount = 2;
            laserLine.useWorldSpace = true;
            laserLine.startWidth = 0.05f;
            laserLine.endWidth = 0.05f;

            // Utiliser startColor et endColor au lieu du material.color
            laserLine.startColor = Color.yellow;
            laserLine.endColor = Color.red;

            laserLine.enabled = false;
        }
        else
        {
            Debug.LogError("LaserLine n'est pas assigné!");
        }

        // Vérifier que gunEnd est assigné
        if (gunEnd == null)
        {
            Debug.LogError("gunEnd n'est pas assigné! Créez un GameObject vide au bout du canon et assignez-le.");
        }
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
            // Dessiner la ligne du gunEnd au point d'impact
            if (laserLine != null)
            {
                
                laserLine.SetPosition(0, gunEnd.position);
                laserLine.SetPosition(1, hit.point);
                laserLine.enabled = true;
            }

            // Appliquer une force physique
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            // Position avec offset pour éviter le z-fighting
            Vector3 impactPosition = hit.point + hit.normal * 0.001f;

            // Instancier les particules d'impact
            if (hitParticlesPrefab != null)
            {
                GameObject particles = Instantiate(hitParticlesPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }

            Debug.Log("Touché: " + hit.collider.name);
        }
        else
        {
            // Si on ne touche rien, la ligne va jusqu'à la portée maximale
            if (laserLine != null)
            {
                laserLine.SetPosition(0, gunEnd.position);
                laserLine.SetPosition(1, rayOrigin + fpsCam.transform.forward * weaponRange);
            }
        }

        // Faire disparaître la ligne après un court instant
        StartCoroutine(DisableLaserLine(0.1f));
    }

    IEnumerator DisableLaserLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (laserLine != null)
        {
            laserLine.enabled = false;
        }
    }
}
