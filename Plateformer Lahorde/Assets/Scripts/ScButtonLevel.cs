using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScButtonLevel : MonoBehaviour
{
    [HideInInspector] public string SceneName;
    
    // transi
    // go to the scene
    public void GoToSelectedSelectLevel()
    {
        Debug.Log("Selected Level : ");
        Camera.main.GetComponentInChildren<Animator>().Play("EnterTransition");
        StartCoroutine(GoToScene(SceneName));
        ResetCheckPoint();
    }

    private IEnumerator GoToScene(string sceneName)
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(sceneName);
    }

    private void ResetCheckPoint()
    {
        // Reset les PlayerPrefs du joueur ,  EN GROS  un bouton nouvelle partie
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            PlayerPrefs.DeleteKey("CheckpointX");
            PlayerPrefs.DeleteKey("CheckpointY");
            PlayerPrefs.DeleteKey("LimitX.x");
            PlayerPrefs.DeleteKey("LimitX.y");
            PlayerPrefs.DeleteKey("LimitY.x");
            PlayerPrefs.DeleteKey("LimitY.y");
            PlayerPrefs.Save();
        }
    }
}
