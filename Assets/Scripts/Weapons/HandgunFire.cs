using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;
    [SerializeField] AudioSource EmptyGunSound;
    [SerializeField] GameObject handGun;
    [SerializeField] bool canFire = true;
    [SerializeField] GameObject extraCross;

    // AJOUT : Référence au script Raycast
    [Header("References")]
    [SerializeField] Raycast raycastScript;

    [Header("Camera Shake Effect")]
    [SerializeField] private float shakeAmount = 0f;
    [SerializeField] private float shakeDuration = 0.15f;
    [SerializeField] private float decreaseFactor = 2.0f;

    private Camera playerCamera;
    private Transform cameraTransform;

    void Start()
    {
        playerCamera = Camera.main;
        if (playerCamera != null)
        {
            cameraTransform = playerCamera.transform;
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            if (GlobalAmmo.handgunAmmo <= 0)
            {
                StartCoroutine(EmptyGun());
            }
            else
            {
                StartCoroutine(FiringGun());
            }
        }
    }

    IEnumerator FiringGun()
    {
        gunFire.Play();
        extraCross.SetActive(true);
        GlobalAmmo.handgunAmmo--;
        handGun.GetComponent<Animator>().SetTrigger("Shoot");

        // APPEL DU RAYCAST ICI
        // On ne tire le rayon et les particules que si on a des munitions
        if (raycastScript != null)
        {
            raycastScript.Shoot();
        }

        StartCoroutine(CameraShake());

        yield return new WaitForSeconds(0.5f); // Ceci gère désormais la cadence de tir globale
        extraCross.SetActive(false);
        canFire = true;
    }

    IEnumerator EmptyGun()
    {
        EmptyGunSound.Play();
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }

    // ... (Votre coroutine CameraShake reste inchangée) ...
    IEnumerator CameraShake()
    {
        if (cameraTransform == null) yield break;
        Vector3 originalPosition = cameraTransform.localPosition;
        float currentDuration = shakeDuration;
        while (currentDuration > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
            cameraTransform.localPosition = originalPosition + shakeOffset;
            currentDuration -= Time.deltaTime * decreaseFactor;
            yield return null;
        }
        cameraTransform.localPosition = originalPosition;
    }
}
