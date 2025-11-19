using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;
    [SerializeField] AudioSource EmptyGunSound;
    [SerializeField] GameObject handGun;
    [SerializeField] bool canFire = true;
    [SerializeField] GameObject extraCross;
    void Update()
    {
        if (Input.GetMouseButton(0) && canFire) { 
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
        Debug.Log("Pew Pew");
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
}


