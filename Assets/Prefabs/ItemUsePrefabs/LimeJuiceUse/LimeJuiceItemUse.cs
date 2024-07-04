using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LimeJuiceItemUse : MonoBehaviour
{
    public Vector2 downMinMax;
    public Vector2 sideMinMax;
    public float itemUseTime;
    public float bubleSpawnFreq;
    public Volume volume;
    private float startTime;
    private float lerpy;
    private MainManager mainManager;
    [SerializeField] GameObject bubblePrefab;

    private void Start()
    {
        mainManager = MainManager.thisMainManager;
        startTime = Time.time;
        //mainManager.ItemAttractionBoost(20, itemUseTime);

        mainManager.bonusesAndMultiplers.scoreAllHitBonus += 10f;
        mainManager.bonusesAndMultiplers.scoreAllHitMultiplier += 0.5f;
        mainManager.bonusesAndMultiplers.joyDynamicHitMultiplier += 0.3f;
        StartCoroutine(Use());
    }

    private IEnumerator Use()
    {
        while (Time.time < startTime + 1f)
        {
            lerpy = Time.time - startTime;
            volume.weight = lerpy;
            //transform.localScale = Vector3.Lerp(Vector3.one * 5, Vector3.one, lerpy);
            yield return new WaitForEndOfFrame();
        }
        volume.weight = 1f;
        //transform.localScale = Vector3.one;
        float bubbleTimer = 0f;
        int bubleCounter = 0;
        while (bubbleTimer < itemUseTime - 2f)
        {
            if(bubleCounter < Mathf.FloorToInt(bubbleTimer / bubleSpawnFreq))
            {
                bubleCounter++;
                Instantiate(bubblePrefab, new Vector3(Random.Range(sideMinMax.x, sideMinMax.y), Random.Range(downMinMax.x, downMinMax.y), 0f), Quaternion.identity);
            }
            bubbleTimer += Time.deltaTime;
            yield return null;
        }
        startTime = Time.time;
        while (Time.time < startTime + 1f)
        {
            lerpy = (startTime + 1f) - Time.time;
            volume.weight = lerpy;
            //transform.localScale = Vector3.Lerp(Vector3.one * 5, Vector3.one, lerpy);
            yield return new WaitForEndOfFrame();
        }
        mainManager.bonusesAndMultiplers.scoreAllHitBonus -= 10f;
        mainManager.bonusesAndMultiplers.scoreAllHitMultiplier -= 0.5f;
        mainManager.bonusesAndMultiplers.joyDynamicHitMultiplier -= 0.3f;
        Destroy(gameObject);
    }
}
