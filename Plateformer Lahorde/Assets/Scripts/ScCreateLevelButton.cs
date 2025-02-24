using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScCreateLevelButton : MonoBehaviour
{
    [SerializeField] private List<string> _sceneName;
    [SerializeField] private Button buttonPrefab;

    [Header("Button Color when Unlocked")]
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor;
    [SerializeField] private Color pressedColor;
    [SerializeField] private Color selectedColor;
    private int levelNb = 0;

    void Start()
    {
        foreach (string scene in _sceneName)
        {
            levelNb += 1;
            Button button = Instantiate(buttonPrefab);
            button.gameObject.name = scene;
            button.gameObject.GetComponentInChildren<TMP_Text>().text = scene;
            button.gameObject.GetComponent<ScButtonLevel>().SceneName = scene;

            button.transform.SetParent(transform);
            button.transform.localScale = new Vector3(1,1,1);

            
            if (PlayerPrefs.HasKey("LevelMaxActuelle"))
            {
                if (levelNb <= PlayerPrefs.GetInt("LevelMaxActuelle"))
                {
                    button.gameObject.GetComponent<ScButtonLevel>().LevelUnlocked = true;

                    ColorBlock colors = button.colors;
                    colors.normalColor = normalColor;
                    colors.highlightedColor = highlightedColor;
                    colors.pressedColor = pressedColor;
                    colors.selectedColor = selectedColor;
                    
                    button.colors = colors;
                }
            }
        }
    }
}
