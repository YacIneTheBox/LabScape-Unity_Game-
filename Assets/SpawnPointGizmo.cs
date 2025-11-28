using UnityEngine;

public class SpawnPointGizmo : MonoBehaviour
{
    public Color gizmoColor = Color.green;
    public float gizmoSize = 0.5f;

    void OnDrawGizmos()
    {
        // Dessine une sphère verte à la position du spawn point
        Gizmos.color = gizmoColor;
        Gizmos.DrawSphere(transform.position, gizmoSize);
        
        // Dessine une icône de personnage
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.7f, new Vector3(0.3f, 1.4f, 0.1f));
    }
}