using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public EnemyData[] enemyDatas;

    [SerializeField]
    float spawnDelayTime = 0.5f;

    Transform spawnTransform;
    public Transform SpawnTransform { set => spawnTransform = value; }

    int level = 1;

    IEnumerator CStartStage()
    {
        for (int i = 0; i < enemyDatas[level].enemyCount; i++)
        {
            Enemy enemy = Instantiate(enemyDatas[level].enemyPrefab,
                                      spawnTransform.position,
                                      Quaternion.Euler(Vector3.up * (spawnTransform.rotation.eulerAngles.y + 90f)));
            enemy.SetHealth(enemyDatas[level].health);
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }
}
