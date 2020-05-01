using UnityEngine;
using UnityEditor;
using System;

[Serializable]
public class LevelUnitData
{
    public Sprite[] unitSprites;
    public Unit[] unitPrefabs;
}


[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public Sprite noneSelectTileSprite;
    public LevelUnitData[] LevelUnitDatas;
}