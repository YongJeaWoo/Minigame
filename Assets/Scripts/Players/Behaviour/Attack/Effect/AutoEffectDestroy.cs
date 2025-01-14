using UnityEngine;

public class AutoEffectDestroy : MonoBehaviour
{
    public void AutoDestroy()
    {
        ObjectPoolManager.Instance.ReturnToPool(gameObject);
    }
}
