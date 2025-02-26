using UnityEngine;

public class ScRebondOnCollision : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 15f; // Force du rebond
    [SerializeField] private float _ajustX = 2f; // ajustement pour l'axe X
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            ScPlayerMovement playerScript = other.gameObject.GetComponentInParent<ScPlayerMovement>();

            playerScript.CancelDash();
            playerScript.ResetDash();
            playerScript.Bounce();

            Vector2 direction = playerScript.gameObject.transform.position - transform.position;

            playerScript.Rb.linearVelocity = new Vector2(direction.x * _ajustX, direction.y).normalized * _bounceForce;
            
        }

    }

}

