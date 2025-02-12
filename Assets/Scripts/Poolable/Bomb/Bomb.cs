using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Exploder _exploder;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 5;
    private int _lifeTime => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);
    
    public event Action<Bomb> Destroyed;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        StartCountdown();
    }

    public void StartCountdown()
    {
        StartCoroutine(DestroyInTime(_lifeTime));
    }
    
    private IEnumerator DestroyInTime(int delay)
    {
        yield return new WaitForSeconds(delay);

        Destroyed?.Invoke(this);
        _exploder.Explode();
    }
}
