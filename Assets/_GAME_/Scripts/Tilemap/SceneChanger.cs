using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private Animator fadeAnim;
    public float delayTime;
    private Transform playerPos;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerPos = collision.transform;
            fadeAnim.Play("FadeToBlack");
            StartCoroutine(WaitToFade());
        }

        IEnumerator WaitToFade()
        {
            yield return new WaitForSeconds(delayTime);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
