using UnityEngine;

public class ScPauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    private bool isPaused = false;

    void Update()
    {
        bool pressedPause = Input.GetAxisRaw("Pause") > 0;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        _panel.SetActive(isPaused);
    }
}
