using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScCreateLevelButton : MonoBehaviour
{
    [SerializeField] private List<string> _sceneName;
    [SerializeField] private Button buttonPrefab;

    void Start()
    {
        foreach (string scene in _sceneName)
        {
            Button button = Instantiate(buttonPrefab);
            button.gameObject.name = scene;
            button.gameObject.GetComponentInChildren<TMP_Text>().text = scene;
            button.gameObject.GetComponent<ScButtonLevel>().SceneName = scene;

            button.transform.SetParent(transform);
            button.transform.localScale = new Vector3(1,1,1);
        }
    }
}
