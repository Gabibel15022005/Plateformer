using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    private bool isPaused = false;

    public void MainMenu()
    {
        StartCoroutine(GoToScene("Main Menu"));
        TogglePause();
    }
    public void Retry()
    {
        StartCoroutine(GoToScene(SceneManager.GetActiveScene().name));
        TogglePause();
    }
    public void Resume()
    {
        TogglePause();
    }
    private IEnumerator GoToScene(string sceneName)
    {
        yield return new WaitForSeconds(0.75f);
        SceneManager.LoadScene(sceneName);
    }
    public void TogglePause()
    {
        Debug.Log("Pause Toogled");
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        _panel.SetActive(isPaused);
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
