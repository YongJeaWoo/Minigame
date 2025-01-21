using UnityEngine;

public class HealthParent : MonoBehaviour, IHit
{
    [Header("최대 체력")]
    [SerializeField] protected float maxHealth;
    protected float health;

    protected bool isDead = false;

    protected AnimationParent animator;
    protected SpriteRenderer sr;

    private int originOrderLayer;

    protected virtual void Awake()
    {
        GetComponents();
    }

    protected virtual void GetComponents()
    {
        animator = GetComponent<AnimationParent>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void OnEnable()
    {
        InitValue();
    }

    protected void InitValue()
    {
        isDead = false;
        originOrderLayer = 7;
        sr.sortingOrder = originOrderLayer;

        InitHpBar();
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
        DecreaseSortingLayer();
    }

    private void DecreaseSortingLayer()
    {
        if (sr != null)
        {
            sr.sortingOrder--;
        }
    }

    public bool GetIsDead() => isDead;
}
