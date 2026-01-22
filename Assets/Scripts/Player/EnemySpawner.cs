using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;    // Le prefab de l'ennemi à faire apparaître
    public Transform[] spawnPoints;   // Liste de points (objets vides) où ils peuvent apparaître
    public float spawnInterval = 0.1f;  // Temps entre chaque apparition
    public int maxEnemies = 100;       // Seuil maximum d'ennemis simultanés

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            // On compte combien d'ennemis sont actuellement dans la scène
            int currentEnemyCount = GameObject.FindGameObjectsWithTag("Ennemie").Length;

            if (currentEnemyCount < maxEnemies)
            {
                SpawnEnemy();
            }

            timer = 0; // Réinitialise le chrono même si le seuil est atteint
        }
    }

    void SpawnEnemy()
    {
        // Choisir un point d'apparition au hasard parmi la liste
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Créer l'ennemi
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}