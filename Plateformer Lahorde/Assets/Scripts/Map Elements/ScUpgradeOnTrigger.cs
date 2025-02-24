using UnityEngine;

public class ScUpgradeOnTrigger : MonoBehaviour
{
    [SerializeField] private string _upgradeToUnlock;

    void Start()
    {
        if (PlayerPrefs.HasKey(_upgradeToUnlock))
        {
            gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Sauvegarde la position du checkpoint
            PlayerPrefs.SetInt(_upgradeToUnlock, 1);
            PlayerPrefs.Save();
            Debug.Log($"Upgrade {_upgradeToUnlock} Unlocked !");
            gameObject.SetActive(false);
        }
    }
}