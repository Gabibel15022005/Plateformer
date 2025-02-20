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
            
            PlayerPrefs.SetFloat("LimitX.x", _newLimitX.x);
            PlayerPrefs.SetFloat("LimitX.y", _newLimitX.y);
            PlayerPrefs.SetFloat("LimitY.x", _newLimitY.x);
            PlayerPrefs.SetFloat("LimitY.y", _newLimitY.y);
            PlayerPrefs.Save();

        }
    }


}
