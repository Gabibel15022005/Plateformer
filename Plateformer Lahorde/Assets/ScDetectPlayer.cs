using UnityEngine;

public class ScDetectPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _visuelRange;
    private bool _playerIsInRange = false;
    private bool _playerUsedThis = false;
    [SerializeField] private float _grabForce;
    private ScPlayerMovement _player;

    private void Start() 
    {
        _visuelRange.SetActive(false);
    }

    private void Update() 
    {
        if (_playerIsInRange)
        {
            if (_player.GetGrabInput() && !_playerUsedThis)
            {
                Debug.Log("Grabbed");
                _playerUsedThis = true;
                _player.SetGrabbed(true);

                _player.CancelDash();
                _player.ResetVelocity();
                _player.ResetDash();

                Vector3 direction = new Vector3(
                transform.position.x - _player.gameObject.transform.position.x,
                transform.position.y - _player.gameObject.transform.position.y,
                0);

                _player.Rb.linearVelocity = direction.normalized * _grabForce;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _visuelRange.SetActive(true);
            _playerIsInRange = true;
            _player = collision.transform.parent.GetComponent<ScPlayerMovement>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _visuelRange.SetActive(false);
            _player.SetGrabbed(false);

            if (_playerUsedThis == true)
            {
                _player.Rb.linearVelocity /= 2;
            }

            _playerUsedThis = false;
            _playerIsInRange = false;

            _player = null;
        }
    }
}
