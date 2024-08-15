using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapLevelManager : MonoBehaviour
{
    public Animator menuAnimator;
    [SerializeField] private int storageSceneId;
    [SerializeField] private int menuSceneId;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            ToMenu();
    }
    public void LoadLevel(int levelId)
    {
        MapMusicPlayer.mapMusicPlayer.FadeOut();
        menuAnimator.SetTrigger("Fade");
        StartCoroutine(ChangeScene(levelId));
    }
    public void ToMenu()
    {
        MapMusicPlayer.mapMusicPlayer.FadeOut();
        menuAnimator.SetTrigger("Fade");
        StartCoroutine(ChangeScene(menuSceneId));
    }
    public void ToStorage()
    {
        MapMusicPlayer.mapMusicPlayer.FadeOut();
        menuAnimator.SetTrigger("Fade");
        StartCoroutine(ChangeScene(storageSceneId)); //FUUUUUCKKKK, MAYBE I CAN SOMEHOW FIX IT BY NOT UNLOADING THIS ONEEEADAWDAWDD
    }
    IEnumerator ChangeScene(int levelId)
    {
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(levelId);
    }
    /*
    IEnumerator AdditiveLoadScene(int levelId)
    {
        yield return new WaitForSeconds(1.1f); //Sure this shit is fun BUT IT DOES NOT WORK LIKE I NEED IT TO!
        SceneManager.LoadScene(levelId,LoadSceneMode.Additive);
    }
    */
}
