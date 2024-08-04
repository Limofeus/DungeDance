using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelSelector : MonoBehaviour
{
    public bool selected = false;
    public Animator animator;
    public MapLevelManager mapLevelManager;
    public string levelName;
    public int levelRequired;
    public int lengthSeconds;
    public float difficulty;
    public string musicNameAndAuthor;
    public AudioClip mapPlayerMusic;
    public float mapMusicPlayerTime;
    public int backgroundId; // For discord rich presence
    public int levelId;
    public Transform itemUnlocksHolder;
    public Transform difficultyMaskHolder;
    public int[] itemUnlocksIds;
    public GameObject itemUnlockPrefab;
    public GameObject itemUnlocksBorders;
    public GameObject completedText;
    public GameObject notCompletedText;
    public TextMeshPro levelNameText;
    public TextMeshPro lengthText;
    public TextMeshPro personalBestText;
    public TextMeshPro levelRequiredText;
    public TextMeshPro musicText;
    public float betweenItemDistance;
    private ItemUnlockIcon[] itemUnlocks;
    private int[] itemUnlockDataTemp;
    private bool itemUnlockDataNeedsUpdate;
    private float itemUnlocksLength;
    private bool moreThenFour;
    private bool movinRight;
    [SerializeField] private float uiLockValueCheck = 0f;
    // Start is called before the first frame update
    void Start()
    {
        levelNameText.text = levelName;
        difficultyMaskHolder.localScale = new Vector3(difficulty / 5f, 1f, 1f);
        if(lengthSeconds % 60 < 10)
            lengthText.text = LocalisationSystem.GetLocalizedValue("ui_levelselect_length") + (lengthSeconds / 60).ToString() + ":0" + (lengthSeconds % 60);
        else
            lengthText.text = LocalisationSystem.GetLocalizedValue("ui_levelselect_length") + (lengthSeconds / 60).ToString() + ":" + (lengthSeconds % 60);
        levelRequiredText.text = levelRequired.ToString();
        musicText.text = LocalisationSystem.GetLocalizedValue("ui_levelselect_music") + musicNameAndAuthor;
        InstantiateItemUnlocks();
        DistributeItemUnlocks();
        if (itemUnlocks.Length > 4)
        {
            moreThenFour = true;
            itemUnlocksBorders.SetActive(true);
        }
        else
            moreThenFour = false;
    }
    /*
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 30), "MBD: " + Input.GetMouseButtonDown(0).ToString());
        GUI.Label(new Rect(0, 30, 100, 30), "TCL: " + Input.touches.Length.ToString());
        GUI.Label(new Rect(0, 60, 100, 30), "PHA: " + (Input.touches.Length>0?Input.touches[0].phase.ToString() : "NoSuch"));
        GUI.Label(new Rect(0, 90, 100, 30), "PHT: " + (Input.touches.Length > 0 ? (Input.touches[0].phase == TouchPhase.Began ? "TRUE" : "FAL"): "FAL"));
        GUI.Label(new Rect(0, 120, 100, 30), "FUL: " + (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began).ToString());
        GUI.Label(new Rect(0, 150, 100, 30), "SEL: " + selected.ToString());
    }
    */

    // Update is called once per frame
    void Update()
    {
        if (itemUnlockDataNeedsUpdate)
        {
            itemUnlockDataNeedsUpdate = false;
            for (int i = 0; i < itemUnlocks.Length; i++)
            {
                //Debug.Log("questionMark case" + itemUnlockDataTemp[itemUnlocksIds[i]].ToString() + "  " + gameObject.name);
                itemUnlocks[i].SetUnlockment(itemUnlockDataTemp[itemUnlocksIds[i]]);
            }
        }
        //Debug.Log("TouchCount: " + Input.touches.Length.ToString() + " FirstTouchPhaze: " + (Input.touches.Length > 0?Input.touches[0].phase.ToString():"NoSuch"));
        if((Input.GetMouseButtonDown(0)/* || (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)*/) && selected)
        {
            int[] itemUnlockDatas = MenuDataManager.saveData.itemUnlockDatas; //Welp, this was the problem all along
            for (int i = 0; i < itemUnlocksIds.Length;i++)
            {
                if (itemUnlockDatas[itemUnlocksIds[i]] == 0)
                    MenuDataManager.saveData.itemUnlockDatas[itemUnlocksIds[i]] = 1;
            }
            SaveSystem.Save(MenuDataManager.saveData);
            if (DiscordIntegrator.working)
                DiscordIntegrator.UpdateActivity("DANCEtroing those monsters!", "On " + levelName + " stage!", "disbg" + backgroundId.ToString(), "DUNGE DACE!", "dif" + Mathf.Floor(difficulty).ToString(), "STAGE DIFFICULTY! O_O");
            mapLevelManager.LoadLevel(levelId);
        }
        if(moreThenFour)
        {
            if (movinRight)
            {
                if (itemUnlocksHolder.localPosition.x - (itemUnlocksLength / 2f) - 0.5f > -1.75f)
                    movinRight = false;
                else
                    itemUnlocksHolder.localPosition = itemUnlocksHolder.localPosition + (Vector3.right * Time.deltaTime);
            }
            else
            {
                if (itemUnlocksHolder.localPosition.x + (itemUnlocksLength / 2f) + 0.5f < 1.75f)
                    movinRight = true;
                else
                    itemUnlocksHolder.localPosition = itemUnlocksHolder.localPosition + (Vector3.left * Time.deltaTime);
            }
        }
    }

    private void OnMouseEnter()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        //Debug.Log($"MDMuiLock: {MenuDataManager.uiLockValue}, CurrSEL: {gameObject.name}, ButnLock: {uiLockValueCheck}");
        animator.SetBool("MouseOver", true);
        MapMusicPlayer.mapMusicPlayer.MouseOverTrack(true, mapPlayerMusic, mapMusicPlayerTime);
        selected = true;
    }
    private void OnMouseExit()
    {
        if (MenuDataManager.uiLockValue > uiLockValueCheck) return;
        animator.SetBool("MouseOver", false);
        MapMusicPlayer.mapMusicPlayer.MouseOverTrack(false, mapPlayerMusic, mapMusicPlayerTime);
        selected = false;
    }
    public void InstantiateItemUnlocks()
    {
        itemUnlocks = new ItemUnlockIcon[itemUnlocksIds.Length];
        for(int i = 0; i < itemUnlocksIds.Length; i++)
        {
            itemUnlocks[i] = Instantiate(itemUnlockPrefab, Vector3.zero, Quaternion.identity, itemUnlocksHolder).GetComponent<ItemUnlockIcon>();
            itemUnlocks[i].SetupItemUnlock(itemUnlocksIds[i]);
        }
    }
    public void DistributeItemUnlocks()
    {
        itemUnlocksLength = betweenItemDistance * (itemUnlocks.Length - 1);
        for(int i = 0; i < itemUnlocks.Length; i++)
        {
            itemUnlocks[i].transform.localPosition = Vector3.right * ((betweenItemDistance * i) - (itemUnlocksLength / 2f));
        }
    }
    public void UpdateLevelData(LevelData levelData)
    {
        if (levelData.completed)
        {
            completedText.SetActive(true);
            notCompletedText.SetActive(false);
        }
        else
        {
            completedText.SetActive(false);
            notCompletedText.SetActive(true);
        }
        personalBestText.text = LocalisationSystem.GetLocalizedValue("ui_levelselect_personal_best") + levelData.maxScore.ToString();
        //Debug.Log("Level updated: " + levelData.ToString() + " " + maxScoreInt.ToString());
    }
    public void UpdateItemUnlockment(int[] itemUnlockmentData)
    {
        itemUnlockDataTemp = itemUnlockmentData;
        itemUnlockDataNeedsUpdate = true;
    }
}
