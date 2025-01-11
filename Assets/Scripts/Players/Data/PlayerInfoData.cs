using UnityEngine;

public class PlayerInfoData : MonoBehaviour
{
    [Header("플레이어 데이터")]
    [SerializeField] private PlayerData playerData;

    public PlayerData GetPlayerData() => playerData;
}
