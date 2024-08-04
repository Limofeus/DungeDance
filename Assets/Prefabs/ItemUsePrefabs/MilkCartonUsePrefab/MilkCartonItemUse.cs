using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkCartonItemUse : MonoBehaviour
{
    [SerializeField] private float timeToActivate;
    [SerializeField] private float activeTime;
    [SerializeField] private float timeToDelete;
    [SerializeField] private Animator animator;
    [SerializeField] private int curseIdToPrevent;

    private MainManager mainManager;
    private CurseHandler curseHandler;

private bool antiCurseActive = false;
void Start()
{
    mainManager = MainManager.thisMainManager;
    curseHandler = mainManager.curseHandler;
    StartCoroutine(Use());
}

private void Update()
{
    if (antiCurseActive)
    {
        curseHandler.TryForceSkipCurse(curseIdToPrevent);
    }
}
private IEnumerator Use()
{
    yield return new WaitForSeconds(timeToActivate);
    antiCurseActive = true;

    mainManager.bonusesAndMultiplers.scoreHitType1Bonus -= 0.2f;
    mainManager.bonusesAndMultiplers.scoreHitType2Bonus -= 0.1f;

    yield return new WaitForSeconds(activeTime);
    antiCurseActive = false;
    animator.SetTrigger("ItemUseEnd");


    mainManager.bonusesAndMultiplers.scoreHitType1Bonus += 0.2f;
    mainManager.bonusesAndMultiplers.scoreHitType2Bonus += 0.1f;


    yield return new WaitForSeconds(timeToDelete);
    Destroy(gameObject);
}
}
