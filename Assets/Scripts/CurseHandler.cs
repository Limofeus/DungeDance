using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CurseHandler : MonoBehaviour
{
    [SerializeField] private bool cursesEnabled;
    public Curse[] ñurses;
    [HideInInspector] public int CurseCounter;
    [HideInInspector] public int FullCurseCounter;
    [SerializeField] private CurseWarningUI curseWarningUI;
    [SerializeField] private GameObject[] curseVisualPrefabs; //Buy thats a sneaky one
    [SerializeField] private Volume curseVolumeObj;
    [SerializeField] private VolumeProfile shadowCurseVolumeProfile;
    [SerializeField] private VolumeProfile rageCurseVolumeProfile;
    [HideInInspector] public bool preCursed { get; private set; }
    [HideInInspector] public bool fullCursed { get; private set; }
    [HideInInspector] public int currentCurseId { get; private set; } = -1; //0 - Shadow, 1 - Rage
    private CurseVisual currentCurseVisual;

    [SerializeField] private MainManager _mainManager;

    private string randomDebugLine = "Hey hi hello, I'm very very PRIVATE!!!";
    public void ToggleCurse(bool enabled)
    {
        cursesEnabled = enabled;
    }
    public void DisableCurseEffects()
    {
        CastRageCurse(false);
        CastShadowCurse(false);
    }
    public void TryForceSkipCurse(int curseId)
    {
        if(currentCurseId == curseId)
        {
            if (preCursed)
            {
                preCursed = false;
                CurseCounter++;
                if (fullCursed)
                {
                    fullCursed = false;
                    CastCurse(false);
                }
                FullCurseCounter++;
            }
            else
            {
                if (fullCursed)
                {
                    fullCursed = false;
                    CastCurse(false);
                    FullCurseCounter++;
                }
            }
        }
    }
    public void HandleCurses(float timer, float timeToGo)
    {
        if (!cursesEnabled) return;

        if (CurseCounter < ñurses.Length)
        {
            if (!preCursed)
            {
                if (timer + timeToGo > ñurses[CurseCounter].StartTime)
                {
                    preCursed = true;
                    //Debug.Log("CUSE TO TRUE!");
                    currentCurseId = ñurses[CurseCounter].CurseId;
                }
            }
            else
            {
                if (timer + timeToGo > ñurses[CurseCounter].EndTime)
                {
                    preCursed = false;
                    //Debug.Log("CUSE TO FALSE!");
                    CurseCounter++;
                }
            }
        }
        if (FullCurseCounter < ñurses.Length)
        {
            if (!fullCursed)
            {
                if (timer > ñurses[FullCurseCounter].StartTime)
                {
                    fullCursed = true;
                    CastCurse(true);
                }
            }
            else
            {
                if (timer > ñurses[FullCurseCounter].EndTime)
                {
                    fullCursed = false;
                    CastCurse(false);
                    FullCurseCounter++;
                }
            }
        }
    }
    void CastCurse(bool CastDecast)
    {
        switch (currentCurseId)
        {
            case 0:
                CastShadowCurse(CastDecast);
                break;
            case 1:
                CastRageCurse(CastDecast);
                break;
            default:
                Debug.Log("Wrong curse ID");
                break;
        }
        //Creating prefab for the curse
        if (CastDecast)
        {
            currentCurseVisual = Instantiate(curseVisualPrefabs[currentCurseId], Vector3.zero, Quaternion.identity).GetComponent<CurseVisual>();
            currentCurseVisual.StartCurse();
        }
        else
        {
            currentCurseVisual.EndCurse(); //IT SHOULD DESTROY ITSELF HERE!!!
        }
    }
    void CastShadowCurse(bool CastDecast)
    {
        curseWarningUI.UpdateCurse(0, CastDecast, "curse_warning_id0");
        if (CastDecast)
        {
            _mainManager.bonusesAndMultiplers.followMonsterToJoyMultiplier -= 0.9f;
            _mainManager.bonusesAndMultiplers.followMonsterToScoreMultiplier -= 0.9f;
            _mainManager.soundSource.PlayCurseSound(0);
            StartCoroutine(ShadowCurse(1f));
            if (_mainManager.MonsterComp != null)
                _mainManager.MonsterComp.AddCurse(0, true);
        }
        else
        {
            _mainManager.bonusesAndMultiplers.followMonsterToJoyMultiplier += 0.9f;
            _mainManager.bonusesAndMultiplers.followMonsterToScoreMultiplier += 0.9f;
            StartCoroutine(ShadowCurse(0f));
            if (_mainManager.MonsterComp != null)
                _mainManager.MonsterComp.AddCurse(0, false);
        }
    }
    void CastRageCurse(bool CastDecast)
    {
        curseWarningUI.UpdateCurse(1, CastDecast, "curse_warning_id1");
        if (CastDecast)
        {
            _mainManager.bonusesAndMultiplers.joyAllHitMultiplier += 0.15f;
            _mainManager.bonusesAndMultiplers.joyHitType0Multiplier += 0.5f;
            _mainManager.bonusesAndMultiplers.scoreAllHitMultiplier += 0.15f;
            _mainManager.bonusesAndMultiplers.scoreHitType0Multiplier += 0.5f;

            _mainManager.soundSource.PlayCurseSound(1);
            StartCoroutine(RageCurse(1f));
            if (_mainManager.MonsterComp != null)
                _mainManager.MonsterComp.AddCurse(1, true);
        }
        else
        {
            _mainManager.bonusesAndMultiplers.joyAllHitMultiplier -= 0.15f;
            _mainManager.bonusesAndMultiplers.joyHitType0Multiplier -= 0.5f;
            _mainManager.bonusesAndMultiplers.scoreAllHitMultiplier -= 0.15f;
            _mainManager.bonusesAndMultiplers.scoreHitType0Multiplier -= 0.5f;

            StartCoroutine(RageCurse(0f));
            if (_mainManager.MonsterComp != null)
                _mainManager.MonsterComp.AddCurse(1, false);
        }
    }
    public IEnumerator ShadowCurse(float final)
    {
        float LocalTimer = 0f;
        if (final == 1f)
            curseVolumeObj.profile = shadowCurseVolumeProfile;
        while (LocalTimer < 1f)
        {
            curseVolumeObj.weight = Mathf.Lerp(curseVolumeObj.weight, final, 2f * Time.deltaTime);
            LocalTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator RageCurse(float final)
    {
        float LocalTimer = 0f; ;
        if (final == 1f)
            curseVolumeObj.profile = rageCurseVolumeProfile;
        if (final == 1f)
            StartCoroutine(_mainManager.ChangeArrowSpeed(0.2f, 6f));
        else
            StartCoroutine(_mainManager.ChangeArrowSpeed(0.2f, -6f));
        while (LocalTimer < 1f)
        {
            curseVolumeObj.weight = Mathf.Lerp(curseVolumeObj.weight, final, 2f * Time.deltaTime);
            LocalTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
