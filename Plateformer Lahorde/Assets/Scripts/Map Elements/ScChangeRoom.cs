using System.Collections;
using UnityEngine;

public class ScChangeRoom : MonoBehaviour
{
    ScCameraSettings _camera;
    [SerializeField] private float _delayChangeZone = 1f;

[Header("Next Zone")]
    [SerializeField] private Vector2 _newLimitX;
    [SerializeField] private Vector2 _newLimitY;

    private void Start() {
        _camera = Camera.main.GetComponent<ScCameraSettings>();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (_camera.LimitX != _newLimitX || _camera.LimitY != _newLimitY)
        {
            _camera.StartTransition(_newLimitX, _newLimitY, _delayChangeZone);
        }
    }


}
