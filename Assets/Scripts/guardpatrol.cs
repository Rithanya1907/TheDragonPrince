using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public float patrolDistance = 4f;
    public float patrolSpeed = 3f;

    public float detectionRange = 7f;
    public float chaseSpeed = 4f;
    public float attackRange = 1.0f;
    public float attackCooldown = 2.0f;

    public GuardManager guardManager;

    private Vector3 startPosition;
    private bool movingForward = true;

    private Transform player;
    private PlayerHealth playerHealth;

    private bool canChase = false;
    private float lastAttackTime = -10f;

    void Start()
    {
        startPosition = transform.position;

        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
        }
    }

    void Update()
    {
        // Every guard keeps patrolling.
        // Only the selected guard can chase.
        if (!canChase)
        {
            Patrol();
            return;
        }

        if (player == null)
        {
            Patrol();
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        ChasePlayer(distance);
    }

    void Patrol()
    {
        float direction = movingForward ? 1f : -1f;

        transform.Translate(
            Vector3.right * direction * patrolSpeed * Time.deltaTime
        );

        if (transform.position.x >= startPosition.x + patrolDistance)
        {
            movingForward = false;
        }

        if (transform.position.x <= startPosition.x - patrolDistance)
        {
            movingForward = true;
        }
    }

    void ChasePlayer(float distance)
    {
        Vector3 direction =
            player.position - transform.position;

        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            transform.rotation =
                Quaternion.LookRotation(direction);
        }

        if (distance > attackRange)
        {
            transform.position +=
                direction.normalized *
                chaseSpeed *
                Time.deltaTime;
        }
        else
        {
            AttackPlayer();
        }
    }

    void AttackPlayer()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            if (playerHealth != null)
            {
                playerHealth.LoseLife();

                Debug.Log("👹 Guard attacked the Prince!");
            }

            lastAttackTime = Time.time;
        }
    }

    public void SetActiveGuard(bool active)
    {
        canChase = active;

        if (active)
        {
            Debug.Log("⚔️ This guard can now chase!");
        }
    }

    public void GuardDied()
    {
        if (guardManager != null)
        {
            guardManager.GuardDefeated();
        }
    }
}