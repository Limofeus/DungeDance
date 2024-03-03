using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using System.Linq;

public class MainManager : MonoBehaviour
{
    public static float followTime = 15f;
    //public static int danceStyle = 0; // 0 - Default; 1 - Intence...
    private DanceStyle[] selectedDanceStyles = new DanceStyle[1] {new DefaultDanceStyle()};
    //public int playerStartMoney; // <- sussy amogass 1 (wont use for now)
    [HideInInspector] public int playerStartXp { get; private set; }
    public int playerAttraction;
    public int baseAttraction;
    public int maxBaseAttraction;
    public int monsterBonusAttraction;
    public int maxMonsterBonusAttraction; // <- Welp, wont use this one
    public int bonusAttraction;
    public int maxBonusAttraction;
    public bool overAttractive;
    private bool itemBonusAttr;
    public int maxCombo = 0;
    public int[] hitCounts;
    private int score = 0;
    private int combo = 0;
    private int greatComboCount = 0;
    private float multiplier = 1f;
    private bool greatCombo = true;
    public Horde[] hordes;
    [SerializeField] private CurseHandler curseHandler;
    private int hordeCounter;
    public int MonstersInHorde;
    private int MonsterHordeCounter;
    public MonsterCounter MonsterCounter;
    public bool NotSpawnArrows;
    public bool NoTimeout;
    public bool NoAfterLocationChange;
    public Transform Bar;
    public Material BarMat;
    public SpriteRenderer BarRen;
    public int effectPower;
    public float Joy;
    public float curretMaxJoy;
    public float RTime;
    public float RMaxTime;
    public float MaxBarScale;
    public GameObject Camera;
    public GameObject bigPP;
    public bool disableMoving;
    public bool disableItemUse;
    public bool AutoMod;
    public bool DisableSpawn;
    public bool disableTimer;
    public string levelName;
    public string songName;
    public int levelId;
    public AudioSource musicSource;
    public AudioClip Music;
    public float BPM;
    public float Offset;
    public float AroowSpeed;
    public float AMiSM;
    public float AMaSM;
    public Transform Spawner;
    public float SpawnerOffset;
    public GameObject[] ArrowPrefabs;
    public float ArrowChance = 1f;
    public float TimeBetweenBeats;
    public float TimeToGo;
    private bool dialogueMode;
    private int dialogueCounter;
    private int lastMultiplier = 1;
    public int followMonstersCount = 0;
    private float dialogueLineLength;
    float NextBeatTime;
    public Location Location;
    public bool characterYes;
    public GameObject Character;
    public static GameObject publicCharacter;
    public static MainManager thisMainManager;
    private Character CharacComp;
    public GameObject Monster;
    public Monster MonsterComp;
    public Transform EnimyHolder;
    public Transform allUiHolder;
    public Transform rateHalder;
    public GameObject NextMonster;
    private Monster nextMonsterComp;
    public GameObject[] MoansterPrefabs;
    public GameObject[] ratePrefabs;
    public MonsterTypes MonsterTypes;
    public bool ItemSpawned;
    private bool monsterSpawnedAttraction;
    public static Color crazycolor1;
    public static Color crazycolor2;
    public Color CC1;
    public Color CC2;
    public SoundSource soundSource;
    public MonsterFollow MonsterFollow;
    public SpeedChangeVisual speedChangeVisual;
    public GameObject TreasurePrefab;
    public GameObject npcPrefab;
    public GameObject BlackScreen;
    public GameObject endScreenPrefab;
    public ItemHolder itemHolder;
    public TextMeshPro textGuiTitle;
    public TextMeshPro textGuiText;
    public TextMeshPro coinCountText;
    public TextMeshPro scoreText;
    public TextMeshPro comboText;
    public TextMeshPro multiplyerText;
    public TextMeshPro attractionText;
    public TextMeshPro levelNameText;
    public TextMeshPro songNameText;
    public Animator attractionAnimator;
    public Animator coinCountAnimator;
    public Animator downUI;
    public Animator comboUI;
    public Animator barHolderAnimator;
    public Animator monsterCounterHolderAnimator;
    public Animator comboScoreAnimator;
    public Animator UUHAIHAnimator;
    public Animator monsterGaugeAnimator;
    public SpriteRenderer monsterGaugeSR;
    public Sprite[] monsterGaugeSprites;
    public Material attractionTextPink;
    public Material attractionTextGreen;
    public Material attractionTextBlue;
    public Material Effect1;
    public Material Effect2;
    private AudioSource audioSource;
    //float ThisArrowTime;
    public static List<GameObject> Arrows = new List<GameObject>();
    public float Timer;

    //Debug
    public bool useDebugPlayerData;
    public SaveData debugPlayerData; // Does save money and values after the end of the level?
    public int[] debugPlayerLevelupXps;

    //MOBILE STUFF
    public bool mobileInput;
    private int screenW;
    private int screenH;
    private int touchZone;

