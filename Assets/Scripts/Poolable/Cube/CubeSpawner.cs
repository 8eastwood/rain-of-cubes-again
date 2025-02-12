using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 1f;

    public event Action<Cube> BackToPool;

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
        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        cube.Removed += RemoveCube;
    }
        
    private void RemoveCube(Cube cube)
    {
        _pool.Release(cube);
        BackToPool?.Invoke(cube);
        cube.Removed -= RemoveCube;
    }
}