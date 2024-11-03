using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewDistance = 12f;       // Distance the player can see
    public float viewAngle = 120f;          // Angle of field of view in degrees
    public LayerMask enemyLayer;           // Layer containing the enemies
    public LayerMask obstacleLayer;        // Layer containing obstacles (walls)

    public float visionCooldown;

    private float time = 0;

    private List<GameObject> visibleEnemies = new List<GameObject>();

    private void Start()
    {
        
    }

    void Update()
    {
        FindVisibleEnemies();
        time += Time.deltaTime;

        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, viewDistance, enemyLayer);

        var enemiesInRangeSet = new HashSet<GameObject>(enemiesInRange.Select(collider => collider.gameObject));

        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            if (!enemiesInRangeSet.Contains(enemy))
            {
                enemy.GetComponent<SpriteRenderer>().enabled = false;
                enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false; // Hide the gun
            }
        }
    }

    void FindVisibleEnemies()
    {
        visibleEnemies.Clear();

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
                    enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true; // Show the gun
                    time = 0;
                }
                else
                {
                    if (time >= visionCooldown)
                    {
                        enemy.GetComponent<SpriteRenderer>().enabled = false; // Hide the enemy if a wall is in the way
                        enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false; // Hide the gun
                        time = 0;
                    }
                }
            }
            else
            {
                if (time >= visionCooldown)
                {
                    enemy.GetComponent<SpriteRenderer>().enabled = false; // Hide the enemy if out of view angle
                    enemy.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false; // Hide the gun
                    time = 0;
                }
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
