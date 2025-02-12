using UnityEngine;

public class ScParallaxEffect : MonoBehaviour
{
    private Transform _cameraTransform; // Référence à la caméra
    public float ParallaxFactor = 0.5f; // Plus il est bas, plus le mouvement est lent
    private Vector3 _lastCameraPosition;

    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _lastCameraPosition = _cameraTransform.position;
    }

    void Update()
    {
        Vector3 deltaMovement = _cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * ParallaxFactor, deltaMovement.y * ParallaxFactor, 0);
        _lastCameraPosition = _cameraTransform.position;
    }
}