    //BONUSES AND MULTIPLIERS
    //Score bonuses
    [HideInInspector] public float scoreMonsterBonus = 0f; //This one is for when the monster gives player score, not tied to follows
    [HideInInspector] public float scoreHitType0Bonus = 0f;
    [HideInInspector] public float scoreHitType1Bonus = 0f;
    [HideInInspector] public float scoreHitType2Bonus = 0f;
    [HideInInspector] public float scoreHitType3Bonus = 0f;
    [HideInInspector] public float scoreAllHitBonus = 0f;
    //Score multipliers
    [HideInInspector] public float scoreMonsterMultiplier = 1f; //Same as above
    [HideInInspector] public float scoreHitType0Multiplier = 1f;
    [HideInInspector] public float scoreHitType1Multiplier = 1f;
    [HideInInspector] public float scoreHitType2Multiplier = 1f;
    [HideInInspector] public float scoreHitType3Multiplier = 1f;
    [HideInInspector] public float scoreAllHitMultiplier = 1f;
    //Joy bonuses
    [HideInInspector] public float joyHitType0Bonus = 0f;
    [HideInInspector] public float joyHitType1Bonus = 0f;
    [HideInInspector] public float joyHitType2Bonus = 0f;
    [HideInInspector] public float joyHitType3Bonus = 0f;
    [HideInInspector] public float joyAllHitBonus = 0f;
    //Joy multipliers
    [HideInInspector] public float joyHitType0Multiplier = 1f;
    [HideInInspector] public float joyHitType1Multiplier = 1f;
    [HideInInspector] public float joyHitType2Multiplier = 1f;
    [HideInInspector] public float joyHitType3Multiplier = 1f;
    [HideInInspector] public float joyAllHitMultiplier = 1f;
    [HideInInspector] public float joyDynamicHitMultiplier = 1f;
    //Some other stat changes
    [HideInInspector] public float followMonsterToScoreMultiplier = 1f;
    [HideInInspector] public float followMonsterToJoyMultiplier = 1f;
    [HideInInspector] public float attractionToScoreMultiplier = 1f;
    [HideInInspector] public float attractionToJoyMultiplier = 1f;

    void Awake()
    {
        //Debug.Log(useDebugPlayerData);
        //This was in void Start() but i put it here while trying to fix one very strange and annoying, one and only THE BUG
        hitCounts = new int[4];
        //!!itemSprites = assignItemSprites;
        thisMainManager = this;
        publicCharacter = Character;
        ItemSpawned = false;
        NoTimeout = true;
        NotSpawnArrows = true;
        levelNameText.text = levelName;
        songNameText.text = songName;
        crazycolor1 = CC1;
        hordeCounter = 0;
        crazycolor2 = CC2;
        Timer = -3f;
        MonsterHordeCounter = 0;
        TimeBetweenBeats = 60f / BPM;
        SyncTempoAnimator.speedForAnimator = 1f / TimeBetweenBeats;
        NextBeatTime = Offset;
    }

    void Start()
    {
        //Mobile st
        if (mobileInput)
        {
            screenH = Screen.height;
            screenW = Screen.width;
            touchZone = Mathf.RoundToInt(screenW / 3.75f);
        }
        //mobile ed
        CharacComp = Character.GetComponent<Character>();
        Location.SendMessage("SkipStart");
        audioSource = this.GetComponent<AudioSource>();
        audioSource.clip = Music;

        if (characterYes)
            CharacComp.Init(TimeBetweenBeats);

        if (curseHandler != null)
            curseHandler.Initialize(this);

        int item1Id;
        int item2Id;
        int item3Id;
        //Debug.Log(useDebugPlayerData);
        if (!useDebugPlayerData)
        {
            playerStartXp = MenuDataManager.saveData.playerXp;  
            item1Id = MenuDataManager.saveData.item1Id;
            item2Id = MenuDataManager.saveData.item2Id;
            item3Id = MenuDataManager.saveData.item3Id;
            //Debug.Log("PLEAEESEE!!");
            ChangeStageVolume(MenuDataManager.saveData.settingsData.soundVolume, MenuDataManager.saveData.settingsData.musicVolume);
        }
        else
        {
            playerStartXp = debugPlayerData.playerXp;
            item1Id = debugPlayerData.item1Id;
            item2Id = debugPlayerData.item2Id;
            item3Id = debugPlayerData.item3Id;
            itemHolder.noAutosave = true;
            //Debug.Log("FUUUCK!!");
            ChangeStageVolume(debugPlayerData.settingsData.soundVolume, debugPlayerData.settingsData.musicVolume);
        }
        /*
        Debug.Log(MenuDataManager.saveData.playerName);
        Debug.Log(MenuDataManager.saveData.moneyAmount);
        Debug.Log(MenuDataManager.saveData.item1Id);
        Debug.Log(MenuDataManager.saveData.item2Id);
        Debug.Log(MenuDataManager.saveData.item3Id);
        Debug.Log(item1Id);
        Debug.Log(item2Id);
        Debug.Log(item3Id);
        */
        if (item1Id != -1)
        {
            itemHolder.AddItem(item1Id);
            if (item2Id != -1)
            {
                itemHolder.AddItem(item2Id);
                if (item3Id != -1)
                {
                    itemHolder.AddItem(item3Id);

                }
            }
        }

        BlackScreen.GetComponent<Animator>().SetTrigger("Starto!");
        StartCoroutine(CountDown());
    }

    public void StartLevel()
    {

        MonsterHordeCounter = -1;
        CalculateNextMonster();
        MonsterHordeCounter = 0;
        OnNewLocation();

        attractionText.text = playerAttraction.ToString();
        NoTimeout = false;
        NotSpawnArrows = false;
        audioSource.Play();
        UpdateMonsterCounter();
    }

