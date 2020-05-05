using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StageManager : MonoBehaviour
{
    public EnemyData[] enemyDatas;
    public Action ClickStartButton;

    [SerializeField]
    float spawnDelayTime = 0.5f;
    Transform spawnTransform;

    bool isStarted;

    public Transform SpawnTransform { set => spawnTransform = value; }
    public bool IsStarted { get => isStarted; }
    bool allEnemySpawn;
    bool allEnemyDestroy;

    public void StartStage()
    {
        if (IsStarted)
            return;

        StartCoroutine(CStartStage());
        ClickStartButton?.Invoke();
        isStarted = true;
    }

    IEnumerator CStartStage()
    {
        for (int i = 0; i < enemyDatas[ResourceManager.Instance.level].enemyCount; i++)
        {
            Enemy enemy = Instantiate(enemyDatas[ResourceManager.Instance.level].enemyPrefab,
                                      spawnTransform.position,
                                      Quaternion.Euler(Vector3.up * (spawnTransform.rotation.eulerAngles.y + 90f)));
            enemy.SetHealth(enemyDatas[ResourceManager.Instance.level].health);
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }
}
