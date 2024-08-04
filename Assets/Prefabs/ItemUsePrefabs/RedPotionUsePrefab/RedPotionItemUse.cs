using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPotionItemUse : MonoBehaviour
{
    [SerializeField] private int repeats;
    [SerializeField] private float bonusTime;
    [SerializeField] private float tillDeleteTime;
    private MainManager mainManager;
    void Start()
    {
        mainManager = MainManager.thisMainManager;
        StartCoroutine(ItemUse());
    }

    private IEnumerator ItemUse()
    {
        mainManager.bonusesAndMultiplers.scoreMonsterMultiplier += 0.1f;
        for (int i = 0; i < repeats; i++)
        {
            mainManager.ItemAttractionBoost(2, i + 5);
        }
        yield return new WaitForSeconds(bonusTime);
        mainManager.bonusesAndMultiplers.scoreMonsterMultiplier -= 0.1f;
        yield return new WaitForSeconds(tillDeleteTime);
        Destroy(gameObject);
    }
}
