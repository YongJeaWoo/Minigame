using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : HealthParent
{
    [SerializeField] private float chipSpeed;
    private float lerpTimer;

    [Header("앞 체력 바")]
    [SerializeField] private Image frontHealthBar;
    [Header("뒷 체력 바")]
    [SerializeField] private Image backHealthBar;

    private PlayerData playerData;
    
    private void Update()
    {
        UpdateHealthUI();
    }

    protected override void GetComponents()
    {
        base.GetComponents();
        playerData = GetComponent<PlayerInfoData>().GetPlayerData();
    }

    protected override void InitHpBar()
    {
        maxHealth = playerData.GetMaxHp();
        base.InitHpBar();
    }

    private void UpdateHealthUI()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hpFraction = health / maxHealth;

        if (fillBack > hpFraction)
        {
            frontHealthBar.fillAmount = hpFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hpFraction, percentComplete);
        }

        if (fillFront < hpFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hpFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete *= percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        health = Mathf.Clamp(health, 0, maxHealth);
        lerpTimer = 0f;
    }
}
