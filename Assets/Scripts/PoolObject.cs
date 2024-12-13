using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;


public class PoolObject : MonoBehaviour
{
    public GameObject prefab;
    public int size = 10;

    public UnityEvent<Transform> OnBeforeEnable;

    private List<GameObject> _pool;

    [Inject]
    private DiContainer _diContainer;

    private void Awake()
    {
        _pool = new List<GameObject>();
        for (var i = 0; i < size; i++)
        {
            var obj = _diContainer.InstantiatePrefab(prefab, transform);
            obj.SetActive(false);
            _pool.Add(obj);
        }
    }

    public GameObject GetObject()
    {
        foreach (var obj in _pool)
        {
            if(obj.activeInHierarchy) continue;
            OnBeforeEnable?.Invoke(obj.transform);
            obj.SetActive(true);
            return obj;
        }

        return null;
    }
}