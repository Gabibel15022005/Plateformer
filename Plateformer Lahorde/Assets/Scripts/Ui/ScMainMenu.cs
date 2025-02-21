using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _selectPanel;
    public void NewGame()
    {
        Debug.Log("NewGame");
        StartCoroutine(GoToScene("Level 1"));
        ResetCheckPoint();
        ResetPlayerPrefs(); // fonction pour effacer la progression du joueur
    }
    public void Continue()
    {
        Debug.Log("Continue");
        StartCoroutine(GoToScene("Level 1"));
        ResetCheckPoint();
    }
    public void ActiveSelectPanel()
    {
        if (_selectPanel.activeSelf)
        {
            _selectPanel.SetActive(false);
        }
        else
        {
            _selectPanel.SetActive(true);
        }
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

    private void ResetPlayerPrefs()
    {
        // Reset les PlayerPrefs du joueur ,  EN GROS  un bouton nouvelle partie
        if (PlayerPrefs.HasKey("CheckpointX"))
        {
            PlayerPrefs.DeleteKey("LevelMaxActuelle");      // reset le niveau max atteint

            PlayerPrefs.DeleteKey("HasDashUpgrade");        // reset les améliorations débloquer
            PlayerPrefs.DeleteKey("HasWallJumpUpgrade");
            PlayerPrefs.DeleteKey("HasGrabUpgrade");

            PlayerPrefs.Save();
        }
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
}
