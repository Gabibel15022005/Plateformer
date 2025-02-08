using System.Collections;
using UnityEngine;

public class ScRespawnAfterDestroy : MonoBehaviour
{
    [SerializeField] private GameObject _Object;
    public float RespawnDelay = 5;
    public void Respawn()
    {
        StartCoroutine(RespawnTheObject());
    }

    private IEnumerator RespawnTheObject()
    {
        yield return new WaitForSeconds(RespawnDelay);
        _Object.SetActive(true );
    }
}
