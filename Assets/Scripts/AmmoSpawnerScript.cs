using UnityEngine;

public class AmmoSpawnerScript : MonoBehaviour
{
    public GameObject ammoPrefab;  // Glisse ton Prefab de munitions ici
    public Transform[] spawnPoints; // Glisse tes points de spawn ici

    void Start()
    {
        // On parcourt tous les points de la liste
        foreach (Transform pt in spawnPoints)
        {
            if (ammoPrefab != null)
            {
                // On fait apparaître une boîte sur chaque point
                Instantiate(ammoPrefab, pt.position, pt.rotation);
            }
        }
    }
}