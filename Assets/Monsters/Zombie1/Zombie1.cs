using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : Monster
{
    public override void ArrowSpawned(Arrow arrow)
    {
        if (Random.value <= 0.5f)
            arrow.SwitchDir("U");
        else
            arrow.SwitchDir("D");
    }

    public override void End4Y(float Joy)
    {
        if (Joy >= 90)
        {
            MainMan.MonsterFollow.AddMonster(1, 5);
            MainMan.AddScore(100);
            DropMoney(Random.Range(7, 12));
            GoodAway();
        }
        else if (Joy >= 60)
        {
            MainMan.AddScore(40);
            MainMan.ChangeAttr(0, 4); //Why tho?
            DropMoney(Random.Range(5, 9));
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
        else if (Joy >= 60)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }
}
