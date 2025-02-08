using System.Collections;
using UnityEngine;

public class ScTeleporter : MonoBehaviour
{
    [SerializeField] private ScTeleporter _teleporter;
    [SerializeField] private float _waitTimeForCamera = 1f;
    [HideInInspector] public bool IsDestination = false;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (IsDestination) return;

        if (other.gameObject.CompareTag("Player"))
        {
            _teleporter.IsDestination = true;

            ScPlayerMovement gameObjectToTp = other.gameObject.GetComponentInParent<ScPlayerMovement>();

            gameObjectToTp.ResetVelocity();
            gameObjectToTp.CancelDash(); 

            gameObjectToTp.gameObject.transform.position = _teleporter.transform.position;
            
            // figer le joueur 
            ChangePlayerState(gameObjectToTp, true);

            StartCoroutine(WaitForCamera(gameObjectToTp));

        }
    }

    private void ChangePlayerState(ScPlayerMovement player, bool freeze)
    {
        if (freeze)
        {
            player.StopPlayer(freeze);                  // Empêche toute interaction
            player.ShowVisuel(false);            // Desactive le visuel
        }
        else
        {
            player.ResetVelocity();
            player.StopPlayer(freeze);                     // Reactive toute interaction
            player.ShowVisuel(true);            // Reactive le visuel
            player.ResetDash();                             // fair regagner le dash à la sortie
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            IsDestination = false;
        }
    }

    private IEnumerator WaitForCamera(ScPlayerMovement player)
    {
        yield return new WaitForSeconds(_waitTimeForCamera);
        
        ChangePlayerState(player, false);
    }
}
