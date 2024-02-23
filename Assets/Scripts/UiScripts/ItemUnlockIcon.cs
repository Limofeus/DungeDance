using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUnlockIcon : MonoBehaviour
{
    public Color[] rarityColours;
    public SpriteRenderer sprite;
    public SpriteRenderer background;
    public GameObject questionMark;

    public void SetupItemUnlock(int itemId)
    {
        sprite.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.sprite = ItemSpriteDictionary.itemSprites[itemId];
        background.color = rarityColours[ItemSpriteDictionary.itemRarity[itemId]];
    }
    public void SetUnlockment(int Unlockment)
    {
        switch (Unlockment)
        {
            case 0:
                questionMark.SetActive(true);
                sprite.color = Color.black;
                break;
            case 1:
                questionMark.SetActive(false);
                //Debug.Log("Hmmm");
                sprite.color = new Color(0.15f, 0.15f, 0.15f, 1f);
                //sprite.color = new Color(1f, 1f, 1f, 1f);
                break;
            case 2:
                questionMark.SetActive(false);
                //sprite.color = Color.white;
                sprite.color = new Color(0.9f, 0.9f, 0.9f, 1f);
                break;
        }
    }
}
