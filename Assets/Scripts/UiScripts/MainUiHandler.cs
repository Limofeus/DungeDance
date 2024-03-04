using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUiHandler : MonoBehaviour
{
    [SerializeField] private TextMeshPro coinCountText;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro comboText;
    [SerializeField] private TextMeshPro multiplyerText;
    [SerializeField] private TextMeshPro levelNameText;
    [SerializeField] private TextMeshPro songNameText;
    [SerializeField] private Animator coinCountAnimator;
    [SerializeField] private Animator comboUI;
    [SerializeField] private Animator barHolderAnimator;
    [SerializeField] private Animator monsterCounterHolderAnimator;
    [SerializeField] private Animator comboScoreAnimator;
    [SerializeField] private Animator UUHAIHAnimator;
    [SerializeField] private Animator monsterGaugeAnimator;
    public void UpdateLevelname(string levelName, string levelSongName)
    {
        levelNameText.text = levelName;
        songNameText.text = levelSongName;
    }
    public void SetMonsterGaugeShown(bool isShown)
    {
        monsterGaugeAnimator.SetBool("Shown", isShown);
    }
    public void SetScoreText(string text)
    {
        scoreText.text = text;
    }
    public void SetComboText(string text) { comboText.text = text; } //Это не я если что, это автозаполнениепоправление мне так порекомендовало на одной строчке захуярить
    public void SetMultiplierText(string text)
    {
        multiplyerText.text = text;
    }
    public void SetComboUITrigger(string trigger)
    {
        comboUI.SetTrigger(trigger);
    }

    public void UpdateCoinUI(string coinCount)
    {
        coinCountAnimator.SetTrigger("Jump");
        coinCountText.text = coinCount;
    }

    public void HideAllUI()
    {
        barHolderAnimator.SetTrigger("HideForewer");
        monsterCounterHolderAnimator.SetTrigger("HideForewer");
        comboScoreAnimator.SetTrigger("HideForewer");
        UUHAIHAnimator.SetTrigger("HideForewer");
    }
}
