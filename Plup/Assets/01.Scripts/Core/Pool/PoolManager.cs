using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private PoolListSO _poolList;
    private Dictionary<string, Pool<PoolableMono>> _poolDic = new Dictionary<string, Pool<PoolableMono>>();

    private void Awake()
    {
        foreach(var pool in _poolList.poolList)
        {
            CreatePool(pool.prefab, pool.prefab.gameObject.name, pool.count);
        }
    }

    public void CreatePool(PoolableMono prefab, string poolName, int count = 10)
    {
        _poolDic.Add(poolName, new Pool<PoolableMono>(prefab, transform, count));
    }

    public void Push(PoolableMono obj)
    {
        if (_poolDic.ContainsKey(obj.gameObject.name))
        {
            obj.Init();
            _poolDic[obj.gameObject.name].Push(obj);
        }
        else
        {
            Debug.LogError($"not have ${obj.name} pool");
        }
    }

    public PoolableMono Pop(string poolName)
    {
        if (!_poolDic.ContainsKey(poolName))
        {
            Debug.LogError($"not have [${poolName}] pool");
            return null;
        }

        PoolableMono obj = _poolDic[poolName].Pop();
        
        return obj;
    }

    public bool IsContainPoolMono(PoolableMono obj)
    {
        return _poolDic[obj.gameObject.name].Contain(obj);
    }
}
