using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class ScCameraSettings : MonoBehaviour
{
    public Vector2 LimitX; // x = minX / y = maxX
    public Vector2 LimitY; 

    [SerializeField] private ScPlayerMovement player;
    [SerializeField] private float _cameraSpeed;
    private bool _isTransitioning = false;
    private Vector2 _targetLimitX;
    private Vector2 _targetLimitY;

    void Start()
    {
        if (PlayerPrefs.HasKey("LimitX.x"))
        {
            LimitX.x = PlayerPrefs.GetFloat("LimitX.x");
            LimitX.y = PlayerPrefs.GetFloat("LimitX.y");
            LimitY.x = PlayerPrefs.GetFloat("LimitY.x");
            LimitY.y = PlayerPrefs.GetFloat("LimitY.y");
        }
        
        _targetLimitX = LimitX;
        _targetLimitY = LimitY;
    }
    
    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {

        // Transition fluide des limites si une transition est en cours
        if (_isTransitioning)
        {
            LimitX = Vector2.Lerp(LimitX, _targetLimitX, _cameraSpeed * Time.deltaTime);
            LimitY = Vector2.Lerp(LimitY, _targetLimitY, _cameraSpeed * Time.deltaTime);

            if (transform.position.x < LimitX.x)
            transform.position = new Vector3(LimitX.x, transform.position.y, transform.position.z);
            else if (transform.position.x > LimitX.y)
            transform.position = new Vector3(LimitX.y, transform.position.y, transform.position.z);
            if (transform.position.y < LimitY.x)
            transform.position = new Vector3(transform.position.x, LimitY.x, transform.position.z);
            else if (transform.position.y > LimitX.y)
            transform.position = new Vector3(transform.position.x, LimitY.y, transform.position.z);
        }

        // Appliquer les limites actuelles avec une interpolation
        float clampedX = Mathf.Clamp(player.transform.position.x, LimitX.x, LimitX.y);
        float clampedY = Mathf.Clamp(player.transform.position.y, LimitY.x, LimitY.y);

        transform.position = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, transform.position.z), _cameraSpeed * Time.deltaTime);

    }
    public void StartTransition(Vector2 limitx, Vector2 limity, float duration)
    {
        StartCoroutine(SmoothTransition(limitx, limity, duration));
    }
    public IEnumerator SmoothTransition(Vector2 newLimitX, Vector2 newLimitY, float duration)
    {
        _isTransitioning = true;
        _targetLimitX = newLimitX;
        _targetLimitY = newLimitY;
        player.StopPlayer(true);

        yield return new WaitForSeconds(duration);
        LimitX = newLimitX;
        LimitY= newLimitY;
        _isTransitioning = false;
        player.StopPlayer(false);
        player.ResetVelocity();
    }
}
