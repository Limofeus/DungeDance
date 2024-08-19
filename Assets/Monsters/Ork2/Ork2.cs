using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ork2 : Monster
{
    public override void ArrowSpawned(Arrow arrow)
    {
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
    }

    public override void End4Y(float Joy)
    {
        int attraction = playerStats.playerAttraction;
        if((Joy >= 90f && attraction >= 45)||(Joy >= 105f && attraction >= 20)||(Joy >= 120f))
        {
            GoodAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(24, 29));
            MainMan.AddScore(520);
        }
        else if ((Joy >= 35f && attraction >= 45) || (Joy >= 45f && attraction >= 20) || (Joy >= 55f))
        {
            NormalAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(19, 24));
            MainMan.AddScore(205);
        }
        else if((Joy >= 10f && attraction >= 45) || (Joy >= 15f && attraction >= 20) || (Joy >= 22f))
        {
            BadAway();
            MainMan.AddScore(15);
            MainMan.ChangeAttr(6, 0);
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
