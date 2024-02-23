using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimofeusPhaze1: Monster
{
    public override void End4Y(float Joy)
    {
        int attraction = MainMan.Attraction;
        if ((Joy >= 175f && attraction >= 45) || (Joy >= 180f && attraction >= 20) || (Joy >= 190f))
        {
            GoodAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(130, 140));
            MainMan.AddScore(1200);
        }
        else if ((Joy >= 110f && attraction >= 45) || (Joy >= 130f && attraction >= 20) || (Joy >= 140f))
        {
            NormalAway();
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(60, 70));
            MainMan.AddScore(800);
        }
        else if ((Joy >= 20f && attraction >= 45) || (Joy >= 30f && attraction >= 20) || (Joy >= 40f))
        {
            BadAway();
            MainMan.AddScore(100);
            MainMan.ChangeAttr(8, 0);
        }
        else
        {
            KillAway();
            MainMan.KillPlayer();
        }
    }

    public override int GetRelationLevel(float Joy)
    {
        int attraction = MainMan.Attraction;
        if ((Joy >= 175f && attraction >= 45) || (Joy >= 180f && attraction >= 20) || (Joy >= 190f))
        {
            return 3;
        }
        else if ((Joy >= 110f && attraction >= 45) || (Joy >= 130f && attraction >= 20) || (Joy >= 140f))
        {
            return 2;
        }
        else if ((Joy >= 20f && attraction >= 45) || (Joy >= 30f && attraction >= 20) || (Joy >= 40f))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
