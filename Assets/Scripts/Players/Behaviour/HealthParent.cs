using UnityEngine;

public class HealthParent : MonoBehaviour
{
    [Header("최대 체력")]
    [SerializeField] protected float maxHealth;
    protected float health;

    protected bool isDead = false;

    protected AnimationParent animator;

    protected virtual void Awake()
    {
        GetComponents();
    }

    protected virtual void GetComponents()
    {
        animator = GetComponent<AnimationParent>();
    }

    protected virtual void OnEnable()
    {
        InitDead();
        InitHpBar();
    }

    protected void InitDead()
    {
        isDead = false;
    }

    protected virtual void InitHpBar()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        if (health <= 0)
        {
            Death();
        }
    }

    protected virtual void Death()
    {
        isDead = true;
        animator.DeadAnimator();
    }

    public bool GetIsDead() => isDead;
}
