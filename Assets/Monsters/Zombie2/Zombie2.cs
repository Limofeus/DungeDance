using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie2 : Monster
{
    public override void ArrowSpawned(Arrow arrow)
    {
        if (Random.value <= 0.7)
        {
            if (Random.value <= 0.5f)
                arrow.SwitchDir("U");
            else
                arrow.SwitchDir("D");
        }
        else
        {
            arrow.SwitchDir("R");
            //Debug.Log("YESSS");
        }
    }
    public override void End4Y(float Joy)
    {
        int attraction = MainMan.playerAttraction;
        if ((Joy >= 90f && attraction >= 45) || (Joy >= 105f && attraction >= 20) || (Joy >= 120f))
        {
            GoodAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(23, 28));
            MainMan.AddScore(500);
        }
        else if ((Joy >= 40f && attraction >= 45) || (Joy >= 50f && attraction >= 20) || (Joy >= 58f))
        {
            NormalAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(18, 22));
            MainMan.AddScore(185);
        }
        else if ((Joy >= 10f && attraction >= 45) || (Joy >= 15f && attraction >= 20) || (Joy >= 18f))
        {
            BadAway();
            MainMan.AddScore(12);
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
        int attraction = MainMan.playerAttraction;
        if ((Joy >= 90f && attraction >= 45) || (Joy >= 105f && attraction >= 20) || (Joy >= 120f))
        {
            return 3;
        }
        else if ((Joy >= 40f && attraction >= 45) || (Joy >= 50f && attraction >= 20) || (Joy >= 58f))
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
