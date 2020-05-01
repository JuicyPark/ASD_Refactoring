using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    GameObject arrowTilePrefab;

    List<GameObject> arrowTiles = new List<GameObject>();

    public void Activate(int arrowSize)
    {
        for (int i = 0; i <= arrowSize; i++)
        {
            if (arrowTiles.Count <= i)
                arrowTiles.Add(Instantiate(arrowTilePrefab, Vector3.right * (i + 1), Quaternion.identity, transform));
            arrowTiles[i].SetActive(true);
        }
    }

    void UnActivate()
    {
        foreach(var arrowTile in arrowTiles)
            arrowTile.SetActive(false);
    }
}
