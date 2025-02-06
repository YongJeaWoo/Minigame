using System;
using UnityEngine;

public class CoinData : MonoBehaviour
{
    private readonly string Coin_Data = $"Coin";

    private int coin;

    public void SaveCoin()
    {
        PlayerPrefs.SetInt(Coin_Data, coin);
    }

    public void LoadCoin()
    {
        PlayerPrefs.GetInt(Coin_Data, coin);
    }

    public void UpdateCoin(int amount)
    {
        coin += amount;
    }

    public int GetMyCoin() => coin;
}
