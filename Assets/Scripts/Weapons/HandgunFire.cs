using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] AudioSource gunFire;
    [SerializeField] GameObject handGun;
    [SerializeField] bool canFire = true;
    void Update()
    {
        if (Input.GetMouseButton(0) && canFire) { 
            canFire = false;
            StartCoroutine(FiringGun());
        }
    }

    IEnumerator FiringGun()
    {
        gunFire.Play();
        handGun.GetComponent<Animator>().SetTrigger("Shoot");
        Debug.Log("Pew Pew");
        yield return new WaitForSeconds(0.5f);
        canFire = true;
    }
}
