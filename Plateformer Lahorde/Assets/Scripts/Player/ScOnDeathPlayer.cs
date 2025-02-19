using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScOnDeathPlayer : MonoBehaviour
{
    private ScPlayerMovement _scPlayerMovement;
    private Animator _playerAnimator;
    private Animator _transiAnimator;

    void Start()
    {
        _scPlayerMovement = GetComponentInParent<ScPlayerMovement>();
        _playerAnimator = GetComponent<Animator>();
        _transiAnimator = Camera.main.GetComponentInChildren<Animator>();
    }
    public void OnDeath()
    {
        _scPlayerMovement.StopPlayer(true);
        _playerAnimator.Play("PlayerDeath");
    }

    public void Transition()
    {
        _transiAnimator.Play("EnterTransition");
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(0.26f); // mettre la durée de "EnterTransition" avec un tout petit décalage ou trouver le temp avec une fonction
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
