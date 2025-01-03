using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

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
    }

    private Dictionary<GameObject, ObjectPool> pools = new();

    public void InitPool(GameObject prefab, int defaultCapacity = 10, int maxCapacity = 30)
    {
        if (pools.ContainsKey(prefab)) return;

        GameObject poolObject = new GameObject(prefab.name + "_Pool");
        poolObject.transform.SetParent(transform);

        ObjectPool pool = poolObject.AddComponent<ObjectPool>();
        pool.InitPool(prefab, defaultCapacity, maxCapacity);

        pools[prefab] = pool;
    }

    public GameObject GetFromPool(GameObject prefab, Transform createPos = null)
    {
        if (!pools.TryGetValue(prefab, out var pool))
        {
            InitPool(prefab);
            pool = pools[prefab];
        }

        return pool.GetPoolObject(createPos);
    }

    public void ReturnToPool(GameObject obj)
    {
        string cleanName = obj.name.Replace("(Clone)", "").Trim();

        foreach (var pool in pools)
        {
            if (pool.Key.name == cleanName)
            {
                pool.Value.ReleasePoolObject(obj);
                return;
            }
        }

        Destroy(obj);
    }

    public void ClearPool(GameObject prefab)
    {
        if (pools.TryGetValue(prefab, out var pool))
        {
            pool.ClearPool();
            pools.Remove(prefab);
        }
    }

    public void ClearAllPools()
    {
        foreach (var pool in pools.Values)
        {
            pool.ClearPool();
        }

        pools.Clear();
    }
}
