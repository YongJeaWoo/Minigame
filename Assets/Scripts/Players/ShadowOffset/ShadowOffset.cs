using UnityEngine;

public class ShadowOffset : MonoBehaviour
{
    [Header("그림자 오프셋")]
    [SerializeField] protected Vector2 shadowOffset;
    private SpriteRenderer spriteRenderer;
    private GameObject shadowObject;

    private void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shadowObject = transform.GetChild(0).gameObject;
    }

    public void UpdateShadowPosition(Vector2 value)
    {
        float offsetX = value.x == 0 ?
            (spriteRenderer.flipX ? -shadowOffset.x : shadowOffset.x) :
            (value.x < 0 ? -shadowOffset.x : shadowOffset.x);

        float offsetY = shadowOffset.y;

        Vector3 playerPos = transform.position;
        Vector3 shadowPosition = new(playerPos.x + offsetX, playerPos.y + offsetY, playerPos.z);

        shadowObject.transform.position = shadowPosition;
    }

    public void ShadowObjectActive(bool isOn) => shadowObject.SetActive(isOn);
    public Vector2 GetShadowOffset() => shadowOffset;
}
