using UnityEngine;

public class RepositionCollider : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) return;

        var player = PlayerManager.Instance.GetPlayer();
        var movement = player.GetComponent<PlayerMovement>();

        Vector3 playerPos = player.transform.position;
        Vector3 myPos = transform.position;

        float diffX = playerPos.x - myPos.x;
        float diffY = playerPos.y - myPos.y;

        Vector3 playerDir = movement.GetInputVector();

        // 플레이어 방향에 따라 이동 방향 계산
        float dirX = playerDir.x < 0 ? -1 : 1;
        float dirY = playerDir.y < 0 ? -1 : 1;

        // 재배치 계산
        switch (transform.tag)
        {
            case "Ground":
                // X축 이동
                if (Mathf.Abs(diffX) > 20) // 20은 콜라이더 반경
                {
                    transform.Translate(Vector3.right * dirX * 40); // 40은 맵 크기의 2배
                }

                // Y축 이동
                if (Mathf.Abs(diffY) > 20)
                {
                    transform.Translate(Vector3.up * dirY * 40);
                }

                if (Mathf.Abs(diffX) > 20 && Mathf.Abs(diffY) > 20)
                {
                    Vector3 moveDirection = new Vector3(dirX, dirY, 0).normalized;  // 대각선 이동을 위한 방향 벡터
                    transform.Translate(moveDirection * 40); // 대각선 이동
                }
                break;
        }
    }
}
