using UnityEngine;

public class ScKillPlayerOnCollid : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) 
    {

        if (other.gameObject.CompareTag("Player")) 
        {
            other.gameObject.GetComponentInChildren<ScOnDeathPlayer>().OnDeath();
        }

    }
}
