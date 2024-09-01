using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GenericMonster : Monster
{
    [System.Serializable]
    public class GenericAwayEffect
    {
        [SerializeField] public bool changeAttr;
        [SerializeField] public int attrChangeValue;
        [SerializeField] public int attrChangeMode;
        [SerializeField] public bool dropMoney;
        [SerializeField] public Vector2Int moneyDropMinMax;
        [SerializeField] public int scoreToAdd;
        [SerializeField] public bool killPlayer;
        [SerializeField] public int addFolowers;
        [SerializeField] public int followerSpriteId;
        [SerializeField] public int followerPower;
        [SerializeField] public string tagToAdd; //No idea why I serialize fielded them all... ah, they were private earlier
        [SerializeField] public bool customTrigger;
    }

    [SerializeField] private string[] possibleArrowDirs;

    [SerializeField] private Vector2[] firstJoyAttrPairs;
    [SerializeField] private Vector2[] secondJoyAttrPairs;
    [SerializeField] private float[] joyOnlyVal;

    [SerializeField] private GenericAwayEffect[] genericAwayEffects;

    protected bool customTrigerred = false;


    public override void ArrowSpawned(Arrow arrow)
    {
        if(possibleArrowDirs.Length > 0)
        {
            string arrowDir = possibleArrowDirs[Random.Range(0, possibleArrowDirs.Length)];
            arrow.SwitchDir(arrowDir);
        }
    }

    public override void End4Y(float Joy)
    {
        switch (GetRelationLevel(Joy))
        {
            case 3:
                GoodAway();
                break;
            case 2:
                NormalAway();
                break;
            case 1:
                BadAway();
                break;
            case 0:
                KillAway();
                break;
        }

        FireGenericEffects(genericAwayEffects[GetRelationLevel(Joy)]); //<- THIS THING IS REVERSED!!!

    }

    private void FireGenericEffects(GenericAwayEffect gae)
    {
        if (gae.customTrigger)
        {
            customTrigerred = true;
        }
        if (gae.changeAttr)
        {
            MainMan.ChangeAttr(gae.attrChangeValue, gae.attrChangeMode);
        }
        if (gae.dropMoney)
        {
            DropMoney(Random.Range(gae.moneyDropMinMax.x, gae.moneyDropMinMax.y));
        }
        if(gae.scoreToAdd > 0)
        {
            MainMan.AddScore(gae.scoreToAdd);
        }
        if (gae.addFolowers > 0)
        {
            for(int i = 0; i < gae.addFolowers; i++)
                MainMan.monsterFollow.AddMonster(gae.followerSpriteId, gae.followerPower);
        }
        if (gae.tagToAdd != "")
        {
            if (!MainMan.useDebugPlayerData && !MenuDataManager.saveData.progressTags.ContainsTag(gae.tagToAdd))
            {
                MenuDataManager.saveData.progressTags.AddTag(gae.tagToAdd);
            }
        }
        if (gae.killPlayer)
        {
            MainMan.KillPlayer();
        }
    }
    public override int GetRelationLevel(float Joy)
    {
        int attraction = playerStats.playerAttraction;
        if ((Joy >= firstJoyAttrPairs[0].x && attraction >= firstJoyAttrPairs[0].y) || (Joy >= secondJoyAttrPairs[0].x && attraction >= secondJoyAttrPairs[0].y) || (Joy >= joyOnlyVal[0]))
        {
            return 3;
        }
        else if ((Joy >= firstJoyAttrPairs[1].x && attraction >= firstJoyAttrPairs[1].y) || (Joy >= secondJoyAttrPairs[1].x && attraction >= secondJoyAttrPairs[1].y) || (Joy >= joyOnlyVal[1]))
        {
            return 2;
        }
        else if ((Joy >= firstJoyAttrPairs[2].x && attraction >= firstJoyAttrPairs[2].y) || (Joy >= secondJoyAttrPairs[2].x && attraction >= secondJoyAttrPairs[2].y) || (Joy >= joyOnlyVal[2]))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
