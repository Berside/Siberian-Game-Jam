using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamSpeed = 2f;
    [SerializeField] private float sightRange = 10f;
    [SerializeField] private float hearingRange = 5f;
    [SerializeField] private float shootingRange = 8f;
    [SerializeField] private float shootingCooldown = 1f;
    [SerializeField] private LayerMask wallLayer;

    private Transform player;
    private float lastShootTime;
    private Vector2 roamDirection;
    private State state = State.Roaming;
    private bool canShoot;

    [SerializeField]
    private enum State { Roaming, Detecting, Engaging }

    private void Start()
    {
        lastShootTime = -shootingCooldown;
        ChooseNewRoamDirection();
    }

    private void Update()
    {
        player = GameObject.FindWithTag("Player").transform;
        switch (state)
        {
            case State.Roaming:
                Roam();
                break;
            case State.Detecting:
                DetectPlayer();
                break;
            case State.Engaging:
                transform.up = -(player.position - transform.position).normalized;
                EngagePlayer();
                break;
        }
    }

    private void Roam()
    {
        Move(roamDirection);

        if (Physics2D.Raycast(transform.position, roamDirection, 0.5f, wallLayer))
        {
            ChooseNewRoamDirection();
        }

        if (CanSeePlayer() || CanHearPlayer())
        {
            state = State.Detecting;
        }
    }

    private void DetectPlayer()
    {
        if (CanSeePlayer())
        {
            state = State.Engaging;
        }
        else
        {
            state = State.Roaming;
        }
    }

    private void EngagePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= shootingRange && CanSeePlayer())
        {
            if (Time.time > lastShootTime + shootingCooldown)
            {
                ShootAtPlayer();
                lastShootTime = Time.time;
            }
        }
        else
        {
            // Decide approach/retreat/hold position
            if (Random.value < 0.5f)
            {
                Move(-directionToPlayer); // Retreat
            }
            else
            {
                Move(directionToPlayer); // Approach
            }
        }

        if (!CanSeePlayer() && !CanHearPlayer())
        {
            state = State.Roaming;
            ChooseNewRoamDirection();
        }
    }

    private bool CanSeePlayer()
    {
        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, player.position) <= sightRange)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, sightRange, wallLayer | LayerMask.GetMask("Player"));
            return hit.collider != null && hit.collider.CompareTag("Player");
        }
        return false;
    }

    private bool CanHearPlayer()
    {
        return Vector2.Distance(transform.position, player.position) <= hearingRange;
    }

    private void ShootAtPlayer()
    {


        // Assuming the enemy has a gun prefab with a Fire() method attached
        var weaponStats = GetComponentInChildren<WeaponStats>(); // Replace Gun with your weapon script type

        if (weaponStats.getCurrentAmmo() > 0)
        {
            weaponStats.fire(weaponStats.transform.Find("GunEndPointPosition").transform);
        }
        else
        {
            weaponStats.reload();
        }
    }

    private void Move(Vector2 direction)
    {
        transform.up = direction;  // Face direction of movement
        transform.position += (Vector3)(direction * roamSpeed * Time.deltaTime);
    }

    private void ChooseNewRoamDirection()
    {
        roamDirection = Random.insideUnitCircle.normalized;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, hearingRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
