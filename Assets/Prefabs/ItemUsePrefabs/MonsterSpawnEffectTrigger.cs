using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnEffectTrigger : ItemEffectTrigger
{
    private MainManager mainManager;
    [SerializeField] private int monstersToSpawn = 1;
    [SerializeField] private int[] followSpriteIds;
    [SerializeField] private Vector2Int minMaxFollowPower;
    [SerializeField] private float overrideFollowTime = -1f;
    [SerializeField] private bool deleteWhenDone;

    private void Start()
    {
        mainManager = MainManager.thisMainManager;
    }
    public override void OnTrigger()
    {
        TriggerAnim();
        for(int i = 0; i < monstersToSpawn; i++)
        {
            mainManager.monsterFollow.AddMonster(followSpriteIds[Random.Range(0, followSpriteIds.Length)], Random.Range(minMaxFollowPower.x, minMaxFollowPower.y), overrideFollowTime);
        }
        StartCoroutine(FireNextTrigger());
    }

    private IEnumerator FireNextTrigger()
    {
        yield return new WaitForSeconds(nextTriggerDelay);
        nextTrigger?.OnTrigger();
        if (deleteWhenDone)
        {
            Destroy(gameObject);
        }
    }
}