    void Update()
    {
        TimeToGo = SpawnerOffset / AroowSpeed;
        if(!disableTimer)
            Timer += Time.deltaTime;
        if (mobileInput)
            MobileYeeemput();
        else
            Yeeemput();
        //if (NextBeatTime < Time.time - (SpawnerOffset / AroowSpeed)) <- is this the thing that causes THE BUG?
        //AND YES IT FUCKING WAS!!!!!!!!!!!!!
        //P.S. for some unknown reason Time.time resets on scene change in editor but still goes in build. How the fuck was i suppost to know this?
        if (NextBeatTime < Timer - (SpawnerOffset / AroowSpeed))
        {
            NextBeatTime += TimeBetweenBeats;
            Beat();
        }

        curseHandler.HandleCurses(Timer, TimeToGo);

        if (dialogueMode)
            HandleDialogue();
        if (!NoTimeout)
            RTime += - Time.deltaTime;
        Bar.localScale = new Vector3(MaxBarScale * (RTime / RMaxTime), Bar.localScale.y, Bar.localScale.z);
        BarMat.SetFloat("MyValueYe", (Mathf.Clamp(Joy, 0, curretMaxJoy) / curretMaxJoy));
        if (RTime <= 1f && monsterSpawnedAttraction)
        {
            monsterSpawnedAttraction = false;
            if(!overAttractive && !(playerAttraction <= maxBaseAttraction / 2f))
            {
                //Debug.Log("ShowingAttraction");
                attractionAnimator.SetTrigger("BeforeBlink");
            }
        }
        if (RTime <= 0f && !NoTimeout)
            TimeOut();
    }
    //Curses castng
    void Beat()
    {
        SpawnArrow();
        //Debug.Log("Beat!");
    }
    void TimeOut()
    {
        //Debug.Log(MonsterHordeCounter);
        if (ItemSpawned)
            EndItem();
        else
            EndMonster();
        if (!ItemSpawned)
        {
            if (MonsterHordeCounter >= MonstersInHorde)
            {
                hordeCounter += 1;
                /*
                MonstersInHorde = Hordes[HordeCounter].MonsterTypes.Length - 1;
                if (Hordes[HordeCounter].hordeLenght != 0)
                {
                    RMaxTime = Hordes[HordeCounter].hordeLenght; //USELESS POS
                    RTime = RMaxTime;
                    Debug.Log(RTime);
                }
                */
                MoveLocation();
            }
            else
            {
                MonsterHordeCounter += 1;
                SpawnCustomHordeMonster();
            }
            UpdateMonsterCounter();
        }
    }
    void EndMonster()
    {
        if(MonsterComp != null)
            MonsterComp.End4Y(Joy);
        Joy = 0;
        Destroy(Monster);
    }
    void MoveLocation()
    {
        NotSpawnArrows = true;
        NoTimeout = true;
        disableMoving = true;
        if (!(hordes[hordeCounter].notAnimatemovingToTime > 0))
            CharacComp.MoveLocation(true);
        MonsterHordeCounter = 0;
        if (!(hordes[hordeCounter].notAnimatemovingToTime > 0))
            Location.Move();
        if (hordes[hordeCounter].HordeType == "End")
        {
            DisableSpawn = true;
        }
        if (hordes[hordeCounter].HordeType != "")
        {
            monsterGaugeAnimator.SetBool("Shown", false);
        }
        UpdateMonsterCounter();
        if(!NoAfterLocationChange)
            StartCoroutine(AfterMoveLocation(hordes[hordeCounter].notAnimatemovingToTime));
    }
    void CalculateNextMonster()
    {
        if (MonsterHordeCounter + 1 < hordes[hordeCounter].MonsterTypes.Length)
        {
            NextMonster = MonsterFromType(hordes[hordeCounter].MonsterTypes[MonsterHordeCounter + 1]);
        }
        else
        {
            if(hordes[hordeCounter + 1].HordeType == "")
            {
                NextMonster = MonsterFromType(hordes[hordeCounter + 1].MonsterTypes[0]);
            }
        }
        if (NextMonster != null)
            nextMonsterComp = NextMonster.GetComponent<Monster>();
        else
            Debug.Log("NextMonsterIsNull here!");
    }
    GameObject MonsterFromType(string Type)
    {
        switch (Type)
        {
            case "L1":
                return MonsterTypes.L1[Random.Range(0, MonsterTypes.L1.Length)];
            case "L2":
                return MonsterTypes.L2[Random.Range(0, MonsterTypes.L2.Length)];
            case "L3":
                return MonsterTypes.L3[Random.Range(0, MonsterTypes.L3.Length)];
            case "D":
                return MonsterTypes.D[Random.Range(0, MonsterTypes.D.Length)];
            case "B":
                return MonsterTypes.B[Random.Range(0, MonsterTypes.B.Length)];
            case "S":
                return MonsterTypes.S[Random.Range(0, MonsterTypes.S.Length)];
            case "O":
                return MonsterTypes.O[Random.Range(0, MonsterTypes.O.Length)];
            default:
                return null;
        }
    }
    void OnNewLocation()
    {
        disableMoving = false;
        CharacComp.MoveLocation(false);
        MonstersInHorde = hordes[hordeCounter].MonsterTypes.Length - 1;
        if (hordes[hordeCounter].hordeLenght != 0)                         //Moved This part to the start of location move cus of double arrow bug
            RMaxTime = hordes[hordeCounter].hordeLenght;
        if (hordes[hordeCounter].HordeType == "Treasure")
        {
            SpawnTreasure();
        }
        else if(hordes[hordeCounter].HordeType == "NPC")
        {
            SpawnNPC();
        }
        else if (hordes[hordeCounter].HordeType == "End")
        {
            //RTime = RMaxTime;
            //NoTimeout = true;
            CompleteLevel();
        }
        else
        {
            monsterGaugeAnimator.SetBool("Shown", true);
            SpawnCustomHordeMonster();
        }
    }
    void SpawnCustomHordeMonster()
    {
        if (!DisableSpawn)
        {
            Monster = Instantiate(NextMonster, EnimyHolder.position, EnimyHolder.rotation, EnimyHolder);
            //Dunge Dance <---- This one mistake cost me 400 hours to make
            MonsterComp = Monster.GetComponent<Monster>();
            CalculateNextMonster();
            MonsterComp.Init(gameObject, TimeBetweenBeats);
            monsterGaugeSR.sprite = monsterGaugeSprites[MonsterComp.GetRelationLevel(Joy)];
            //Debug.Log("TBB: " + TimeBetweenBeats);
            if (curseHandler.fullCursed)
                MonsterComp.AddCurse(curseHandler.currentCurseId, true);
        }
        RTime = RMaxTime;
        monsterSpawnedAttraction = true;
        UpdateMonsterCounter();
    }
    void EndItem()
    {
        //Debug.Log("Here i go!");
        ItemSpawned = false;
        Monster.GetComponent<ItemAway>().GoAway();
        Destroy(Monster);
    }
    void SpawnTreasure()
    {
        RTime = RMaxTime;
        ItemSpawned = true;
        Monster = Instantiate(TreasurePrefab, EnimyHolder.position, EnimyHolder.rotation, EnimyHolder);
        Monster.GetComponent<Chest>().InitializeChestParameters(this, hordes[hordeCounter].treasureKind);
        MonsterHordeCounter = MonstersInHorde;
        CalculateNextMonster();
    }

