using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EnemyData : ScriptableObject
{
    public Sprite enemySprites;
    public Unit enemyPrefab;
    public int enemyCount;
    public float health;
}
