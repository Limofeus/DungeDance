using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapContentAnimAppear : MonoBehaviour
{
    [SerializeField] private int levelIdToCheckCompletion;
    [SerializeField] private bool useTagAndAnimate;
    [SerializeField] private string tagName;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float startDelay;
    [SerializeField] private Vector2 animTimeDelay;
    [SerializeField] private bool autoUpdStartScales;
    [SerializeField] private GameObject[] gameObjChain;
    [SerializeField] private GameObject[] gameObjInsta;
    [SerializeField] private GameObject[] objsToHide;
    private Vector3[] gameObjScales;
    void Start()
    {
        if (autoUpdStartScales)
        {
            UpdScales();
        }
        CheckAndAnimate();
    }

    public void CheckAndAnimate()
    {
        if (!IsLevelCompleted())
        {
            Debug.Log($"LevelNotComplete, id: {levelIdToCheckCompletion}");
            SetAllShownHidden(false);
            SetInstaShowHidden(false);
        }
        else
        {
            SetInstaShowHidden(true);
            HideObjsToHide();
            if (useTagAndAnimate && (!SaveHasAnimTag()))
            {
                //animate and add tag
                StartAnimCoroutine();
                AddTagToSaveData();
            }
            else
            {
                SetAllShownHidden(true);
            }
        }
    }

    private bool IsLevelCompleted()
    {
        Debug.Log($"Loading save data in anim script, SD!=null: {MenuDataManager.saveData != null}, dataLoaded: {MenuDataManager.dataLoaded}");
        if(MenuDataManager.saveData != null)
        {
            return MenuDataManager.saveData.levelDatas[levelIdToCheckCompletion].completed;
        }
        else
        {
            return SaveSystem.Load().levelDatas[levelIdToCheckCompletion].completed;
        }
    }
    private bool SaveHasAnimTag()
    {
        if (MenuDataManager.saveData != null)
        {
            return MenuDataManager.saveData.progressTags.ContainsTag(tagName);
        }
        else
        {
            return SaveSystem.Load().progressTags.ContainsTag(tagName);
        }
    }
    private void StartAnimCoroutine()
    {
        SetAllShownHidden(false);
        ApplyScaleCoroutOnObjById(0);
    }
    private void ApplyScaleCoroutOnObjById(int currObj)
    {
        if(currObj < gameObjChain.Length)
            StartCoroutine(ObjScalingCorout(currObj));
    }
    private void AddTagToSaveData()
    {
        if (MenuDataManager.saveData == null)
        {
            MenuDataManager.saveData = SaveSystem.Load();
        }
        MenuDataManager.saveData.progressTags.AddTag(tagName);
        SaveSystem.Save(MenuDataManager.saveData);
    }
    private void UpdScales()
    {
        List<Vector3> scalesList = new List<Vector3>();
        foreach (GameObject gameObj in gameObjChain)
        {
            scalesList.Add(gameObj.transform.localScale);
        }
        gameObjScales = scalesList.ToArray();
    }

    private void SetAllShownHidden(bool isShown)
    {
        foreach (GameObject gameObj in gameObjChain)
        {
            gameObj.SetActive(isShown);
        }
    }

    private void SetInstaShowHidden(bool isShown)
    {
        foreach (GameObject gameObj in gameObjInsta)
        {
            gameObj.SetActive(isShown);
        }
    }

    private void HideObjsToHide()
    {
        if (objsToHide.Length == 0) return;
        foreach (GameObject gameObj in objsToHide)
        {
            gameObj.SetActive(false);
        }
    }

    private void SetObjScaleById(float sclaeCoeff, int objId)
    {
        Vector3 scaleMult = Vector3.one;
        if (autoUpdStartScales)
            scaleMult = gameObjScales[objId];
        gameObjChain[objId].transform.localScale = scaleMult * sclaeCoeff;
    }

    private IEnumerator ObjScalingCorout(int objId)
    {
        Debug.Log($"Corout started on obj id {objId}");
        if (objId == 0)
        {
            yield return new WaitForSeconds(startDelay);
        }
        gameObjChain[objId].SetActive(true);
        float coroutWorkTime = Mathf.Max(animTimeDelay.x, animTimeDelay.y) + 0.5f; //I know that this thing might break if FPS falls under 2 BUT HEAR ME OUT!
        float coroutTimer = 0f;
        bool nextStarted = false;
        while (coroutTimer <= coroutWorkTime) //Actually, I think it shouldnt break this way
        {
            coroutTimer += Time.deltaTime;
            if((coroutTimer >= animTimeDelay.y) && (!nextStarted))
            {
                ApplyScaleCoroutOnObjById(objId + 1);
                nextStarted = true;
            }
            if(coroutTimer < animTimeDelay.x)
            {
                float scaleCoeff = animCurve.Evaluate(Mathf.Clamp01(coroutTimer / animTimeDelay.x));
                SetObjScaleById(scaleCoeff, objId);
            }
            else
            {
                SetObjScaleById(animCurve.Evaluate(1f), objId);
            }
            yield return null;
        }
        yield return null;
    }
}
