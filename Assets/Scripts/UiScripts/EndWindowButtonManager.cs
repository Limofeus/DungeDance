using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWindowButtonManager : MonoBehaviour
{
    public Animator blackScreenAnimator;
    const int levelSelectSceneId = 2;
    public void Menu()
    {
        StartCoroutine(LoadScene(levelSelectSceneId));
    }
    public void Retry()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(sceneIndex));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        blackScreenAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneIndex);
    }
}
