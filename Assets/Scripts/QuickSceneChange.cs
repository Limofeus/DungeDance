using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickSceneChange : MonoBehaviour
{
    [SerializeField] private int defaultSceneId;
    [SerializeField] private bool preloadScene;
    [SerializeField] private Animator blackScreenAnimator;
    [SerializeField] private GameObject additionalMessageObj;
    [SerializeField] private string additionalMessage;
    private AsyncOperation asyncLoad;


    private void Start()
    {
        if (preloadScene)
        {
            asyncLoad = SceneManager.LoadSceneAsync(defaultSceneId);
            asyncLoad.allowSceneActivation = false;
        }
    }
    public void ChangeScene()
    {
        ChangeSceneById(-1);
    }
    public void ChangeSceneById(int sceneId)
    {
        if (sceneId < 0) sceneId = defaultSceneId;
        if (preloadScene)
        {
            StartCoroutine(AnimateAndExit());
        }
    }

    IEnumerator AnimateAndExit()
    {
        blackScreenAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.45f);
        if(additionalMessageObj != null)
        {
            additionalMessageObj.SendMessage(additionalMessage);
        }
        asyncLoad.allowSceneActivation = true;
    }
}
