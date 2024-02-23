using UnityEngine;
[System.Serializable]
public class Horde
{
    [SerializeField]
    public string HordeType;   // Treasure, NPC, End
    [SerializeField]
    public float hordeLenght;
    [SerializeField]
    public string[] MonsterTypes;
    [SerializeField]
    public int treasureKind;
    [SerializeField]
    public int npcKind;
    [SerializeField]
    public string[] npcLines;
    [SerializeField]
    public int[] npcLinesRandomizer;
    [SerializeField]
    public float notAnimatemovingToTime;
}
