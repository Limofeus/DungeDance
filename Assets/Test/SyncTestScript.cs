using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SyncTestScript : MonoBehaviour
{
    public TextMeshPro offsetText;
    public TextMeshPro timeSinceStartText;
    public TextMeshPro trackTimeText;
    public TextMeshPro beatOldText;
    public TextMeshPro beatNewText;
    public TextMeshPro timeTillBeatOldText;
    public TextMeshPro timeTillBeatNewText;
    public TextMeshPro differenceOldText;
    public TextMeshPro differenceNewText;

    private int arhC = 0;

    public void SetTexts(float offset = 0f, float tss = 0f, float trackTime = 0f, float nbt = 0, int beatNew = 0, float ttbO = 0f, float ttbN = 0f, float difO = 0f, float difN = 0f)
    {
        //Debug.Log(tss);
        offsetText.text = "Offset: " + offset.ToString("0.00000");
        timeSinceStartText.text = "TSS: " + tss.ToString("0.00000");
        trackTimeText.text = "TT: " + trackTime.ToString("0.00000");
        beatOldText.text = "NBT-O: " + nbt.ToString("0.00000");
        beatNewText.text = "B-N: " + beatNew.ToString();
        timeTillBeatOldText.text = "TTB-O: " + ttbO.ToString("0.00000");
        timeTillBeatNewText.text = "TTB-N: " + ttbN.ToString("0.00000");
        differenceOldText.text = "NBT-N: " + difO.ToString("0.00000");
        differenceNewText.text = "ARH-C: " + arhC.ToString();
    }

    public void EmulateOnArrowHit(float arrowTravelTime)
    {
        StartCoroutine(ArrowTravelEmulation(arrowTravelTime));
    }

    private IEnumerator ArrowTravelEmulation(float arrowTravelTime)
    {
        yield return new WaitForSeconds(arrowTravelTime);
        arhC++;
    }
}
