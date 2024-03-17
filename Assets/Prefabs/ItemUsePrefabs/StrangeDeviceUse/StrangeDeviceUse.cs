using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class StrangeDeviceUse : MonoBehaviour
{
    public float itemUseTime;
    public Volume volume;
    private float startTime;
    private float lerpy;
    private MainManager mainManager;

    private void Start()
    {
        mainManager = MainManager.thisMainManager;
        startTime = Time.time;
        mainManager.ItemAttractionBoost(20, itemUseTime);
        //mainManager.ScoreBonusMultiplier(0.6f, itemUseTime); //rework needed
        //mainManager.AddStatsEffect(5f, 0f, 0.6f, 0f, 0f, 0f);
        mainManager.bonusesAndMultiplers.scoreAllHitMultiplier += 0.6f;
        mainManager.bonusesAndMultiplers.joyDynamicHitMultiplier += 0.2f;
        StartCoroutine(Use());
    }

    private IEnumerator Use()
    {
        while(Time.time < startTime + 1f)
        {
            lerpy = Time.time - startTime;
            volume.weight = lerpy;
            transform.localScale = Vector3.Lerp(Vector3.one * 5, Vector3.one, lerpy);
            yield return new WaitForEndOfFrame();
        }
        volume.weight = 1f;
        transform.localScale = Vector3.one;
        yield return new WaitForSeconds(itemUseTime - 2f);
        startTime = Time.time;
        while (Time.time < startTime + 1f)
        {
            lerpy = (startTime + 1f) - Time.time;
            volume.weight = lerpy;
            transform.localScale = Vector3.Lerp(Vector3.one * 5, Vector3.one, lerpy);
            yield return new WaitForEndOfFrame();
        }
        mainManager.bonusesAndMultiplers.scoreAllHitMultiplier -= 0.6f;
        mainManager.bonusesAndMultiplers.joyDynamicHitMultiplier -= 0.2f;
        Destroy(gameObject);
    }
}
