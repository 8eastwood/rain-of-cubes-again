using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private List<Material> _colors = new List<Material>();
    [SerializeField] private Material _startMaterial;

    private MeshRenderer _renderer;
    private bool _isColorChanged = false;
    private int _minLifeTime = 2;
    private int _maxLifeTime = 6;
    private int _lifeTime => UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);

    public event Action<Cube> Removed;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        ResetMaterial();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Platform platform) && _isColorChanged == false)
        {
            SetRandomColor(_renderer);
            StartCoroutine(DestroyInTime(_lifeTime));
        }
    }

    public void TransferVelocity()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private IEnumerator DestroyInTime(int delay)
    {
        yield return new WaitForSeconds(delay);
        Removed?.Invoke(this);
        ResetMaterial();
    }

    private void ResetMaterial()
    {
        _isColorChanged = false;
        _renderer.material = _startMaterial;
    }

    private void SetRandomColor(MeshRenderer meshRenderer)
    {
        int minColorNumber = 0;
        int randomColor = UnityEngine.Random.Range(minColorNumber, _colors.Count);
        meshRenderer.material = _colors[randomColor];
        _isColorChanged = true;
    }
}