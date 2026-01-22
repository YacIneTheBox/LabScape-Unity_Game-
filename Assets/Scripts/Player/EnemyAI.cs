using UnityEngine;
using UnityEngine.AI; // Obligatoire pour utiliser le NavMesh

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;

    [Header("Paramètres de Combat")]
    public float attackRange = 2f;    // Distance pour frapper
    public float attackRate = 1.5f;   // Temps entre deux attaques
    public int damage = 10;           // Dégâts infligés au joueur
    private float nextAttackTime = 0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // On trouve le joueur automatiquement par son Tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        // L'ennemi définit sa destination vers le joueur en continu
        agent.SetDestination(player.position);

        // Calculer la distance entre l'ennemi et le joueur
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si l'ennemi est assez proche, il attaque
        if (distanceToPlayer <= attackRange)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackRate;
            }
        }
    }

    void Attack()
    {
        Debug.Log("L'ennemi t'a touché !");
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}