    void SpawnNPC()
    {
        RTime = RMaxTime;
        ItemSpawned = true;
        Monster = Instantiate(npcPrefab, EnimyHolder.position, EnimyHolder.rotation, EnimyHolder);
        Monster.GetComponent<NPC>().InitializeNPCParameters(this, hordes[hordeCounter].npcKind);
        dialogueLineLength = RMaxTime / hordes[hordeCounter].npcLines.Length;
        //Debug.Log("DIAL LENGTH IS: " + dialogueLineLength.ToString());
        MonsterHordeCounter = MonstersInHorde;
        CalculateNextMonster();
        DisplayText(GetNPCNameById(hordes[hordeCounter].npcKind), LocalisationSystem.GetLocalizedValue(hordes[hordeCounter].npcLines[0]));
        dialogueCounter = 0;
        dialogueMode = true;
    }

    string GetNPCNameById(int npcId)
    {
        //Debug.Log(npcId);
        return LocalisationSystem.GetLocalizedValue("npc_name_id"+npcId.ToString());
    }
    void Yeeemput()
    {
        if (!disableMoving)
        {
            if (Input.GetButtonDown("R"))
            {
                PressThis("R");
                MonsterFollow.Animate("Right");
            }
            if (Input.GetButtonDown("L"))
            {
                PressThis("L");
                MonsterFollow.Animate("Left");
            }
            if (Input.GetButtonDown("U"))
            {
                PressThis("U");
                MonsterFollow.Animate("Up");
            }
            if (Input.GetButtonDown("D"))
            {
                PressThis("D");
                MonsterFollow.Animate("Down");
            }
        }
        if (!disableItemUse) 
        {
            if (Input.GetButtonDown("1"))
            {
                ActivateItem(1);
            }
            if (Input.GetButtonDown("2"))
            {
                ActivateItem(2);
            }
            if (Input.GetButtonDown("3"))
            {
                ActivateItem(3);
            }
        }
    }
    private void ActivateItem(int itemId)
    {
        if (!Input.GetButton("AltAction"))
            itemHolder.UseItem(itemId);
        else
            itemHolder.DropItem(itemId);
    }
    private void MobileYeeemput()
    {
        Touch lastTouch;
        if (Input.touches.Length > 0)
        {
            lastTouch = Input.touches[Input.touches.Length - 1];
            if(lastTouch.phase == TouchPhase.Began)
            {
                if(lastTouch.position.y < touchZone)
                {
                    if(lastTouch.position.x < touchZone)
                    {
                        MobileYeeemput2(lastTouch.position.y / touchZone, lastTouch.position.x / touchZone);
                    }
                    else if(lastTouch.position.x > (screenW - touchZone))
                    {
                        MobileYeeemput2(lastTouch.position.y / touchZone, (lastTouch.position.x - (screenW - touchZone)) / touchZone);
                    }
                }
            }
        }
    }
    private void MobileYeeemput2(float touchX, float TouchY)
    {
        if(TouchY > (1f - touchX))
        {
            if(TouchY > touchX)
            {
                PressThis("R");
                MonsterFollow.Animate("Right");
            }
            else
            {
                PressThis("U");
                MonsterFollow.Animate("Up");
            }
        }
        else
        {
            if (TouchY > touchX)
            {
                PressThis("D");
                MonsterFollow.Animate("Down");
            }
            else
            {
                PressThis("L");
                MonsterFollow.Animate("Left");
            }
        }
    }
    public void PressThis(string WhatToPress)
    {
        GameObject NextArrow = CalculateNextArrow();
        if(NextArrow != null)
            NextArrow.GetComponent<Arrow>().Yes(WhatToPress);
        CharacComp.Press(WhatToPress);
    }
    
    GameObject CalculateNextArrow()
    {
        if (Arrows == null)
            return null;
        else
        {
            GameObject Wanana = null;
            //Debug.Log(Arrows.Count);
            foreach (GameObject Arrow in Arrows)
            {
                if (Wanana == null)
                    Wanana = Arrow;
                if (Mathf.Abs(Arrow.transform.localPosition.x) < Mathf.Abs(Wanana.transform.localPosition.x) && !Arrow.GetComponent<Arrow>().disabled)
                    Wanana = Arrow;
            }
            if (Wanana != null && Mathf.Abs(Wanana.transform.localPosition.x) < 3)
                return Wanana;
            else
                return null;
        }
    }

    void DestroyAllArrows()
    {
        speedChangeVisual.Popup();
        if(Arrows != null)
        {
            foreach(GameObject Arrow in Arrows)
            {
                Destroy(Arrow);
            }
        }
        Arrows.Clear();
    }
    
    void UpdateMonsterCounter()
    { 
        if(hordes[hordeCounter].HordeType == "")
        {
            string[] CircleTypes = new string[MonstersInHorde + 1];
            for (int i = 0; i < CircleTypes.Length; i++)
            {
                if (MonsterHordeCounter > i)
                {
                    CircleTypes[i] = "YCS";
                }
                else if (MonsterHordeCounter == i)
                {
                    CircleTypes[i] = "BCS";
                }
                else
                {
                    CircleTypes[i] = "WCS";
                }
            }
            MonsterCounter.UpdateCounter(CircleTypes);
        }
        else
        {
            MonsterCounter.HideCounter();
        }
    }
    
