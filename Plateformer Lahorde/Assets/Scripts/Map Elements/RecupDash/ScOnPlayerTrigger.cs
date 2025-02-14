using UnityEngine;

public class ScOnPlayerTrigger : MonoBehaviour
{
    private ScRespawnAfterDestroy _respawn;

    private void Start() 
    {
        _respawn = GetComponentInParent<ScRespawnAfterDestroy>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) 
        {
            other.gameObject.GetComponentInParent<ScPlayerMovement>().IsAbleToDash = true;
            _respawn.Respawn();
            gameObject.SetActive(false);
        }
    }
}
