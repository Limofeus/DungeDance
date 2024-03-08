using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHandler : MonoBehaviour
{
    public bool notSpawnArrows;
    public float arrowSpeed;
    public bool autoMod;
    public int beatsLeft;
    public int beatsToWait;
    [SerializeField] private Transform arrowSpawner;
    public float spawnerOffset;
    [SerializeField] private MainManager mainManager;
    [SerializeField] private CurseHandler curseHandler;
    private Monster nextMonsterComp;

    public void SetNextMonsterComp(Monster monster)
    {
        nextMonsterComp = monster;
    }
    public void SpawnArrow()
    {
        beatsLeft = Mathf.FloorToInt(mainManager.timeRemaining / mainManager.timeBetweenBeats) - 1;

        if (!notSpawnArrows && beatsToWait <= 0)
        {
            GameObject Arrow;
            Horde currentHorde = mainManager.GetCurrentHorde();
            var arrowPrefabs = mainManager.MonsterComp.ArrowPrefabs;
            //GameObject Arrow = Instantiate(ArrowPrefabs[Random.Range(0, ArrowPrefabs.Length)], Spawner.position, Quaternion.identity);
            if (mainManager.timeRemaining > mainManager.arrowTravelTime)
            {
                if (currentHorde.HordeType == "")
                {
                    Arrow = Instantiate(arrowPrefabs[Random.Range(0, arrowPrefabs.Length)], arrowSpawner.position, arrowSpawner.rotation, mainManager.allUiHolder);
                }
                else
                {
                    Arrow = null;
                }
            }
            else
            { //OH SHIT, ITS LINE 666 (he is somwere around here O_O)
                if (mainManager.MonsterHordeCounter + 1 < currentHorde.MonsterTypes.Length)
                    Arrow = Instantiate(nextMonsterComp.ArrowPrefabs[Random.Range(0, nextMonsterComp.ArrowPrefabs.Length)], arrowSpawner.position, arrowSpawner.rotation, mainManager.allUiHolder);
                else
                    Arrow = null;
            }
            if (Arrow != null)
            {
                Arrow NewArComp = Arrow.GetComponent<Arrow>();
                NewArComp.Speed = arrowSpeed;
                NewArComp.Auto = autoMod;
                NewArComp.Manager = mainManager;

                if (mainManager.MonsterHordeCounter + 1 < currentHorde.MonsterTypes.Length)
                {
                    NewArComp.lastArrow = false;
                }
                else
                {
                    NewArComp.lastArrow = (mainManager.timeRemaining < (spawnerOffset + (mainManager.timeBetweenBeats * arrowSpeed)) / arrowSpeed) && mainManager.timeRemaining > 0;  // WELP, -3 hours of my life, and i have a fix that looks like SHIT and works like SHIT
                }

                //NewArComp.Manager.Monster = Monster;
                NewArComp.starto();
                if (curseHandler.preCursed)
                    NewArComp.arrowVisual.AddCurse(curseHandler.currentCurseId);
                //Monster.SendMessage("ArrowSpawned", NewArComp);
            }
        }

        if (beatsToWait > 0) beatsToWait--;

    }
}
