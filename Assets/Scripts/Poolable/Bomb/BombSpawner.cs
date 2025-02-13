using UnityEngine;

public class BombSpawner : PoolHandler<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.BackedToPool += GetBombFromPool;
    }

    private void OnDisable()
    {
        _cubeSpawner.BackedToPool -= GetBombFromPool;
    }

    private void GetBombFromPool(Cube cube)
    {
        Bomb bomb = _pool.Get();
        
        bomb.transform.position = cube.transform.position;
        bomb.StartCountdown();
        
        bomb.Destroyed += ReleaseBomb;
    }

    private void ReleaseBomb(Bomb bomb)
    {
        bomb.Destroyed -= ReleaseBomb;

        _pool.Release(bomb);
    }
}