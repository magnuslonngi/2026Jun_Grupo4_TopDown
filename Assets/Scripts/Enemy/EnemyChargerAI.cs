using System.Collections;
using UnityEngine;

public class EnemyChargerAI : MonoBehaviour
{
    public float detectionRadius = 4.5f;
    public float loseRadius = 7f;
    public float attackRange = 1.3f;
    public float chargeTime = 1f;
    public float restTime = 1.5f;
    public float patrolRadius = 2.5f;

    private CharacterMovement movement;
    private CharacterAttack attack;
    private Health health;
    private Transform player;

    private Vector2 startPosition;
    private Vector2 patrolTarget;
    private bool chasing;
    private bool isAttacking;
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
        if (isDead || isAttacking) return;

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance <= detectionRadius) chasing = true;
        if (distance > loseRadius) chasing = false;

        if (chasing)
        {
            if (distance <= attackRange)
                StartCoroutine(ChargedAttack());
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

    private IEnumerator ChargedAttack()
    {
        isAttacking = true;

        MoveTowards(player.position);
        attack.AttackPerformed();
        movement.MoveDirection = Vector2.zero;   //se queda quieto mientras carga

        yield return new WaitForSeconds(chargeTime);
        attack.AttackCanceled();                 //suelta el ataque cargado

        yield return new WaitForSeconds(restTime);
        isAttacking = false;
    }

    private void Die(float remaining)
    {
        isDead = true;
        movement.MoveDirection = Vector2.zero;
        StopAllCoroutines();
        Destroy(gameObject, 0.4f);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
#endif

}
