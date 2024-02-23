using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin1 : Monster
{
    public override void ArrowSpawned(Arrow arrow)
    {
        if (Random.value <= 0.5f)
            arrow.SwitchDir("R");
        else
            arrow.SwitchDir("L");
    }

    public override void End4Y(float Joy)
    {
        if (Joy >= 90)
        {
            MainMan.MonsterFollow.AddMonster(0, 6);
            MainMan.AddScore(120);
            DropMoney(Random.Range(8, 13));
            GoodAway();
        }
        else if(Joy >= 50)
        {
            MainMan.AddScore(45);
            MainMan.ChangeAttr(0, 4);
            DropMoney(Random.Range(5, 10));
            NormalAway();
        }
        else
        {
            MainMan.ChangeAttr(3, 0);
            BadAway();
        }
        base.End4Y(Joy);
    }

    public override int GetRelationLevel(float Joy)
    {
        if (Joy >= 90)
        {
            return 3;
        }
        else if (Joy >= 50)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}