using UnityEngine;
using UnityEngine.SceneManagement;

public class ScMainMenu : MonoBehaviour
{
    public void GoToSceneTest()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
