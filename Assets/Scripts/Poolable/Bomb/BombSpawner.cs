using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.BackToPool += GetBombFromPool;
    }

    private void OnDisable()
    {
        _cubeSpawner.BackToPool -= GetBombFromPool;
    }

    private void GetBombFromPool(Cube cube)
    {
        Bomb bomb = _pool.Get();
        
        bomb.transform.position = cube.transform.position;
        bomb.StartCountdown();
        
        bomb.Destroyed += RemoveBomb;
    }

    private void RemoveBomb(Bomb bomb)
    {
        bomb.Destroyed -= RemoveBomb;

        _pool.Release(bomb);
    }
}