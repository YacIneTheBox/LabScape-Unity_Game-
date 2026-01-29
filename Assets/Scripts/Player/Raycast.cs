using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Raycast : MonoBehaviour
{
    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    [Header("Hitmarker UI")]
    public RawImage leftcross;
    public RawImage rightcross;
    public RawImage upcross;
    public RawImage downcross;
    public Color normalColor = Color.black;
    public Color hitColor = Color.red;

    private Camera fpsCam;
    private LineRenderer laserLine;

    [SerializeField] private GameObject hitParticlesPrefab;

    void Start()
    {
        fpsCam = GetComponentInParent<Camera>();
        laserLine = GetComponent<LineRenderer>();

        if (fpsCam == null) fpsCam = Camera.main;

        // Initialiser la couleur du viseur
        SetCrosshairColor(normalColor);

        if (laserLine != null)
        {
            laserLine.positionCount = 2;
            laserLine.enabled = false;
        }
    }

    public void Shoot()
    {
        // Consommation de munitions (vu précédemment)
        if (GlobalAmmo.handgunAmmo <= 0) return;
        GlobalAmmo.handgunAmmo -= 1;

        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            // --- DETECTION ENNEMI ---
            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(gunDamage);
                // On active l'effet visuel du hitmarker ici !
                StartCoroutine(HitMarkerEffect());
            }

            // --- RESTE DU CODE (Laser, Particules, Physique) ---
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
                laserLine.enabled = true;
            }
        }

        StartCoroutine(DisableLaserLine(0.07f));
    }

    // Fonction pour changer la couleur de toutes les parties du viseur d'un coup
    private void SetCrosshairColor(Color targetColor)
    {
        leftcross.color = targetColor;
        rightcross.color = targetColor;
        upcross.color = targetColor;
        downcross.color = targetColor;
    }

    // Coroutine pour l'effet de flash du hitmarker
    IEnumerator HitMarkerEffect()
    {
        SetCrosshairColor(hitColor); // Devient rouge
        yield return new WaitForSeconds(0.2f); // Attend un tout petit peu
        SetCrosshairColor(normalColor); // Revient blanc (ou ta couleur de base)
    }

    IEnumerator DisableLaserLine(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (laserLine != null) laserLine.enabled = false;
    }
}