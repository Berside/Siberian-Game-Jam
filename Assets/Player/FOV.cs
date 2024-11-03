using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewDistance = 12f;       // Distance the player can see
    public float viewAngle = 60f;          // Angle of field of view in degrees
    public LayerMask enemyLayer;           // Layer containing the enemies
    public LayerMask obstacleLayer;        // Layer containing obstacles (walls)
    private List<GameObject> visibleEnemies = new List<GameObject>();

    private void Start()
    {
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            enemy.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void Update()
    {
        FindVisibleEnemies();
    }

    void FindVisibleEnemies()
    {
        visibleEnemies.Clear();
        Debug.Log(visibleEnemies);
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, viewDistance, enemyLayer);

        foreach (var enemyCollider in enemiesInRange)
        {
            GameObject enemy = enemyCollider.gameObject;
            Vector2 directionToEnemy = (enemy.transform.position - transform.position).normalized;
            float angleToEnemy = Vector2.Angle(transform.up * -1, directionToEnemy) - 90; // Player facing -Y axis

            if (angleToEnemy < viewAngle / 2 - 90)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToEnemy, distanceToEnemy, obstacleLayer);

                if (!hit) // Enemy is visible if there's no wall blocking the view
                {
                    visibleEnemies.Add(enemy);
                    enemy.GetComponent<SpriteRenderer>().enabled = true; // Show the enemy
                }
                else
                {
                    enemy.GetComponent<SpriteRenderer>().enabled = false; // Hide the enemy if a wall is in the way
                }
            }
            else
            {
                enemy.GetComponent<SpriteRenderer>().enabled = true; // Hide the enemy if out of view angle
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw view distance and FOV angle lines in the scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        Vector3 viewAngleA = DirFromAngle(-viewAngle / 2 - 90, false);
        Vector3 viewAngleB = DirFromAngle(viewAngle / 2 - 90, false);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewDistance);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewDistance);
    }

    private Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.z;

        return new Vector3(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
