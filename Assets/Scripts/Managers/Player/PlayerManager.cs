using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private GameObject player;
    private GameObject playerPrefab;

    #region Singleton
    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public void SetPlayer(PlayerObjectData player)
    {
        playerPrefab = player.GetPlayerPrefab();
    }

    public void InstantPlayer()
    {
        player = Instantiate(playerPrefab);
    }

    public GameObject GetPlayer() => player;
}
