using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefabObject;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    protected ObjectPool<T> _pool;
    
    protected int minStartPointX = 15;
    protected int maxStartPointX = 25;
    protected int minStartPointZ = -10;
    protected int maxStartPointZ = 10;
    protected int startPointY = 15;

    private void Awake()
    {
        _pool = new ObjectPool<T>
        (createFunc: () => Instantiate(_prefabObject),
            actionOnGet: GetFromPool,
            actionOnRelease: ReleaseInPool,
            actionOnDestroy: Destroy,
            collectionCheck: true,
            _poolCapacity,
            _poolMaxSize
        );
    }

    private void GetFromPool(T prefabObject)
    {
        prefabObject.gameObject.SetActive(true);
    }

    private void ReleaseInPool(T prefabObject)
    {
        prefabObject.gameObject.SetActive(false);
    }
}