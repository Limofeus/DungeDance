using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabSwitcher : MonoBehaviour, NumSelectable
{
    [System.Serializable]
    private class TabContent
    {
        public GameObject[] tabObjects;
    }

    [SerializeField] private TabContent[] tabsContents;
    [SerializeField] private int startTabNum = 0;

    private void Start()
    {
        ChangeTab(startTabNum);
    }

    public void OnNumSelected(int numSelected)
    {
        ChangeTab(numSelected);
    }

    private void ChangeTab(int tabNum)
    {
        for(int i = 0; i < tabsContents.Length; i++)
        {
            bool objectStateSet = i == tabNum;
            foreach(var obj in tabsContents[i].tabObjects)
            {
                obj.SetActive(objectStateSet);
            }
        }
    }
}
