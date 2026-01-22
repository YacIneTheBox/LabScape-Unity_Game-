using UnityEngine;
using System.Collections;

public class Raycast : MonoBehaviour
{
    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private Camera fpsCam;
    private LineRenderer laserLine;

    [SerializeField] private GameObject hitParticlesPrefab;

    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
        laserLine = GetComponent<LineRenderer>();

        // ... (Le reste de votre Start reste identique) ...

        if (fpsCam == null) fpsCam = Camera.main;
        if (laserLine != null)
        {
            laserLine.positionCount = 2;
            // ... configuration du line renderer ...
            laserLine.enabled = false;
        }
    }

    public void Shoot()
    {

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(gunDamage);
            }
            if (laserLine != null)
            {
                laserLine.SetPosition(0, gunEnd.position);
                laserLine.SetPosition(1, hit.point);
                laserLine.enabled = true;
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * hitForce);
            }

            Vector3 impactPosition = hit.point + hit.normal * 0.001f;

            if (hitParticlesPrefab != null)
            {
                Instantiate(hitParticlesPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        else
        {
            if (laserLine != null)
            {
                laserLine.SetPosition(0, gunEnd.position);
                laserLine.SetPosition(1, rayOrigin + fpsCam.transform.forward * weaponRange);
                laserLine.enabled = true; // Important d'activer la ligne même si on manque
            }
        }

        StartCoroutine(DisableLaserLine(0.07f)); // Durée un peu plus courte pour être plus "snappy"
    }

    IEnumerator DisableLaserLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (laserLine != null) laserLine.enabled = false;
    }
}
