using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningAltar : MonoBehaviour
{
    private int selectedItemId;
    public SaveData saveData;
    void Start()
    {
        if (MenuDataManager.saveData != null)
            saveData = new SaveData(MenuDataManager.saveData);
        else
            saveData = SaveSystem.Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
