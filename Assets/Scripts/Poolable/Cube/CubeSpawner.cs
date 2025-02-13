using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : PoolHandler<Cube>
{
    [SerializeField] private float _repeatRate = 1f;

    public event Action<Cube> BackedToPool;

    private void Start()
    {
        StartCoroutine(SpawnCubeWithRate(_repeatRate));
    }
    
    private IEnumerator SpawnCubeWithRate(float repeatRate)
    {
        WaitForSeconds wait = new WaitForSeconds(repeatRate);

        bool isWorking = true;

        while (isWorking)
        {
            yield return wait;

            GetCubeFromPool();
        }
    }

    private void GetCubeFromPool()
    {
        Cube cube = _pool.Get();
        
        cube.transform.position = new Vector3(Random.Range(minStartPointX, maxStartPointX),
            startPointY, Random.Range(minStartPointZ, maxStartPointZ));
        cube.TransferVelocity();
        
        cube.Removed += ReleaseCube;
    }

   
        
    private void ReleaseCube(Cube cube)
    {
        _pool.Release(cube);
        BackedToPool?.Invoke(cube);
        cube.Removed -= ReleaseCube;
    }
}