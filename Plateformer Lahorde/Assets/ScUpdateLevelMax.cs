using UnityEngine;

public class ScUpdateLevelMax : MonoBehaviour
{
    [SerializeField] private int _levelNb;
    void Start()
    {
        if (!PlayerPrefs.HasKey("LevelMaxActuelle"))
        {
            PlayerPrefs.SetInt("LevelMaxActuelle",_levelNb);
        }
        else if (_levelNb > PlayerPrefs.GetInt("LevelMaxActuelle"))
        {
            PlayerPrefs.SetInt("LevelMaxActuelle",_levelNb);
        }
    }
}
