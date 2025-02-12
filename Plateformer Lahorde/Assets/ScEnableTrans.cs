using UnityEngine;

public class ScEnableTrans : MonoBehaviour
{
    // THIS SCRIPT IS ONLY FOR A BETTER EDITOR 
    // Remove this script and enable the transtion gameobject for the build
    [SerializeField] private GameObject transition;

    void Start()
    {
        transition.SetActive(true);
    }
}
