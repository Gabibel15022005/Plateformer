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
        ResetCheckPoint();
        ResetPlayerPrefs(); // fonction pour effacer la progression du joueur
        StartCoroutine(GoToScene("Level 1"));
    }
    public void Continue()
    {
        if (PlayerPrefs.HasKey("LevelMaxActuelle"))
        {
            Debug.Log("Continue");
            StartCoroutine(GoToScene($"Level {PlayerPrefs.GetInt("LevelMaxActuelle")}"));
            ResetCheckPoint();
        }
        else 
        {
            NewGame();
        }
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
        
        PlayerPrefs.DeleteKey("LevelMaxActuelle");      // reset le niveau max atteint
        PlayerPrefs.DeleteKey("Dash");        // reset les améliorations débloquer
        PlayerPrefs.Save();

        Debug.Log("ResetPlayerPrefs has been Preformed");
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
