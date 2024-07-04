using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BongoItemUse : MonoBehaviour
{
    public GameObject[] newArrowPrefabs;
    void Start()
    {
        StartCoroutine(DeleteAfterTime()); 
        Monster currentMonster = MainManager.thisMainManager.MonsterComp;
        if (currentMonster == null) return;
        if (!currentMonster.arrowChangeBlock)
        {
            currentMonster.ArrowPrefabs = newArrowPrefabs;
        }
        else
        {
            Debug.Log("Arrow change blocked");
        }
    }

    IEnumerator DeleteAfterTime()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
