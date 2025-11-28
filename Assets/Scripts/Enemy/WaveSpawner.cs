using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemyPrefab;
        public int count;
        public float rate;
    }

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [Header("Target")]
    public Transform player;

    [Header("Spawn Settings")]
    [Tooltip("How many random spawn locations do you want to generate?")]
    public int numberOfSpawnPoints = 4; 
    
    private Vector3[] validSpawnPositions; 

    [Header("Wave Settings")]
    public Wave[] waves;
    public float timeBetweenWaves = 5f;

    [Header("Status (Read Only)")]
    public SpawnState state = SpawnState.COUNTING;
    public float waveCountdown;

    private int nextWave = 0;
    private float searchCountdown = 1f;
    private NavMeshTriangulation navMeshData;

    void Start()
    {
        waveCountdown = timeBetweenWaves;
        
        navMeshData = NavMesh.CalculateTriangulation();

        GenerateSpawnPoints();

        if (player == null) {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void GenerateSpawnPoints()
    {
        validSpawnPositions = new Vector3[numberOfSpawnPoints];

        for (int i = 0; i < numberOfSpawnPoints; i++)
        {
            int randomVertex = Random.Range(0, navMeshData.vertices.Length);
            
            validSpawnPositions[i] = navMeshData.vertices[randomVertex] + Vector3.up * 1.5f;
            
            Debug.Log($"Spawn Point {i+1} created at: {validSpawnPositions[i]}");
        }
    }

    void Update()
    {
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
                WaveCompleted();
            else
                return;
        }

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
                StartCoroutine(SpawnWave(waves[nextWave]));
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        Debug.Log("Wave Completed!");
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
            nextWave = 0;
        else
            nextWave++;
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Ennemie") == null)
                return false;
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave: " + _wave.name);
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemyPrefab);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.WAITING;
        yield break;
    }

    void SpawnEnemy(GameObject _enemy)
    {
        int randomIndex = Random.Range(0, validSpawnPositions.Length);
        Vector3 spawnPos = validSpawnPositions[randomIndex];

        GameObject tempEnemy = Instantiate(_enemy, spawnPos, Quaternion.identity);

        DroneChase droneScript = tempEnemy.GetComponent<DroneChase>();
        if (droneScript != null)
        {
            droneScript.playerTarget = player;
        }
    }
    
    
}
