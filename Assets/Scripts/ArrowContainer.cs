using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainer : MonoBehaviour
{
    Color color;

    [SerializeField]
    float spawnDelayTime = 0.03f;

    [SerializeField]
    GameObject arrowTilePrefab;
    List<GameObject> arrowTiles = new List<GameObject>();

    Transform lastArrowTileTransform;
    public Transform LastArrowTileTransform { get => lastArrowTileTransform; }

    public void SetArrowColor(Color arrowColor)
    {
        color = arrowColor;
        GetComponentInChildren<SpriteRenderer>().color = color + Color.black;
    }

    public void SpawnArrow(int arrowSize)
    {
        for (int i = 0; i <= arrowSize; i++)
        {
            if (arrowTiles.Count <= i)
            {
                GameObject arrowTile = Instantiate(arrowTilePrefab, Vector3.right * (i + 1), Quaternion.identity, transform);
                arrowTile.GetComponent<SpriteRenderer>().color = color;
                arrowTile.SetActive(false);
                arrowTiles.Add(arrowTile);
            }
        }
        lastArrowTileTransform = arrowTiles[arrowSize].transform;
    }

    public IEnumerator CActivate(int arrowSize)
    {
        for (int i = 0; i <= arrowSize; i++)
        {
            arrowTiles[i].SetActive(true);
            yield return new WaitForSeconds(spawnDelayTime);
        }
    }

    void UnActivate()
    {
        foreach (var arrowTile in arrowTiles)
            arrowTile.SetActive(false);
    }
}
