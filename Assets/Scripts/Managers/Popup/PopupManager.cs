using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    #region Singleton
    public static PopupManager Instance;

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
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    #endregion   

    [Header("사용할 패널들")]
    [SerializeField] private List<GameObject> popups;
    [Header("패널 생성 위치")]
    [SerializeField] private Transform popupLocation;

    private List<GameObject> activePopups = new List<GameObject>();

    public GameObject AddPopup(string popupName)
    {
        RemovePopup(popupName);

        var popupPrefab = popups.FirstOrDefault(p => p.name == popupName);

        if (popupPrefab != null)
        {
            var ins = Instantiate(popupPrefab, popupLocation);
            ins.name = popupName;
            activePopups.Add(ins);
            return ins;
        }

        return null;
    }

    public bool RemovePopup(string popupName)
    {
        GameObject removePopup = activePopups.FirstOrDefault(p => p.name == popupName);

        if (popupName != null)
        {
            activePopups.Remove(removePopup);
            Destroy(removePopup);

            return true;
        }

        return false;
    }
}
