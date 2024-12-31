using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 inputVec;
    private Vector2 lastInputVec;

    private Rigidbody2D rigid;

    [Header("플레이어 움직임 속도")]
    [SerializeField] private float moveSpeed;
    [Header("그림자 오프셋")]
    [SerializeField] private Vector2 shadowOffset;

    private void Awake()
    {
        GetComponents();
    }

    private void GetComponents()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        InitValue();
    }

    private void InitValue()
    {
        lastInputVec = Vector2.right;
    }

    private void Update()
    {
        InputKeys();
    }

    private void InputKeys()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (inputVec != Vector2.zero)
        {
            lastInputVec = inputVec;
        }
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        Vector2 nextVec = moveSpeed * Time.fixedDeltaTime * inputVec.normalized;
        rigid.MovePosition(rigid.position + nextVec);
    }

    public Vector2 GetInputVector() => inputVec.normalized;
    public Vector2 GetLastInputVector() => lastInputVec.normalized;
    public Vector2 GetShadowOffset() => shadowOffset;
}
