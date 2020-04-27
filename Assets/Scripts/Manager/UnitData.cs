using UnityEngine;
using UnityEditor;

[CreateAssetMenu]
public class UnitData : ScriptableObject
{
    public Sprite[] unitSprites;
    public Unit[] unitPrefabs;
}