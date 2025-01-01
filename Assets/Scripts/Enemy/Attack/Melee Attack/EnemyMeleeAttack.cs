using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    [Header("대미지 설정")]
    [SerializeField] private float damageAmount = 10;
    [SerializeField] private float damageDelay = 1.5f;
    [SerializeField] private float attackRange = 1.2f;

    private bool canAttack = true;

    private EnemyAnimation enemy;

    private void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        enemy = GetComponent<EnemyAnimation>();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (enemy.GetPlayer() != null && canAttack)
        {
            float distance = Vector2.Distance(transform.position, enemy.GetPlayer().position);
            if (distance <= attackRange)
            {
                DealDamage();
                StartCoroutine(AttackCooldown());
            }
        }
    }

    private void DealDamage()
    {
        if (enemy.GetPlayer().TryGetComponent<PlayerHealth>(out var playerHealth))
        {
            playerHealth.TakeDamage(damageAmount);
            
            if (playerHealth.GetIsDead())
            {
                canAttack = false;
                enemy.SetNullPlayer();
                return;
            }
        }
    }

    private IEnumerator AttackCooldown()
    {
        if (!canAttack) yield break;

        canAttack = false;
        yield return new WaitForSeconds(damageDelay);
        canAttack = true;
    }
}
