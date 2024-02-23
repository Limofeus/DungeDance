using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndWindow : MonoBehaviour
{
    public MainManager mainManager;
    public GameObject deathSprite;
    public GameObject completeSprite;
    public Transform xpBar;
    public Transform xpBarHolder;
    public Animator endWindowAnimator;
    public string[] deathMessages;
    public string[] completeMessages;
    public TextMeshPro popup;
    public TextMeshPro scoreText;
    public TextMeshPro playerLevelText;
    public TextMeshPro playerNextLevelText;
    public TextMeshPro remainingXp;
    public TextMeshPro money;
    public TextMeshPro levelName;
    public TextMeshPro songName;
    public TextMeshPro maxCombo;
    public TextMeshPro hitCountM;
    public TextMeshPro hitCountN;
    public TextMeshPro hitCountG;
    public TextMeshPro hitCountL;
    public float levelUpAnimationSpeed;
    public float shakeMagnitude;
    public float shakeMagnitudeLimit;
    public float shakeThreshold;
    private float xpBarShakeLerpStrenght;
    private Vector3 xpBarShakePosition;
    private Vector3 xpBarScaleTargert;
    private int[] rankXps;
    private int playerXp;
    private int playerStartXp;
    private int playerLevel;
    private int playerStartLevel;
    private int playerAnimatedLevel;
    private string xpleftName;
    private string levelupName;

    private void Start()
    {
        xpleftName = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_xpleft");
        levelupName = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_levelup");
    }
    private void Update()
    {
        xpBar.localPosition = Vector3.Lerp(xpBar.localPosition, xpBarShakePosition, 10f * Time.deltaTime);
        xpBarShakePosition = Vector3.Lerp(xpBarShakePosition, Vector3.zero, xpBarShakeLerpStrenght * Time.deltaTime);
        xpBar.localScale = Vector3.Lerp(xpBar.localScale, xpBarScaleTargert, 4f * Time.deltaTime);
    }
    private void AssignParameters(bool debug, SaveData saveData)
    {
        mainManager = MainManager.thisMainManager;
        if (!debug)
            rankXps = MenuDataManager.rankXps;
        else
            rankXps = mainManager.debugPlayerLevelupXps;

        playerXp = saveData.playerXp;
        playerStartXp = mainManager.playerStartXp;

        levelName.text = mainManager.levelName;
        songName.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_music") + mainManager.songName;

        maxCombo.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_maxcombo") + (mainManager.maxCombo).ToString();

        hitCountM.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_hittype_1") + (mainManager.hitCounts[0]).ToString();
        hitCountN.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_hittype_2") + (mainManager.hitCounts[1]).ToString();
        hitCountG.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_hittype_3") + (mainManager.hitCounts[2]).ToString();
        hitCountL.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_hittype_4") + (mainManager.hitCounts[3]).ToString();

        playerLevel = CalculateLevel(playerXp);
        playerStartLevel = CalculateLevel(playerStartXp);
        playerAnimatedLevel = playerStartLevel;

        scoreText.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_label_score") + (playerXp - playerStartXp).ToString();
        money.text = (saveData.moneyAmount).ToString();
    }
    public void EndWindowStats(bool dead, bool debug, SaveData saveData)
    {   
        AssignParameters(debug, saveData);
        Debug.Log("Here");
        StartCoroutine(LevelUpAnimation());
        if (dead)
        {
            deathSprite.SetActive(true);
            popup.text = LocalisationSystem.GetLocalizedValue(deathMessages[Random.Range(0, deathMessages.Length)]);
        }
        else
        {
            completeSprite.SetActive(true);
            popup.text = LocalisationSystem.GetLocalizedValue(completeMessages[Random.Range(0, completeMessages.Length)]);
        }
        if(Random.value < 0.0001f)
        {
            popup.text = LocalisationSystem.GetLocalizedValue("ui_endwindow_splash_secret");
        }
    }
    public IEnumerator LevelUpAnimation() //<- That one will be hard. Ill do it tomorrow tho
    {
        /*
        int playerAnimatedXp = (playerAnimatedLevel == playerStartLevel) ? playerStartXp : rankXps[playerAnimatedLevel];
        int playerAnimatedFinalXp = (playerAnimatedLevel == CalculateLevel(playerXp)) ? playerXp : rankXps[playerAnimatedLevel + 1];
        playerLevelText.text = CalculateLevel(playerAnimatedXp).ToString();
        playerNextLevelText.text = (CalculateLevel(playerAnimatedXp) + 1).ToString();
        int minLevelXp = rankXps[CalculateLevel(playerAnimatedXp)];
        int maxLevelXp = rankXps[CalculateLevel(playerAnimatedXp) + 1];
        int curretLevelXp = maxLevelXp - minLevelXp;
        int curretAnimatedXp = playerAnimatedXp - minLevelXp;
        int levelUpLeftXp = maxLevelXp - playerAnimatedXp;
        float animationValue = ;
        float speed = levelUpAnimationSpeed;
        while(animationValue < 1f)
        {

            levelUpLeftXp = maxLevelXp - playerAnimatedXp;
         ------ Spent 4 hours on this, but this wont work... ------
        */
        Debug.Log(rankXps[1].ToString() + " | " + playerXp.ToString());
        int playerAnimatedXp = playerStartXp;
        int levelXp;
        float stepFloat;
        float speed = levelUpAnimationSpeed;
        float barScale = 0;
        float startBarScale;
        Debug.Log("Then Here ( " + playerAnimatedXp.ToString() + " | " + playerXp.ToString() + " )");
        barScale = (float)(playerAnimatedXp - rankXps[playerAnimatedLevel]) / (rankXps[playerAnimatedLevel + 1] - rankXps[playerAnimatedLevel]);
        startBarScale = barScale;
        xpBarScaleTargert = Vector3.one;
        xpBarHolder.localScale = new Vector3(barScale, 1f, 1f);
        Debug.Log("This thingy ( " + barScale.ToString() + " | " + playerAnimatedXp.ToString() + " | " + rankXps[playerAnimatedLevel + 1].ToString() + " | " + rankXps[playerAnimatedLevel].ToString() + " )");
        remainingXp.text = xpleftName + (rankXps[playerStartLevel + 1] - playerStartXp).ToString();
        playerLevelText.text = (playerAnimatedLevel + 1).ToString();
        playerNextLevelText.text = (playerAnimatedLevel + 2).ToString();
        yield return new WaitForSeconds(1f);
        while (playerAnimatedXp < playerXp)
        {
            Debug.Log("And Here");
            levelXp = rankXps[playerAnimatedLevel + 1] - rankXps[playerAnimatedLevel];
            stepFloat = speed * Time.deltaTime;
            barScale += stepFloat;
            playerAnimatedXp = Mathf.RoundToInt(Mathf.Lerp(rankXps[playerAnimatedLevel], rankXps[playerAnimatedLevel + 1], barScale));
            if (playerAnimatedXp >= rankXps[playerAnimatedLevel + 1])
            {
                xpBarShakeLerpStrenght = 3f;
                speed *= 1.4f;
                startBarScale = 0f;
                playerAnimatedLevel += 1;
                barScale = 0;
                xpBarScaleTargert = Vector3.one;
                xpBarHolder.localScale = new Vector3(1.2f, 1f, 1f);
                Debug.Log("May be Here");
                remainingXp.text = levelupName;
                endWindowAnimator.SetTrigger("LevelUp");
                playerLevelText.text = (playerAnimatedLevel + 1).ToString();
                playerNextLevelText.text = (playerAnimatedLevel + 2).ToString();
                yield return new WaitForSeconds(1f);
            }
            playerLevelText.text = (playerAnimatedLevel + 1).ToString();
            playerNextLevelText.text = (playerAnimatedLevel + 2).ToString();
            xpBarHolder.localScale = new Vector3(barScale, 1f, 1f);
            float shakeStrenght = barScale - startBarScale;
            if (xpBarShakePosition.magnitude < shakeMagnitude * shakeThreshold * shakeStrenght)
            {
                xpBarShakePosition = new Vector3(Mathf.Clamp(Random.Range(-shakeMagnitude * shakeStrenght, shakeMagnitude * shakeStrenght),-shakeMagnitudeLimit, shakeMagnitudeLimit), Mathf.Clamp(Random.Range(-shakeMagnitude * shakeStrenght, shakeMagnitude * shakeStrenght), -shakeMagnitudeLimit, shakeMagnitudeLimit), 0);
            }
            xpBarShakeLerpStrenght = shakeStrenght * 20f;
            xpBarScaleTargert = new Vector3(1f + (0.12f * shakeStrenght), 1f + (0.12f * shakeStrenght), 1f);
            remainingXp.text = xpleftName + (rankXps[playerAnimatedLevel+1] - playerAnimatedXp).ToString();
            //Debug.Log(shakeStrenght);
            yield return new WaitForEndOfFrame();
        }
        xpBarShakeLerpStrenght = 3f;
        xpBarScaleTargert = Vector3.one;
        barScale = (float)(playerAnimatedXp - rankXps[playerAnimatedLevel]) / (rankXps[playerAnimatedLevel + 1] - rankXps[playerAnimatedLevel]);
        xpBarHolder.localScale = new Vector3(barScale, 1f, 1f);
        remainingXp.text = xpleftName + (rankXps[playerLevel + 1] - playerXp).ToString();
        Debug.Log("After Here ( " + rankXps[playerLevel + 1] + " | " + playerLevel + " )");
        yield return null;
    }

    private int CalculateLevel(int xp)
    {
        int i = 0;
        while (xp >= rankXps[i] && i < rankXps.Length)
        {
            i++;
        }
        //Debug.Log("Returned: " + i.ToString() + " for: " + xp.ToString() + "cus of: " + rankXps[i].ToString());
        return i - 1;
    }

}
