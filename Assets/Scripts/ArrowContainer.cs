using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContainer : MonoBehaviour
{
    public Color color;

    [SerializeField]
    GameObject arrowTilePrefab;

    List<GameObject> arrowTiles = new List<GameObject>();

    public void SetArrowColor(Color arrowColor)
    {
        color = arrowColor;
        GetComponentInChildren<SpriteRenderer>().color = color + Color.black;
    }

    public void Activate(int arrowSize)
    {
        for (int i = 0; i <= arrowSize; i++)
        {
            if (arrowTiles.Count <= i)
            {
                GameObject arrowTile = Instantiate(arrowTilePrefab, Vector3.right * (i + 1), Quaternion.identity, transform);
                arrowTile.GetComponent<SpriteRenderer>().color = color;
                arrowTiles.Add(arrowTile);
            }
            arrowTiles[i].SetActive(true);
        }
    }

    void UnActivate()
    {
        foreach(var arrowTile in arrowTiles)
            arrowTile.SetActive(false);
    }
}
