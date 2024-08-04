using UnityEngine;

[System.Serializable]
public class ScoreJoyBonuses
{
    //BONUSES AND MULTIPLIERS
    //Score bonuses
    public float scoreMonsterBonus = 0f; //This one is for when the monster gives player score, not tied to follows
    public float scoreHitType0Bonus = 0f;
    public float scoreHitType1Bonus = 0f;
    public float scoreHitType2Bonus = 0f;
     public float scoreHitType3Bonus = 0f;
     public float scoreAllHitBonus = 0f;
    //Score multipliers
     public float scoreMonsterMultiplier = 1f; //Same as above
     public float scoreHitType0Multiplier = 1f;
     public float scoreHitType1Multiplier = 1f;
     public float scoreHitType2Multiplier = 1f;
     public float scoreHitType3Multiplier = 1f;
     public float scoreAllHitMultiplier = 1f;
    //Joy bonuses
     public float joyHitType0Bonus = 0f;
     public float joyHitType1Bonus = 0f;
     public float joyHitType2Bonus = 0f;
     public float joyHitType3Bonus = 0f;
     public float joyAllHitBonus = 0f;
    //Joy multipliers
     public float joyHitType0Multiplier = 1f;
     public float joyHitType1Multiplier = 1f;
     public float joyHitType2Multiplier = 1f;
     public float joyHitType3Multiplier = 1f;
     public float joyAllHitMultiplier = 1f;
     public float joyDynamicHitMultiplier = 1f;
    //Some other stat changes
     public float followMonsterToScoreMultiplier = 1f;
     public float followMonsterToJoyMultiplier = 1f;
     public float attractionToScoreMultiplier = 1f;
     public float attractionToJoyMultiplier = 1f;
}
