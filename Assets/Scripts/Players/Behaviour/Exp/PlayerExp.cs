using System;
using UnityEngine;

public class PlayerExp : MonoBehaviour
{
    private float exp;
    private float maxExp;
    private float baseMaxExp;
    private int level;   

    private PlayerData playerData;

    public event Action OnLevelUp;

    private void Awake()
    {
        GetComponents();
    }

    private void Start()
    {
        InitLevel();   
    }

    private void GetComponents()
    {
        playerData = GetComponent<PlayerInfoData>().GetPlayerData();
    }

    private void InitLevel()
    {
        exp = 0;
        level = 0;
        baseMaxExp = playerData.GetMaxExp();
        maxExp = baseMaxExp;

        UpdateExpUI();
    }

    public void AddExp(float addAmount)
    {
        exp += addAmount;

        while (exp >= maxExp)
        {
            LevelUp();
        }

        UpdateExpUI();
    }

    private void LevelUp()
    {
        exp -= maxExp;
        level++;
        maxExp = baseMaxExp * Mathf.Pow(1.3f, level);

        if (level % 5 == 0)
        {
            OnLevelUp?.Invoke();
        }
    }

    private void UpdateExpUI()
    {
        var expImage = UIManager.Instance.GetExpImage();
        expImage.fillAmount = exp / maxExp;
    }
}
