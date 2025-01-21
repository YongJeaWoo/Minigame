using System.Collections;
using UnityEngine;

public class BringerCastState : BossAttackState
{
    [Space(2)]
    [Header("생성 정보")]
    [SerializeField] private GameObject castPrefab;
    [SerializeField] private GameObject hintPrefab;
    [SerializeField] private float castDelay;
    [SerializeField] private float hintDuration;

    private WaitForSeconds hintWaitTime;
    private bool hasCasted = false;

    #region FSM State
    public override void EnterState(BossFSMController.E_State state)
    {
        InitCast(state);
    }

    public override void ExitState()
    {
        ExitValue();
    }

    public override void UpdateState()
    {
        
    }
    #endregion

    #region InitValue
    private void InitCast(BossFSMController.E_State state)
    {
        hintWaitTime = new(hintDuration);
        hasCasted = false;
        animator.SetInteger(Animator_ParamName, (int)state);

        StartCoroutine(CastCoroutine());
    }
    #endregion

    #region ExitValue
    private void ExitValue()
    {
        StopAllCoroutines();
        hasCasted = false;
    }
    #endregion

    private IEnumerator CastCoroutine()
    {
        int castCount = Random.Range(3, 6);
        Vector3[] spawnPositions = new Vector3[castCount];

        for (int i = 0; i < castCount; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle.normalized * 1f;
            spawnPositions[i] = controller.Player.transform.position + (Vector3)randomOffset;

            ShowHint(spawnPositions[i]);
        }

        yield return hintWaitTime;

        if (!hasCasted)
        {
            for (int i = 0; i < castCount; i++)
            {
                SpawnObject(spawnPositions[i]);
            }

            hasCasted = true;
        }

        controller.TransitionToState(BossFSMController.E_State.Idle);
    }

    private void ShowHint(Vector3 position)
    {
        var hintObj = ObjectPoolManager.Instance.GetFromPool(hintPrefab);

        if (hintObj != null)
        {
            hintObj.transform.position = position;

            var hintRenderer = hintObj.GetComponent<SpriteRenderer>();
            if (hintRenderer != null)
            {
                var collider = castPrefab.GetComponentInChildren<BoxCollider2D>();
                if (collider != null)
                {
                    hintRenderer.size = collider.size;
                    hintObj.transform.position = position + (Vector3)collider.offset;
                }
            }

            StartCoroutine(HideHintAfterTime(hintObj, hintWaitTime));
        }
    }

    private IEnumerator HideHintAfterTime(GameObject hintObj, WaitForSeconds waitTime)
    {
        yield return waitTime;

        ObjectPoolManager.Instance.ReturnToPool(hintObj);
    }

    private void SpawnObject(Vector3 position)
    {
        if (castPrefab != null)
        {
            var castObj = ObjectPoolManager.Instance.GetFromPool(castPrefab);
            castObj.transform.SetPositionAndRotation(position, Quaternion.identity);
        }
    }
}
