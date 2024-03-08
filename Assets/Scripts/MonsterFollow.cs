using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollow : MonoBehaviour
{
    public MainManager mainManager;
    public GameObject monsterPrefab;
    public GameObject minimonAwayPrefab;
    private int monsterCount = 0;
    public List<GameObject> miniMonsters = new List<GameObject>();
    public List<Animator> animators = new List<Animator>();
    public List<int> followPowers = new List<int>();
    public Sprite[] Textures;

    public void AddMonster(int TextureId, int followPower)
    {
        mainManager.soundSource.PlayOtherSound(0);
        foreach(GameObject MiniMonster in miniMonsters)
        {
            float place = Random.Range(-0.3f, 0.3f);
            StartCoroutine(SlideMonsters(MiniMonster.transform, MiniMonster.transform.position + new Vector3(-1.5f, place, place)));
        }
        GameObject Monster = Instantiate(monsterPrefab, transform.position, Quaternion.identity, transform);
        Monster.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Textures[TextureId];
        Animator MonsAni = Monster.GetComponent<Animator>();
        MonsAni.speed = (1f / mainManager.timeBetweenBeats);
        MonsAni.SetTrigger("Appear");
        miniMonsters.Add(Monster);
        animators.Add(MonsAni);
        followPowers.Add(followPower);
        mainManager.ChangeAttr(followPower, 1);
        if(miniMonsters.Count > 5)
        {
            AwayLastFollow();
        }
        else
        {
            StartCoroutine(TimeOutFollow(MainManager.followTime));
        }
        monsterCount++;
        UpdateMainmanMonCount();
    }
    void AwayLastFollow()
    {
        Instantiate(minimonAwayPrefab, miniMonsters[0].transform.position, Quaternion.identity).GetComponent<MiniMonsterAway>().Initialaze(miniMonsters[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        Destroy(miniMonsters[0]);
        mainManager.ChangeAttr(followPowers[0], 2);
        followPowers.RemoveAt(0);
        miniMonsters.RemoveAt(0);
        animators.RemoveAt(0);
        monsterCount--;
        UpdateMainmanMonCount();
    }

    void UpdateMainmanMonCount()
    {
        mainManager.followMonstersCount = monsterCount;
    }
    IEnumerator TimeOutFollow(float timeFollow)
    {
        yield return new WaitForSeconds(timeFollow);
        AwayLastFollow();
    }
    public void Animate(string AnimationName)
    {
        Animator[] reverseAnimators = animators.ToArray();
        System.Array.Reverse(reverseAnimators);
        StartCoroutine(MakeMove(AnimationName, reverseAnimators));
    }

    IEnumerator MakeMove(string direction, Animator[] localAnimators)
    {
        for(int i = 0; i < localAnimators.Length; i++)
        {
            if(localAnimators[i] != null)
                localAnimators[i].SetTrigger(direction);
            yield return new WaitForSeconds(0.06f);
        }
    }
    IEnumerator SlideMonsters(Transform MonsterToGO, Vector3 FinalPosition)
    {
        float Timer = 0;
        while(Timer < 0.5f)
        {
            if(MonsterToGO != null)
                MonsterToGO.position = Vector3.Lerp(MonsterToGO.position, FinalPosition, 3f * Time.deltaTime);
            Timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
