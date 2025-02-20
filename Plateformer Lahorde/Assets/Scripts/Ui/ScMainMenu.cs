using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour
{
    public void GoToSceneTest()
    {
        Debug.Log("Pressed Play Button");
        StartCoroutine(GoToSceneTest_Co());

        ResetPlayerPrefs();
    }
    private IEnumerator GoToSceneTest_Co()
    {
        yield return new WaitForSeconds(0.75f);
        Debug.Log("Execute Play Button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // METTRE le niveau 1
    }

    public void Quit()
    {
        Debug.Log("Pressed Quit Button");
        StartCoroutine(Quit_Co());
    }
    private IEnumerator Quit_Co()
    {
        yield return new WaitForSeconds(0.75f);
        Debug.Log("Execute Quit Button");
        Application.Quit();
    }

    private void ResetPlayerPrefs()
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
            
            Debug.Log("Checkpoint Reset !");
        }
    }
}
