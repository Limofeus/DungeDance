using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScene : MonoBehaviour
{
    public Animator buttonHolder;
    public Animator clickToStart;
    public Animator logoAnimator;
    public Animator mainHolderAnimator;
    public Animator nameWindowAnimator;
    //public AudioSource musicSource;
    public AudioVolumeMultiply audioVolumeMultiply;
    public NicknameSelectionWindow nicknameSelectionWindow;
    public bool startTime = false;
    public bool uiShown;
    public bool canHide = false;
    public bool firstTimeOpen;
    private bool takesButtons= true;
    private bool settingsOpen;

    private void Start()
    {
        StartCoroutine(ShowLogo());
        nicknameSelectionWindow.enabledd = false;
        if(DiscordIntegrator.working)
            DiscordIntegrator.UpdateActivity("Preparing to get in to this...", "It'll be fire!", "disdunge", "At the enterance");
        UpdateVolume();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!startTime)
            {
                buttonHolder.SetBool("Shown", true);
                clickToStart.SetTrigger("Hide");
                startTime = true;
                uiShown = true;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (!settingsOpen)
            {
                if(startTime && canHide)
                {
                    HideShowUI();
                }
            }
            else
            {
                HideSettings();
            }
        }
    }
    public void UpdateVolume()
    {
        //Debug.Log("UpdatingVol by " + SaveSystem.Load().settingsData.musicVolume.ToString());
        //musicSource.volume = SaveSystem.Load().settingsData.musicVolume; // FFS this thing wont work cus of animations, welp Imma apply it to audio listener then... WHAT YOU CANT APPLY IT TO LISTENER????
        audioVolumeMultiply.volumeB = SaveSystem.Load().settingsData.musicVolume;
    }
    public void HideShowUI()
    {
        if (uiShown)
        {
            uiShown = false;
            buttonHolder.SetBool("Shown", false);
            logoAnimator.SetBool("Shown", false);
        }
        else
        {
            uiShown = true;
            buttonHolder.SetBool("Shown", true);
            logoAnimator.SetBool("Shown", true);
        }
    }
    public void StartGame()
    {
        if (takesButtons)
        {
            if (!firstTimeOpen)
            {
                takesButtons = false;
                StartCoroutine(StartGameCourutine());
                canHide = false;
                buttonHolder.SetBool("Shown", false);
                logoAnimator.SetBool("Shown", false);
                mainHolderAnimator.SetTrigger("EnterDungeon");
            }
            else
            {
                nameWindowAnimator.SetBool("opened", true);
                nicknameSelectionWindow.enabledd = true;
                takesButtons = false;
                canHide = false;
                buttonHolder.SetBool("Shown", false);
                logoAnimator.SetBool("Shown", false);
            }
        }
    }
    public void NewGameStart()
    {
        nameWindowAnimator.SetBool("opened", false);
        nicknameSelectionWindow.enabledd = false;
        StartCoroutine(StartGameCourutine());
        mainHolderAnimator.SetTrigger("EnterDungeon");
    }
    public void Quit()
    {
        if (takesButtons)
        {
            ActuallyQuit();
        }
    }
    public void ActuallyQuit()
    {
        takesButtons = false;
        StartCoroutine(QuitCourutine());
        canHide = false;
        buttonHolder.SetBool("Shown", false);
        logoAnimator.SetBool("Shown", false);
        mainHolderAnimator.SetTrigger("Quit");
    }
    public void Settings()
    {
        //Debug.Log("Fufa");
        if (takesButtons)
        {
            takesButtons = false;
            canHide = false;
            settingsOpen = true;
            mainHolderAnimator.SetBool("Settings", true);
            buttonHolder.SetBool("Shown", false);
            logoAnimator.SetBool("Shown", false);
        }
    }
    public void HideSettings()
    {
        takesButtons = true;
        canHide = true;
        settingsOpen = false;
        mainHolderAnimator.SetBool("Settings", false);
        buttonHolder.SetBool("Shown", true);
        logoAnimator.SetBool("Shown", true);
    }
    IEnumerator QuitCourutine()
    {
        yield return new WaitForSeconds(1.1f);
        Application.Quit();
        yield return null;
    }
    IEnumerator StartGameCourutine()
    {
        yield return new WaitForSeconds(2.1f);
        MapMusicPlayer.menuTrackMoment = audioVolumeMultiply.audioSource.time;
        SceneManager.LoadScene(1);
        yield return null;
    }
    IEnumerator ShowLogo()
    {
        yield return new WaitForSeconds(1.7f);
        uiShown = true;
        canHide = true;
        logoAnimator.SetBool("Shown", true);
        yield return null;
    }
}
