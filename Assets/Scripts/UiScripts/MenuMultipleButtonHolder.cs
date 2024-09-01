using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMultipleButtonHolder : MonoBehaviour
{
    //https://ibb.co/C2btj9T
    public GameObject[] buttons;
    public int buttonsUnlocked;
    private Vector3 startPos;
    void Start()
    {
        startPos = transform.localPosition;
        foreach(GameObject button in buttons)
        {
            button.SetActive(false);
        }
        UpdateButtonCount(buttonsUnlocked);
    }

    public void UpdateButtonCount(int buttonCount)
    {
        buttonsUnlocked = buttonCount;
        transform.localPosition = new Vector3(startPos.x - (buttonsUnlocked - 1), startPos.y, startPos.z);
        for(int i = 1; i <= buttons.Length; i++)
        {
            if(i <= buttonsUnlocked)
            {
                buttons[i - 1].SetActive(true);
            }
        }
    }
}
