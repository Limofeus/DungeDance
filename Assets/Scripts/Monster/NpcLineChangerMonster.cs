using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcLineChangerMonster : GenericMonster
{
    [SerializeField] private int hordeId;
    [SerializeField] private string[] newNpcLines;
    [SerializeField] private int[] newRandLines;
    public override void End4Y(float Joy)
    {
        base.End4Y(Joy);
        if (customTrigerred)
        {
            MainMan.hordes[hordeId].npcLines = newNpcLines;
            MainMan.hordes[hordeId].npcLinesRandomizer = newRandLines;
        }
    }
}
