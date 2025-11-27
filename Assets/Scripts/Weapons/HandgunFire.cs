using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;
    [SerializeField] AudioSource EmptyGunSound;
    [SerializeField] GameObject handGun;
    [SerializeField] bool canFire = true;
    [SerializeField] GameObject extraCross;

    // ===== CAMERA SHAKE VARIABLES ===== 
    [Header("Camera Shake Effect")]
    [SerializeField]
    [Tooltip("Intensité du shake (en unités Unity)")]
    private float shakeAmount = 0.1f;

    [SerializeField]
    [Tooltip("Durée du shake (en secondes)")]
    private float shakeDuration = 0.15f;

    [SerializeField]
    [Tooltip("Vitesse de décroissance du shake")]
    private float decreaseFactor = 2.0f;

    private Camera playerCamera;
    private Transform cameraTransform;
    // ==================================

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
        // ===== LANCE LE CAMERA SHAKE ===== 
        StartCoroutine(CameraShake());
        // =================================
        yield return new WaitForSeconds(0.5f);
        extraCross.SetActive(false);
        canFire = true;
    }

    IEnumerator EmptyGun()
    {
        EmptyGunSound.Play();
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }

    // ===== CAMERA SHAKE COROUTINE ===== 
    IEnumerator CameraShake()
    {
        if (cameraTransform == null) yield break;

        Vector3 originalPosition = cameraTransform.localPosition;
        float currentDuration = shakeDuration;

        while (currentDuration > 0)
        {
            // Génère une position aléatoire dans un rayon défini
            Vector3 shakeOffset = Random.insideUnitSphere * shakeAmount;
            cameraTransform.localPosition = originalPosition + shakeOffset;

            // Décroissance progressive de la durée
            currentDuration -= Time.deltaTime * decreaseFactor;

            yield return null; // Attends la prochaine frame
        }

        // Retour à la position d'origine
        cameraTransform.localPosition = originalPosition;
    }
    // ===================================
}


