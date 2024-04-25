using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningAltar : MonoBehaviour
{
    [SerializeField] private GeneralizedItemIcon[] playerItemIcons;
    private int selectedItemId;
    public SaveData saveData;
    void Start()
    {
        if (MenuDataManager.saveData != null)
            saveData = new SaveData(MenuDataManager.saveData);
        else
            saveData = SaveSystem.Load();

        playerItemIcons[0].UpdateItem(saveData.item1Id, 0, "ЛКМ - выбрать");
    }
    public void TestMethod(int id)
    {
        Debug.Log("Recieved event");
        playerItemIcons[id].HideShowItem(!playerItemIcons[id].isShown);
    }

    void Update()
    {
        
    }
}
