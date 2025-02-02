using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("�÷��̾� �⺻ ����")]
    [SerializeField] private float maxExp;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHp;

    [Header("���� ����")]
    [SerializeField] private float attackPoint;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackDelay;

    public float GetMaxHp() => maxHp;
    public float GetMoveSpeed() => moveSpeed;
    public float GetMaxExp() => maxExp;
    public float GetAttackPoint() => attackPoint;
    public float GetAttackRange() => attackRange;   
    public float GetAttackDelay() => attackDelay;
}
