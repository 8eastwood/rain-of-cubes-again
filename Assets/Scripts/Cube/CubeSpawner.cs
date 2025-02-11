using System.Collections;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 1f;

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
        
        cube.Removed += RemoveObject;
    }
        
    private void RemoveObject(Cube cube)
    {
        _pool.Release(cube);
        cube.Removed -= RemoveObject;
    }
}