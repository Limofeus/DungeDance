using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseWindow : MonoBehaviour
{
    public Animator pauseWindowAnimator;
    public Animator blackScreenAnimator;
    public PauseSummoner pauseSummoner;
    public AudioSource musicSource;
    private bool animating;
    void Start()
    {
        musicSource = MainManager.thisMainManager.musicSource;
        musicSource.Pause();
        Time.timeScale = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Time.timeScale);
        if (Input.GetKeyDown(KeyCode.Escape))
            Continue();
    }
    public void Continue()
    {
        if (!animating)
        {
            musicSource.UnPause();
            animating = true;
            Time.timeScale = 1f;
            StartCoroutine(RemoveWindow());
        }
    }
    public void Retry()
    {
        animating = true;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadScene(sceneIndex));
    }
    public void Menu()
    {
        animating = true;
        StartCoroutine(LoadScene(1));
    }

    IEnumerator LoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        blackScreenAnimator.SetTrigger("Fade");
        yield return new WaitForSeconds(0.5f);
        MainManager.Arrows.Clear();
        SceneManager.LoadScene(sceneIndex);
    }

    IEnumerator RemoveWindow()
    {
        pauseWindowAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.25f);
        pauseSummoner.paused = false;
        Destroy(gameObject);
    }
}
