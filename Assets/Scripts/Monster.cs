using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float ArrowChance; // This thing funcking does NOTHING AHAHAHHAHAHHA HHAHAHHAHA (I wrote this code 4 years ago)
    public Animator Animator;
    protected MainManager MainMan;
    protected PlayerStats playerStats;
    public bool arrowChangeBlock = false;
    public GameObject[] ArrowPrefabs;
    public string[] animationNames;
    public GameObject killText;
    public GameObject BadText;
    public GameObject NormalText;
    public GameObject GoodText;
    public GameObject GoAway;
    public float AnimatorSpeed;
    public float DefaultJoy;
    public float maxJoy;
    public float joyMultToAddToScoreOrSomething = 0f;
    public SpriteRenderer EffectSR;
    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin3;
    public GameObject coin4;
    public GameObject coin5;
    // Start is called before the first frame update
    public void Init(MainManager maiman, PlayerStats playerStatsPass, float speed)
    {
        AnimatorSpeed = 1f / speed;
        if (Animator != null)
            Animator.speed = AnimatorSpeed;
        //Debug.Log("ANIMS: " + AnimatorSpeed);
        playerStats = playerStatsPass;
        MainMan = maiman;
        MainMan.Joy = DefaultJoy;
        MainMan.curretMaxJoy = maxJoy;
        MainMan.ArrowPrefabs = ArrowPrefabs;
        MainMan.ArrowChance = ArrowChance;
        //to make some monsters joy change faster
        MainMan.hiddenMonsterJoyMult += joyMultToAddToScoreOrSomething;
    }
    public virtual void ArrowSpawned(Arrow arrow)
    {

    }
    public void AddCurse(int CurseId, bool AddOrRemove)
    {
        if(EffectSR != null)
        {
            if (AddOrRemove)
            {
                switch (CurseId)
                {
                    case 0:
                        EffectSR.material = MainMan.Effect1;
                        EffectSR.color = new Color(0.5f, 0f, 1f, 0.8f);
                        break;
                    case 1:
                        EffectSR.material = MainMan.Effect2;
                        EffectSR.color = new Color(1f, 0.7f, 0f, 0.8f);
                        break;
                    default:
                        Debug.LogError("Error");
                        break;
                }
            }
            else
            {
                EffectSR.color = new Color(0f, 0f, 0f, 0f);
            }
        }
        else
        {
            Debug.Log("No EffectSR");
        }
    }

    private void Start()
    {
        //Debug.Log("WHEN START: " + Animator.speed + "  " + AnimatorSpeed);
        //Debug.Log("AFTER START: " + Animator.speed + "  " + AnimatorSpeed);
    }

    public virtual void Arrow(float offset, bool rightdir)
    {
        /*
        if (rightdir)
        {
            float ofs = Mathf.Abs(offset);
            MainMan.Joy += 15 - Mathf.Clamp(ofs * 10, 0, 25);
            if(offset < 1.5f)
            {
                MainMan.AddScore(2);
                MainMan.AddCombo((offset < 0.5f) ? true : false);
            }
            else
            {
                MainMan.LooseCombo();
            }
        }
        else
        {
            MainMan.Joy -= 10;
            MainMan.LooseCombo();
        }
        */
    }

    public virtual void ArrowThere(Arrow WhatArrow)
    {
        //Debug.Log("WHEN ARROW: " + Animator.speed);
        if (WhatArrow.direction == "U")
            Animator.SetTrigger("Jump");
        else if(WhatArrow.direction == "D")
            Animator.SetTrigger("GoDown");
        else if (WhatArrow.direction == "R")
            Animator.SetTrigger("Right");
        else if (WhatArrow.direction == "L")
            Animator.SetTrigger("Left");
    }

    public virtual void End4Y(float Joy)
    {
        //Debug.Log("BaseEnd");
    }

    public virtual int GetRelationLevel(float Joy) // 0 - Anger(kill), 1 - Sad, 2 - Neutral, 3 - Friendly
    {
        return 0;
    }

    public void DropMoney(int money)
    {
        Transform spawnPoint = this.transform;
        while (money > 0)
        {
            if (money > 1200)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin5, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 4; i++)
                    {
                        Instantiate(coin4, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -1200;
            }
            else if (money > 300)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin4, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 6; i++)
                    {
                        Instantiate(coin3, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -300;
            }
            else if (money > 50)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin3, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 5; i++)
                    {
                        Instantiate(coin2, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -50;
            }
            else if (money > 10)
            {
                if (Random.value > 0.5f)
                    Instantiate(coin2, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                else
                    for (int i = 0; i < 10; i++)
                    {
                        Instantiate(coin1, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                    }
                money += -10;
            }
            else
            {
                Instantiate(coin1, spawnPoint.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-0.5f, 0.5f), 0f), spawnPoint.rotation);
                money += -1;
            }
        }
    }

    public void GoodAway()
    {
        MainMan.hiddenMonsterJoyMult -= joyMultToAddToScoreOrSomething;

        Instantiate(GoodText, transform.position, transform.rotation);
        if (GoAway == null) return;
        Instantiate(GoAway, transform.position, Quaternion.identity).GetComponent<GoAwayScript>().REEEOVEE(animationNames[0]);
    }

    public void NormalAway()
    {
        MainMan.hiddenMonsterJoyMult -= joyMultToAddToScoreOrSomething;

        Instantiate(NormalText, transform.position, transform.rotation);
        if (GoAway == null) return;
        Instantiate(GoAway, transform.position, Quaternion.identity).GetComponent<GoAwayScript>().REEEOVEE(animationNames[1]);
    }
    public void BadAway()
    {
        MainMan.hiddenMonsterJoyMult -= joyMultToAddToScoreOrSomething;

        Instantiate(BadText, transform.position, transform.rotation);
        if (GoAway == null) return;
        Instantiate(GoAway, transform.position, Quaternion.identity).GetComponent<GoAwayScript>().REEEOVEE(animationNames[2]);
    }

    public void KillAway()
    {
        MainMan.hiddenMonsterJoyMult -= joyMultToAddToScoreOrSomething;

        Instantiate(killText, transform.position, transform.rotation);
        if (GoAway == null) return;
        Instantiate(GoAway, transform.position, Quaternion.identity).GetComponent<GoAwayScript>().REEEOVEE(animationNames[3]);
    }
}
