using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    private const int AmountOfLocalScaleAxis = 3;

    [SerializeField] private float _defaultExplosionRadius = 5f;
    [SerializeField] private float _defaultExplosionForce = 150f;
    // [SerializeField] private ParticleSystem _effect;

    public void Explode()
    {
        List<Rigidbody> explodableObjects = GetExplodableObjects();

        foreach (Rigidbody explodableObject in explodableObjects)
        {
            explodableObject.AddExplosionForce(GetExplosionForce(), transform.position, GetExplosionRadius());
        }
        
        Debug.Log("explosion has happened");
    }

    private float CalculateAverageCoefficient()
    {
        return (transform.localScale.x + transform.localScale.y + transform.localScale.z) / AmountOfLocalScaleAxis;
    }

    private float GetExplosionForce()
    {
        return _defaultExplosionForce / CalculateAverageCoefficient();
    }

    private float GetExplosionRadius()
    {
        return _defaultExplosionRadius / CalculateAverageCoefficient();
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _defaultExplosionRadius);

        List<Rigidbody> objectsToExplode = new();

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                objectsToExplode.Add(hit.attachedRigidbody);
            }
        }

        return objectsToExplode;
    }
}
