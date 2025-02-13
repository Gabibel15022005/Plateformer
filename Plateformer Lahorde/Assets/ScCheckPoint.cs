using UnityEngine;

public class ScCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Sauvegarde la position du checkpoint
            PlayerPrefs.SetFloat("CheckpointX", transform.position.x);
            PlayerPrefs.SetFloat("CheckpointY", transform.position.y);
            PlayerPrefs.Save();
            Debug.Log("Checkpoint sauvegardé !");
        }
    }
}
