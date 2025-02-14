using UnityEngine;

public class ScRespawn : MonoBehaviour
{
    void Start()
    {
        // Vérifie si un checkpoint a été sauvegardé
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            float x = PlayerPrefs.GetFloat("CheckpointX");
            float y = PlayerPrefs.GetFloat("CheckpointY");

            transform.position = new Vector3(x, y, transform.position.z);
            Debug.Log("Joueur respawn au dernier checkpoint !");
        }
    }

}