    void SpawnArrow()
    {
        if (!NotSpawnArrows)
        {
            GameObject Arrow;
            //GameObject Arrow = Instantiate(ArrowPrefabs[Random.Range(0, ArrowPrefabs.Length)], Spawner.position, Quaternion.identity);
            if (RTime > TimeToGo)
            {
                if(hordes[hordeCounter].HordeType == "")
                {
                    Arrow = Instantiate(MonsterComp.ArrowPrefabs[Random.Range(0, MonsterComp.ArrowPrefabs.Length)],Spawner.position, Spawner.rotation, allUiHolder);
                }
                else
                {
                    Arrow = null;
                }
            }
            else
            { //OH SHIT, ITS LINE 666 (he is somwere around here O_O)
                if (MonsterHordeCounter + 1 < hordes[hordeCounter].MonsterTypes.Length)
                    Arrow = Instantiate(nextMonsterComp.ArrowPrefabs[Random.Range(0, nextMonsterComp.ArrowPrefabs.Length)], Spawner.position, Spawner.rotation, allUiHolder);
                else
                    Arrow = null;
            }
            if (Arrow != null)
            {
                Arrow NewArComp = Arrow.GetComponent<Arrow>();
                NewArComp.Speed = AroowSpeed;
                NewArComp.Auto = AutoMod;
                NewArComp.Manager = this;

                if (MonsterHordeCounter + 1 < hordes[hordeCounter].MonsterTypes.Length)
                {
                    NewArComp.lastArrow = false;
                }
                else
                {
                    NewArComp.lastArrow = (RTime < (SpawnerOffset + (TimeBetweenBeats * AroowSpeed)) / AroowSpeed) && RTime > 0;  // WELP, -3 hours of my life, and i have a fix that looks like SHIT and works like SHIT
                }

                //NewArComp.Manager.Monster = Monster;
                NewArComp.starto();
                if (curseHandler.preCursed)
                    NewArComp.arrowVisual.AddCurse(curseHandler.currentCurseId);
                //Monster.SendMessage("ArrowSpawned", NewArComp);
            }
        }
    }

    public void ArrowHit(float offset, float arrowSpeed, bool rightdir, Transform arrowTrans)
    {
        ArrowHit(offset, arrowSpeed, rightdir, arrowTrans, false);
    }
    public void ArrowHit(float offset, float arrowSpeed, bool rightdir, Transform arrowTrans, bool miniArrow)
    {
        float timeOffset = offset / arrowSpeed;
        //Debug.Log(timeOffset.ToString() + " DIR: " + rightdir.ToString());
        if(rightdir && timeOffset < 0.1f) // <- Так же поменять в визуале
        {
            if (effectPower >= 3)
                ScreenHit();
            if (effectPower >= 1 && timeOffset < 0.05f)
            {
                if(effectPower >= 2 && timeOffset < 0.02f)
                {
                    hitCounts[3]++;
                    soundSource.PlayHitSound(3);
                    Instantiate(ratePrefabs[3], arrowTrans.position, Quaternion.identity);
                    CalculateHitScore(3, miniArrow);
                    AddCombo(true);
                    //LEGENDARY
                }
                else
                {
                    hitCounts[2]++;
                    soundSource.PlayHitSound(2);
                    Instantiate(ratePrefabs[2], arrowTrans.position, Quaternion.identity);
                    CalculateHitScore(2, miniArrow);
                    AddCombo(true);
                    //GREAT
                }
            }
            else
            {
                hitCounts[1]++;
                soundSource.PlayHitSound(1);
                Instantiate(ratePrefabs[0], arrowTrans.position, Quaternion.identity);
                CalculateHitScore(1, miniArrow);
                AddCombo(false);
                //NORMAL
            }
        }
        else
        {
            hitCounts[0]++;
            if (!miniArrow)
                soundSource.PlayHitSound(0);
            else
                soundSource.PlayHitSound(0, 0.5f);
            Instantiate(ratePrefabs[1], arrowTrans.position, Quaternion.identity);
            CalculateHitScore(0, miniArrow);
            if(!miniArrow)
                LooseCombo();
            //MISS
        }
        if (MonsterComp != null)
            monsterGaugeSR.sprite = monsterGaugeSprites[MonsterComp.GetRelationLevel(Joy)];
        //MonsterComp.Arrow(offset, rightdir);
    }

    public void CalculateHitScore(int hitType, bool miniArrow)
    {
        float joyToAdd = 0;
        float scoreToAdd = 0;
        int joyScoreCount = 0;
        (float, float) joyScore;
        foreach(DanceStyle danceStyle in selectedDanceStyles)
        {
            if (danceStyle.affectsScore)
            {
                joyScoreCount++;
                joyScore = selectedDanceStyles[0].CalculateHitScore(hitType, miniArrow, thisMainManager);
                joyToAdd += joyScore.Item1;
                scoreToAdd += joyScore.Item2;
            }
        }
        joyToAdd /= joyScoreCount;
        joyToAdd /= joyScoreCount;
        AddJoy(joyToAdd, hitType);
        AddScore(scoreToAdd, hitType);
    }

