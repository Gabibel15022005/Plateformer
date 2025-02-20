using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScGoToNextLevel : MonoBehaviour
{
    [SerializeField] string _levelToGo;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject);

            Camera.main.GetComponentInChildren<Animator>().Play("EnterTransition");
            collision.gameObject.GetComponentInParent<ScPlayerMovement>().StopPlayer(true);
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(_levelToGo);
    }
}
