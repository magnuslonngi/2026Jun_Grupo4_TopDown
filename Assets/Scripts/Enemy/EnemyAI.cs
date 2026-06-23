using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float detectionRadius = 4.5f;
    public float loseRadius = 7f;
    public float attackRange = 1.1f;
    public float attackCooldown = 1.25f;
    public float patrolRadius = 2.5f;

    private CharacterMovement movement;
    private CharacterAttack attack;
    private Health health;
    private Transform player;

    private Vector2 startPosition;
    private Vector2 patrolTarget;
    private float attackTimer;
    private bool chasing;
    private bool isDead;

    private void Start()
    {
        movement = GetComponent<CharacterMovement>();
        attack = GetComponent<CharacterAttack>();
        health = GetComponent<Health>();
        health.OnHealthDeplete.AddListener(Die);

        startPosition = transform.position;
        patrolTarget = startPosition;
        player = FindFirstObjectByType<PlayerInput>().transform;
    }

    private void Update()
    {
        if (isDead) return;

        attackTimer -= Time.deltaTime;
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius) chasing = true;
        if (distance > loseRadius) chasing = false;

        if (chasing)
        {
            if (distance <= attackRange)
                Attack();
            else
                MoveTowards(player.position);
        }
        else
        {
            Patrol();
        }
    }

    private void MoveTowards(Vector2 target)
    {
        movement.MoveDirection = (target - (Vector2)transform.position).normalized;
    }

    private void Patrol()
    {
        MoveTowards(patrolTarget);
        if (Vector2.Distance(transform.position, patrolTarget) < 0.2f)
            patrolTarget = startPosition + Random.insideUnitCircle * patrolRadius;
    }

    private void Attack()
    {
        movement.MoveDirection = Vector2.zero;
        if (attackTimer > 0f) return;

        MoveTowards(player.position);

        // golpe simple: se cancela enseguida para que no entre en modo carga
        attack.AttackPerformed();
        attack.AttackCanceled();

        attackTimer = attackCooldown;
    }

    private void Die(float remaining)
    {
        isDead = true;
        movement.MoveDirection = Vector2.zero;
        Destroy(gameObject, 0.4f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
