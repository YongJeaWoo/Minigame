using UnityEngine;

public class BossHealthParent : MonoBehaviour, IHit
{
    [SerializeField] protected float maxHealth;
    protected float health;

    protected bool isDead = false;
    protected BossFSMController controller;
    
    private void Awake()
    {
        GetComponents();
    }
    
    private void GetComponents()
    {
        controller = GetComponent<BossFSMController>();
    }

    private void Start()
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

        controller.TakeHit();
    }

    private void Death()
    {
        controller.TransitionToState(BossFSMController.E_State.Dead);
        isDead = true;
    }

    public bool GetIsDead() => isDead;
    public float GetHealth() => health;
}
