using System;
using JetBrains.Annotations;
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
    
    public int Count { get; private set; }

    public event Action Spawned;

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

    public int CountActiveInPool()
    {
       return _pool.CountActive;
    }

    public int CountAllInPool()
    {
        return _pool.CountAll;
    }

    private void GetFromPool(T prefabObject)
    {
        prefabObject.gameObject.SetActive(true);

        Count++;
        Spawned?.Invoke();
    }

    private void ReleaseInPool(T prefabObject)
    {
        prefabObject.gameObject.SetActive(false);
    }
}