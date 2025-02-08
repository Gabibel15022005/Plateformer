using System.Collections;
using UnityEngine;

public class ScChangeRoom : MonoBehaviour
{
    ScCameraSettings _camera;
    [SerializeField] private float _delayChangeZone = 1f;

[Header("Previous Zone")]
    [SerializeField] private Vector2 _oldLimitX;
    [SerializeField] private Vector2 _oldLimitY;

    [Space(20)]

[Header("Next Zone")]
    [SerializeField] private Vector2 _newLimitX;
    [SerializeField] private Vector2 _newLimitY;

    private void Start() {
        _camera = Camera.main.GetComponent<ScCameraSettings>();
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {  
            if (_camera.LimitX == _oldLimitX && _camera.LimitY == _oldLimitY)  
            {
                _camera.StartTransition(_newLimitX, _newLimitY, _delayChangeZone);

            }
            else if (_camera.LimitX == _newLimitX && _camera.LimitY == _newLimitY)
            {
                _camera.StartTransition(_oldLimitX, _oldLimitY, _delayChangeZone);
            }
        }
    }
}
