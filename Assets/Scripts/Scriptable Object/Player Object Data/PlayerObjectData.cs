using UnityEngine;

[CreateAssetMenu(fileName = "Selected Data")]
public class PlayerObjectData : ScriptableObject
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Sprite selectedImage;

    [Header("캐릭터 설명")]
    [TextArea(5, 8)]
    [SerializeField] private string explain;

    public string GetExplain() => explain;
    public Sprite GetSelectedImage() => selectedImage;
    public GameObject GetPlayerPrefab() => playerPrefab;
}
