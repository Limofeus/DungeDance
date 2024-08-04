using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursePreventItemEffectTrigger : ItemEffectTrigger
{
    private MainManager mainManager;
    [SerializeField] private int[] curseIdsToPrevent;
    [SerializeField] private float preventTime = -1f;
    [SerializeField] private bool deleteWhenDone;

    private void Start()
    {
        mainManager = MainManager.thisMainManager;
    }
    public override void OnTrigger()
    {
        TriggerAnim();
        CursePrevent();
        StartCoroutine(FireNextTrigger());
    }
    private void CursePrevent()
    {
        //Debug.Log("Trying to prevent curse");
        for (int i = 0; i < curseIdsToPrevent.Length; i++)
        {
            mainManager.curseHandler.TryForceSkipCurse(curseIdsToPrevent[i]);
        }
    }

    private IEnumerator FireNextTrigger()
    {
        float timer = 0;
        while(timer < preventTime)
        {
            timer += Time.deltaTime;
            CursePrevent();
            yield return null;
        }
        yield return new WaitForSeconds(nextTriggerDelay);
        nextTrigger?.OnTrigger();
        if (deleteWhenDone)
        {
            Destroy(gameObject);
        }
    }
}
