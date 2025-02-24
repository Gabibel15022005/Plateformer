using System.Collections;
using UnityEngine;

public class ScDestroyParticleAfter : MonoBehaviour
{
    [SerializeField] private float _delay = 1f; 
    void Start()
    {
        StartCoroutine(DestroyAfter());
    }

    private IEnumerator DestroyAfter()
    {
        yield return new WaitForSeconds(_delay);
        Destroy(gameObject);
    }
}
