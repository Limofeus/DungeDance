using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BottomTextHandler : MonoBehaviour
{
    [SerializeField] private MainManager _mainManager;
    [SerializeField] private TextMeshPro textGuiTitle;
    [SerializeField] private TextMeshPro textGuiText;
    [SerializeField] private Animator downUI;
    private bool dialogueMode;
    private int dialogueCounter;
    private float dialogueLineLength;
    public void SetDialogueParams(bool dialMode, int dialCounter, float dialLineLen)
    {
        dialogueMode = dialMode;
        dialogueCounter = dialCounter;
        dialogueLineLength = dialLineLen;
    }
    public void HandleDialogue()
    {
        if(!dialogueMode)
            return;
        int dialLen = _mainManager.GetCurrentHorde().npcLines.Length;
        bool dialRand = _mainManager.hordes[_mainManager.hordeCounter].npcLines.Length == _mainManager.hordes[_mainManager.hordeCounter].npcLinesRandomizer.Length;
        if (dialogueCounter < dialLen)
        {
            if ((dialogueCounter + 1) * dialogueLineLength < _mainManager.RMaxTime - _mainManager.RTime)
            {
                dialogueCounter++;
                if (!dialRand || _mainManager.GetCurrentHorde().npcLinesRandomizer[dialogueCounter] < 2)
                    DisplayText(GetNPCNameById(_mainManager.GetCurrentHorde().npcKind), LocalisationSystem.GetLocalizedValue(_mainManager.GetCurrentHorde().npcLines[dialogueCounter]));
                else
                    DisplayText(GetNPCNameById(_mainManager.GetCurrentHorde().npcKind), LocalisationSystem.GetLocalizedValue(_mainManager.GetCurrentHorde().npcLines[dialogueCounter] + "_r" + Random.Range(1, _mainManager.GetCurrentHorde().npcLinesRandomizer[dialogueCounter] + 1).ToString()));
            }
        }
        else
        {
            ChangeUIType(false);
            dialogueMode = false;
        }
    }
    public string GetNPCNameById(int npcId)
    {
        //Debug.Log(npcId);
        return LocalisationSystem.GetLocalizedValue("npc_name_id" + npcId.ToString());
    }
    public void DisplayItemText(int itemId)
    {
        DisplayText(LocalisationSystem.GetLocalizedValue("item_name_id" + itemId.ToString()), LocalisationSystem.GetLocalizedValue("item_desc_id" + itemId.ToString()));
    }
    public void DisplayText(string title, string text)
    {
        textGuiTitle.text = title;
        //textGuiText.text = text;
        textGuiText.text = "";
        StartCoroutine(SmoothShowText(text));
        ChangeUIType(true);
    }
    public void HideBottomUI()
    {
        downUI.SetTrigger("HideForewer");
    }
    public void ChangeUIType(bool TextUI)
    {
        downUI.SetBool("TextUI", TextUI);
    }

    public IEnumerator SmoothShowText(string text)
    {
        bool skipArgument = false;
        foreach (char ch in text)
        {
            textGuiText.text += ch;
            if (ch == '<')
                skipArgument = true;
            if (ch == '>')
                skipArgument = false;
            if (!skipArgument)
                yield return new WaitForSeconds(0.025f);
        }
        yield return null;
    }
}
