using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationItemHandler : MonoBehaviour
{
    [SerializeField] private MainManager _mainManager;
    [SerializeField] private BottomTextHandler _bottomTextHandler;
    public GameObject TreasurePrefab;
    public GameObject npcPrefab;

    public void EndItem()
    {
        //Debug.Log("Here i go!");
        _mainManager.ItemSpawned = false;
        _mainManager.Monster.GetComponent<ItemAway>().GoAway();
        Destroy(_mainManager.Monster);
    }
    public void SpawnTreasure()
    {
        _mainManager.Monster = Instantiate(TreasurePrefab, _mainManager.EnimyHolder.position, _mainManager.EnimyHolder.rotation, _mainManager.EnimyHolder);
        _mainManager.Monster.GetComponent<Chest>().InitializeChestParameters(_mainManager, _mainManager.hordes[_mainManager.hordeCounter].treasureKind);
    }

    public void SpawnNPC()
    {

        _mainManager.Monster = Instantiate(npcPrefab, _mainManager.EnimyHolder.position, _mainManager.EnimyHolder.rotation, _mainManager.EnimyHolder);
        _mainManager.Monster.GetComponent<NPC>().InitializeNPCParameters(_mainManager, _mainManager.hordes[_mainManager.hordeCounter].npcKind);

        _bottomTextHandler.SetDialogueParams(true, 0, _mainManager.RMaxTime / _mainManager.GetCurrentHorde().npcLines.Length);
        _bottomTextHandler.DisplayText(_bottomTextHandler.GetNPCNameById(_mainManager.GetCurrentHorde().npcKind), LocalisationSystem.GetLocalizedValue(_mainManager.hordes[_mainManager.hordeCounter].npcLines[0]));
    }
}
