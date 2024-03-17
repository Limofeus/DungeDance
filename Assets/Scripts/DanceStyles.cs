using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface AffectsHitScore
{
    (float, float) CalculateHitScore(int hitType, bool miniArrow, MainManager mainManager); //(float, float) = Joy, Score
}

interface AffectsMonsterSpawn
{
    void OnMonsterSpawn();
}
public abstract class DanceStyle
{
    //’а ха пусто капуста
}

public class DefaultDanceStyle : DanceStyle, AffectsHitScore
{
    public (float, float) CalculateHitScore(int hitType, bool miniArrow, MainManager mainManager)
    {
        //Debug.Log("Trololo default dance style workin'");
        float joyToAdd = 0;
        float scoreToAdd = 0;

        switch (hitType)
        {
            case 0:
                joyToAdd = -5; //Hardcoded for dance styles?? Seems so..
                scoreToAdd = 0;
                break;
            case 1:
                joyToAdd = 2;
                scoreToAdd = 5;
                break;
            case 2:
                joyToAdd = 3;
                scoreToAdd = 8;
                break;
            case 3:
                joyToAdd = 5;
                scoreToAdd = 10;
                break;
            default:
                Debug.Log("Erorerorerorerorerorero");
                break;
        }
        //Debug.Log($"Pre add: MtJ: {followMonsterToJoyMultiplier} | MtS: {followMonsterToScoreMultiplier} | JtA: {joyToAdd} | StA: {scoreToAdd}");
        joyToAdd += (mainManager.bonusesAndMultiplers.followMonsterToJoyMultiplier * mainManager.followMonstersCount) / 2f; //—юды вписывать :)
        joyToAdd += (mainManager.bonusesAndMultiplers.attractionToJoyMultiplier * mainManager.playerStats.playerAttraction) / 100f;
        scoreToAdd += (mainManager.bonusesAndMultiplers.followMonsterToScoreMultiplier * mainManager.followMonstersCount) / 3f; //Also style dependant + hardcoded
        scoreToAdd += (mainManager.bonusesAndMultiplers.attractionToScoreMultiplier * mainManager.playerStats.playerAttraction) / 150f;
        //Debug.Log($"After add: JtA: {joyToAdd} | StA: {scoreToAdd}");

        if (miniArrow && joyToAdd < 0) //I'll make this thing style dependant as well then lol
            joyToAdd = (joyToAdd / 2);

        return (joyToAdd, scoreToAdd);
    }

    public DefaultDanceStyle()
    {

    }
}

public class MonsterFollowerStyle : DanceStyle, AffectsMonsterSpawn
{
    public void OnMonsterSpawn()
    {
        throw new NotImplementedException();
    }
    public MonsterFollowerStyle()
    {

    }
}
