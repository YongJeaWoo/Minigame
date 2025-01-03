using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    private ObjectPool<GameObject> pool;
    private GameObject poolPrefab;

    public void InitPool(GameObject prefab, int defaultCapacity = 10, int maxCapacity = 30)
    {
        poolPrefab = prefab;

        pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(poolPrefab),
            actionOnGet: obj => obj.SetActive(true),
            actionOnRelease: obj => obj.SetActive(false),
            actionOnDestroy: Destroy,
            collectionCheck: true,
            defaultCapacity: defaultCapacity,
            maxSize: maxCapacity);
    }

    public GameObject GetPoolObject(Transform createPos = null)
    {
        var obj = pool.Get();

        if (createPos != null)
        {
            obj.transform.position = createPos.position;
        }

        return obj;
    }

    public void ReleasePoolObject(GameObject poolObj)
    {
        if (poolObj != null)
        {
            pool.Release(poolObj);
        }
    }

    public void ClearPool()
    {
        pool.Clear();
    }
}
