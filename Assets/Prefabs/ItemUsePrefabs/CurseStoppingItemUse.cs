using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CurseStoppingItemUse : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int curseIdToStop;
    [SerializeField] private float stopCurseOffset;
    [SerializeField] private float timeUntilDeletion;
    private MainManager mainManager;
    private CurseHandler curseHandler;
    void Start()
    {
        mainManager = MainManager.thisMainManager;

        //CurseHandler callMeEncapsulationGOD = (CurseHandler)(typeof(MainManager).GetField("curseHandler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(mainManager));
        //string AchievingFullEncapsulation = (string)(typeof(CurseHandler).GetField("randomDebugLine", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(callMeEncapsulationGOD));
        //curseHandler = callMeEncapsulationGOD;
        curseHandler = mainManager.curseHandler;

        //Debug.Log($"RDL: {AchievingFullEncapsulation}");
        StartCoroutine(StopCurseAndDelete());
    }

    private IEnumerator StopCurseAndDelete()
    {
        yield return new WaitForSeconds(stopCurseOffset);
        curseHandler.TryForceSkipCurse(curseIdToStop);
        yield return new WaitForSeconds(timeUntilDeletion - stopCurseOffset);
        Destroy(gameObject);
    }
}
