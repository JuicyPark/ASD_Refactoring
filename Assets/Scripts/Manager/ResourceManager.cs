using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    public int gold = 20;
    public int maxStep = 20;
    public int step = 20;
    public int level = 1;

    [SerializeField]
    int goldCost = 5;
    [SerializeField]
    int stepCost = 1;

    public void UseGold() => gold -= goldCost;

    public void UseStep() => step -= stepCost;

    public bool UnitCreatable()
    {
        return gold >= goldCost;
    }

    public bool TileMoveable()
    {
        return step >= stepCost;
    }
}
