using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleSmallMonster : Monster
{
    public override void ArrowSpawned(Arrow arrow)
    {
        /*
        if (Random.value <= 0.7)
        {
            if (Random.value <= 0.5f)
                arrow.SwitchDir("R");
            else
                arrow.SwitchDir("L");
        }
        else
        {
            arrow.SwitchDir("U");
            //Debug.Log("YESSS");
        }
        */
    }

    public override void End4Y(float Joy)
    {
        int attraction = playerStats.playerAttraction;
        if ((Joy >= 100f && attraction >= 40) || (Joy >= 110f && attraction >= 20) || (Joy >= 130f))
        {
            MainMan.monsterFollow.AddMonster(0, 6);
            MainMan.monsterFollow.AddMonster(1, 5);
            GoodAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(40, 100));
            MainMan.AddScore(500);
        }
        else if ((Joy >= 40f && attraction >= 40) || (Joy >= 50f && attraction >= 20) || (Joy >= 70f))
        {
            if(Random.Range(0, 1f) > 0.5f)
            {
                MainMan.monsterFollow.AddMonster(0, 6);
            }
            else
            {
                MainMan.monsterFollow.AddMonster(1, 5);
            }
            NormalAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(20, 40));
            MainMan.AddScore(200);
        }
        else if ((Joy >= 15f && attraction >= 40) || (Joy >= 20f && attraction >= 20) || (Joy >= 25f))
        {
            BadAway();
            MainMan.AddScore(15);
            MainMan.ChangeAttr(10, 0);
        }
        else
        {
            KillAway();
            MainMan.KillPlayer();
        }
    }

    public override int GetRelationLevel(float Joy)
    {
        int attraction = playerStats.playerAttraction;
        if ((Joy >= 90f && attraction >= 45) || (Joy >= 105f && attraction >= 20) || (Joy >= 120f))
        {
            return 3;
        }
        else if ((Joy >= 35f && attraction >= 45) || (Joy >= 45f && attraction >= 20) || (Joy >= 55f))
        {
            return 2;
        }
        else if ((Joy >= 10f && attraction >= 45) || (Joy >= 15f && attraction >= 20) || (Joy >= 18f))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