    public void AddJoy(float joyToAdd, int hitType)
    {
        float firstAmmount = joyToAdd;
        switch (hitType)
        {
            case 0:
                firstAmmount += joyHitType0Bonus;
                break;
            case 1:
                firstAmmount += joyHitType1Bonus;
                break;
            case 2:
                firstAmmount += joyHitType2Bonus;
                break;
            case 3:
                firstAmmount += joyHitType3Bonus;
                break;
        }
        firstAmmount += joyAllHitBonus;
        switch (hitType)
        {
            case 0:
                firstAmmount *= joyHitType0Multiplier + joyAllHitMultiplier -1f + ((firstAmmount > 0f )? (joyDynamicHitMultiplier -1f):0f);
                break;
            case 1:
                firstAmmount *= joyHitType1Multiplier + joyAllHitMultiplier - 1f + ((firstAmmount > 0f )? (joyDynamicHitMultiplier - 1f) : 0f);
                break;
            case 2:
                firstAmmount *= joyHitType2Multiplier + joyAllHitMultiplier - 1f + ((firstAmmount > 0f )? (joyDynamicHitMultiplier - 1f) : 0f);
                break;
            case 3:
                firstAmmount *= joyHitType3Multiplier + joyAllHitMultiplier - 1f + ((firstAmmount > 0f )? (joyDynamicHitMultiplier - 1f) : 0f);
                break;
        }
        int roundedJoy = Mathf.RoundToInt(firstAmmount);
        Joy = Mathf.Clamp(Joy + roundedJoy, 0, curretMaxJoy);
    }
    public void AddScore(int amount)
    {
        AddScore(amount, -2);
    }
    public void AddScore(float amount, int hitType) // 0 - 4 = HitTypes | -2 = Monster(Default) | -1 = Item? Maybe
    {
        float firstAmmount = amount;
        switch (hitType) {
            case -2:
                firstAmmount += scoreMonsterBonus;
                break;
            case 0:
                firstAmmount += scoreHitType0Bonus;
                break;
            case 1:
                firstAmmount += scoreHitType1Bonus;
                break;
            case 2:
                firstAmmount += scoreHitType2Bonus;
                break;
            case 3:
                firstAmmount += scoreHitType3Bonus;
                break;
        }
        firstAmmount += scoreAllHitBonus;

        firstAmmount = Mathf.Clamp(firstAmmount, 0, float.MaxValue); //Why TF did I add that here? Does it even.. do anything?
        float unroundedScore = multiplier * firstAmmount;
        //float unroundedScore = (multiplier * firstAmmount)/* + Random.Range(-0.5f * firstAmmount, +0.5f * firstAmmount)*/ + ((greatCombo && (combo > 15))? 0.2f * firstAmmount : 0f); //Commented the randomness part
        switch (hitType)
        {
            case -2:
                unroundedScore *= scoreMonsterMultiplier + scoreAllHitMultiplier - 1f;
                break;
            case 0:
                unroundedScore *= scoreHitType0Multiplier + scoreAllHitMultiplier - 1f;
                break;
            case 1:
                unroundedScore *= scoreHitType1Multiplier + scoreAllHitMultiplier - 1f;
                break;
            case 2:
                unroundedScore *= scoreHitType2Multiplier + scoreAllHitMultiplier - 1f;
                break;
            case 3:
                unroundedScore *= scoreHitType3Multiplier + scoreAllHitMultiplier - 1f;
                break;
        }
        score += Mathf.RoundToInt(unroundedScore);
        scoreText.text = score.ToString();
    }
    public void AddCombo(bool greatOrHigher)
    {
        combo++;
        if (!greatOrHigher)
        { 
            greatCombo = false;
            greatComboCount = 0;
        }
        else
            greatComboCount += 1;
        if(greatComboCount >= 20)
            greatCombo = true;
        multiplier = Mathf.Clamp(combo / 10f, 1f, 4f);
        if (combo >= 20)
            DisplayCombo(true);
        DisplayMultiplier();
        lastMultiplier = Mathf.RoundToInt(multiplier);
    }
    public void LooseCombo()
    {
        if (maxCombo < combo)
            maxCombo = combo;
        if(combo >= 20)
            DisplayCombo(false);
        combo = 0;
        multiplier = 1f;
        DisplayMultiplier();
        lastMultiplier = 1;
        greatCombo = true;
    }
    public void ChangeAttr(int bywhat, int mode) //mode - 0) hit (first remove bonus then monster then base); 1) monster (added to monster bonus if attraction is over maximum); 2) monster loose (remove monster bonus attraction); 3) bonus (added to bonus if attraction is over maximum); 4) blink (thats it); 5) bonus loose (for whenewer the bonus ends, removes bonus attraction, just like monster loose does)
    {
        switch (mode)
        {
            case 0:
                if(bywhat > bonusAttraction)
                {
                    bywhat -= bonusAttraction;
                    bonusAttraction = 0;
                    if(bywhat > monsterBonusAttraction)
                    {
                        bywhat -= monsterBonusAttraction;
                        monsterBonusAttraction = 0;
                        baseAttraction = Mathf.Clamp(baseAttraction - bywhat, 0, maxBaseAttraction);
                    }
                    else
                    {
                        //Debug.Log("Should Go here");
                        monsterBonusAttraction -= bywhat;
                    }
                }
                else
                {
                    bonusAttraction -= bywhat;
                }
                break;
            case 1:
                if (baseAttraction + bywhat > maxBaseAttraction)
                {
                    bywhat -= (maxBaseAttraction - baseAttraction);
                    baseAttraction = maxBaseAttraction;
                    //Debug.Log(bywhat);
                    monsterBonusAttraction = Mathf.Clamp(monsterBonusAttraction + bywhat, 0, maxMonsterBonusAttraction);
                    //Debug.Log((baseAttraction + monsterBonusAttraction).ToString());
                }
                else
                    baseAttraction += bywhat;
                break;
            case 2:
                if(monsterBonusAttraction - bywhat > 0)
                {
                    monsterBonusAttraction -= bywhat;
                }
                else
                {
                    monsterBonusAttraction = 0;
                }
                break;
            case 3:
                if (baseAttraction + bywhat > maxBaseAttraction)
                {
                    bywhat -= (maxBaseAttraction - baseAttraction);
                    baseAttraction = maxBaseAttraction;
                    bonusAttraction = bonusAttraction + bywhat;
                   
                }
                else
                    baseAttraction += bywhat;
                break;
            case 4:
                Debug.Log("Blinkin"); //okay, now what?
                break;
            case 5:
                if(bonusAttraction - bywhat > 0)
                {
                    bonusAttraction -= bywhat;
                }
                else
                {
                    bonusAttraction = 0;
                }
                break;
            default:
                Debug.LogError("ERROR Mode ERROR selected ERROR the ERROR humanity ERROR is ERROR fucERRORked");
                break;
        }
        playerAttraction = baseAttraction + monsterBonusAttraction + bonusAttraction;
        attractionText.text = playerAttraction.ToString();
        if (playerAttraction > maxBaseAttraction)
            overAttractive = true;
        else
            overAttractive = false;
        if (bonusAttraction > 0)
            itemBonusAttr = true;
        else
            itemBonusAttr = false;
        if (playerAttraction <= maxBaseAttraction / 2f)
        {
            attractionAnimator.SetBool("LowAttraction", true);
            attractionAnimator.SetBool("OverAttraction", false);
            attractionAnimator.SetBool("BonusAttraction", false);
            attractionAnimator.SetBool("KeepShown", true);
            //attractionText.material = attractionTextPink;
            attractionText.fontMaterial = attractionTextPink;
            //attractionText.fontSharedMaterial = attractionTextPink;
        }
        else if (itemBonusAttr) //CHANGE THAT!!!!
        {
            attractionAnimator.SetBool("LowAttraction", false);
            attractionAnimator.SetBool("OverAttraction", false);
            attractionAnimator.SetBool("BonusAttraction", true);
            attractionAnimator.SetBool("KeepShown", true);
            attractionText.fontMaterial = attractionTextBlue;
        }
        else if(overAttractive)
        {
            attractionAnimator.SetBool("LowAttraction", false);
            attractionAnimator.SetBool("OverAttraction", true);
            attractionAnimator.SetBool("BonusAttraction", false);
            attractionAnimator.SetBool("KeepShown", true);
            attractionText.fontMaterial = attractionTextGreen;
        }
        else
        {
            attractionAnimator.SetBool("LowAttraction", false);
            attractionAnimator.SetBool("OverAttraction", false);
            attractionAnimator.SetBool("BonusAttraction", false);
            attractionAnimator.SetBool("KeepShown", false);
            attractionText.fontMaterial = attractionTextPink;
        }
        //attractionText.UpdateFontAsset();
        attractionAnimator.SetTrigger("Blink");
    }
    public void DisplayCombo(bool displayHide)
    {
        comboText.text = (greatCombo?combo.ToString()+ "•": combo.ToString());
        if (displayHide)
            comboUI.SetTrigger("Show");
        else
            comboUI.SetTrigger("Hide");
    }
    public void DisplayMultiplier()
    {
        multiplyerText.text = "x" + Mathf.RoundToInt(multiplier * scoreAllHitMultiplier).ToString(); // Kind of a poor way of adding scoreBonusMultiplier, but it's fii~~~~~iine Upd: Idk i'll need to do something with all that Upd2: Idk added allHitMult. or smth...
        if (lastMultiplier < Mathf.RoundToInt(multiplier))
        {
            comboUI.SetTrigger("ShowM");
            if (lastMultiplier == 3)
                soundSource.PlayOtherSound(1);
        }
        if (lastMultiplier > Mathf.RoundToInt(multiplier))
            comboUI.SetTrigger("HideM");
    }
    public void KillPlayer()
    {
        Debug.Log("KILLING1");
        GameEnded(false);
    }
    public void CompleteLevel()
    {
        GameEnded(true);
    }
    private void GameEnded(bool levelCompleted)
    {
        NoTimeout = true;
        NotSpawnArrows = true;
        DisableSpawn = true;
        disableMoving = true;
        disableItemUse = true;
        curseHandler.ToggleCurse(false);
        NoAfterLocationChange = true;
        if (maxCombo < combo)
            maxCombo = combo;
        if (!levelCompleted)
            CharacComp.Die();
        HideAllUiForever();
        ApplyXP();
        if (!useDebugPlayerData)
        {
            MenuDataManager.saveData.item1Id = itemHolder.item1Id;
            MenuDataManager.saveData.item2Id = itemHolder.item2Id;
            MenuDataManager.saveData.item3Id = itemHolder.item3Id;
            if (MenuDataManager.saveData.playerXp - playerStartXp > MenuDataManager.saveData.levelDatas[levelId].maxScore)
                MenuDataManager.saveData.levelDatas[levelId].maxScore = MenuDataManager.saveData.playerXp - playerStartXp;
            if(levelCompleted)
                MenuDataManager.saveData.levelDatas[levelId].completed = true;
            SaveSystem.Save(MenuDataManager.saveData);
            Instantiate(endScreenPrefab, Vector3.zero, Quaternion.identity, allUiHolder).GetComponent<EndWindow>().EndWindowStats(!levelCompleted, false, MenuDataManager.saveData);
        }
        else
        {
            Instantiate(endScreenPrefab, Vector3.zero, Quaternion.identity, allUiHolder).GetComponent<EndWindow>().EndWindowStats(!levelCompleted, true, debugPlayerData);
        }
        StartCoroutine(LowerEndStageVolume());
    }
    public void ApplyXP()
    {
        if (!useDebugPlayerData)
        {
            int i = 0;
            MenuDataManager.saveData.playerXp += score;
            int playerXp = MenuDataManager.saveData.playerXp;
            int[] rankXps = MenuDataManager.rankXps;
            while (playerXp >= rankXps[i] && i < rankXps.Length)
            {
                i++;
            }
            MenuDataManager.saveData.playerLevel = i - 1;
        }
        else
        {
            int i = 0;
            debugPlayerData.playerXp += score;
            int playerXp = debugPlayerData.playerXp;
            int[] rankXps = debugPlayerLevelupXps;
            while (playerXp >= rankXps[i] && i < rankXps.Length)
            {
                i++;
            }
            debugPlayerData.playerLevel = i - 1;
        }
    }
    //Just came up with meme no one will understand, here it is: 16B2 16BA 16D6 16DA 0020 16CF 16C1
    public void HideAllUiForever()
    {
        downUI.SetTrigger("HideForewer");
        barHolderAnimator.SetTrigger("HideForewer");
        monsterCounterHolderAnimator.SetTrigger("HideForewer");
        comboScoreAnimator.SetTrigger("HideForewer");
        UUHAIHAnimator.SetTrigger("HideForewer");
        curseHandler.DisableCurseEffects();
        DestroyAllArrows();
    }
    public void AddMoney(int amount)
    {
        coinCountAnimator.SetTrigger("Jump");
        if (!useDebugPlayerData)
        {
            MenuDataManager.saveData.moneyAmount += amount;
            coinCountText.text = MenuDataManager.saveData.moneyAmount.ToString();
        }
        else
        {
            debugPlayerData.moneyAmount += amount;
            coinCountText.text = debugPlayerData.moneyAmount.ToString();
        }
    }
    public void ScreenHit()
    {
        Camera.GetComponent<Animator>().SetTrigger("Hit");
        bigPP.GetComponent<Animator>().SetTrigger("Hit");
    }
    void HandleDialogue()
    {
        int dialLen = hordes[hordeCounter].npcLines.Length;
        bool dialRand = hordes[hordeCounter].npcLines.Length == hordes[hordeCounter].npcLinesRandomizer.Length;
        if (dialogueCounter < dialLen)
        {
            if((dialogueCounter + 1) * dialogueLineLength < RMaxTime - RTime)
            {
                dialogueCounter++;
                if(!dialRand || hordes[hordeCounter].npcLinesRandomizer[dialogueCounter] < 2)
                    DisplayText(GetNPCNameById(hordes[hordeCounter].npcKind), LocalisationSystem.GetLocalizedValue(hordes[hordeCounter].npcLines[dialogueCounter]));
                else
                    DisplayText(GetNPCNameById(hordes[hordeCounter].npcKind), LocalisationSystem.GetLocalizedValue(hordes[hordeCounter].npcLines[dialogueCounter] + "_r" + Random.Range(1, hordes[hordeCounter].npcLinesRandomizer[dialogueCounter] + 1).ToString()));
            }
        }
        else
        {
            ChangeUIType(false);
            dialogueMode = false;
        }
    }
    public void DisplayItemText(int itemId)
    {
        DisplayText(LocalisationSystem.GetLocalizedValue("item_name_id" + itemId.ToString()), LocalisationSystem.GetLocalizedValue("item_desc_id" + itemId.ToString()));
    }
    public  void DisplayText(string title, string text)
    {
        textGuiTitle.text = title;
        //textGuiText.text = text;
        textGuiText.text = "";
        StartCoroutine(SmoothShowText(text));
        ChangeUIType(true);
    }
    public void ChangeUIType(bool TextUI)
    {
        downUI.SetBool("TextUI", TextUI);
    }
    public void ChangeStageVolume(float soundVolume, float musicVolume)
    {
        soundSource.soundVolume = soundVolume;
        audioSource.volume = musicVolume;
        SpectrumManager.volumeDemultiplier = 1f / musicVolume;
    }
    public IEnumerator LowerEndStageVolume()
    {
        float localTimer = 0;
        float volume = audioSource.volume;
        while(localTimer < 1f)
        {
            localTimer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(volume, volume * 0.22f, localTimer);
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator SmoothShowText(string text)
    {
        bool skipArgument = false;
        foreach (char ch in text)
        {
            textGuiText.text += ch;
            if (ch == '<')
                skipArgument = true;
            if (ch == '>')
                skipArgument = false;
            if(!skipArgument)
                yield return new WaitForSeconds(0.025f);
        }
        yield return null;
    }
    public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(3f);
        StartLevel();
    }
    public void ItemAttractionBoost(int boost, float retime)
    {
        StartCoroutine(ItemAttractionBoostCoroutine(boost, retime));
    }
    public IEnumerator ItemAttractionBoostCoroutine(int boost, float retime)
    {
        ChangeAttr(boost, 3);
        yield return new WaitForSeconds(retime);
        ChangeAttr(boost, 5);
    }
    public IEnumerator ChangeArrowSpeed(float sleepFor, float speedChange)
    {
        bool startNSA;
        startNSA = NotSpawnArrows;
        NotSpawnArrows = true;
        AroowSpeed += speedChange;
        speedChangeVisual.Popup();
        yield return new WaitForSeconds(NextBeatTime - Timer + TimeBetweenBeats);
        foreach (GameObject arow in Arrows)
        {
            Destroy(arow);
        }
        Arrows.Clear();
        yield return new WaitForSeconds(sleepFor);
        NotSpawnArrows = startNSA;
    }
    public IEnumerator AfterMoveLocation(float standingTime)
    {
        if(standingTime > 0f)
        {
            Debug.Log("StandingForSeconds: " + standingTime.ToString());
            yield return new WaitForSeconds(( standingTime + 0.1f) - TimeToGo);
            NotSpawnArrows = false;
            yield return new WaitForSeconds(TimeToGo - 0.1f);
        }
        else
        {
            yield return new WaitForSeconds(2.1f - TimeToGo);
            NotSpawnArrows = false;
            yield return new WaitForSeconds(TimeToGo - 0.1f);
        }
        NoTimeout = false;
        OnNewLocation();
        yield return null;
    }
    //As for me, this code just looks bad. Anyway i dont think anyone will even see it.
    //-Limofeus [Main Developer]
}