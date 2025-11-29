using UnityEngine;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawnPoints = new List<GameObject>();

    void Start()
    {
        // Trouve tous les points de spawn
        GameObject[] foundSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        
        foreach (GameObject spawnPoint in foundSpawnPoints)
        {
            spawnPoints.Add(spawnPoint);
            Debug.Log("Point de spawn trouvé: " + spawnPoint.name);
        }

        SpawnPlayerAtRandomPoint();
    }

    void SpawnPlayerAtRandomPoint()
    {
        if (spawnPoints.Count == 0)
        {
            Debug.LogError("Aucun point de spawn trouvé !");
            return;
        }

        // Trouve le joueur (qui est déjà dans la scène)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if (player == null)
        {
            Debug.LogError("Joueur non trouvé dans la scène !");
            return;
        }

        // Choisis un point aléatoire
        int randomIndex = Random.Range(0, spawnPoints.Count);
        GameObject chosenSpawnPoint = spawnPoints[randomIndex];

        // Déplace le joueur
        player.transform.position = chosenSpawnPoint.transform.position;
        Debug.Log("Joueur spawn à: " + chosenSpawnPoint.name);
    }
}