using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField]
    Tile[] tiles;

    Tile selectedTile;
    Tile[] selectedTiles = new Tile[7];

    [SerializeField]
    Transform swipeContainer;



    public void SelectTiles(Vector3 position, Direction direction)
    {
        selectedTile = SelectTile(position);

        if (selectedTile == null)
            return;

        if (direction == Direction.UP || direction == Direction.DOWN)
        {
            int tileIndex = 0;
            foreach (var currentTile in tiles)
            {
                if (Mathf.Approximately(currentTile.transform.position.x, selectedTile.transform.position.x))
                {
                    selectedTiles[tileIndex] = currentTile;
                    tileIndex++;
                }
            }
        }
        else if (direction == Direction.RIGHT || direction == Direction.LEFT)
        {
            int tileIndex = 0;
            foreach (var currentTile in tiles)
            {
                if (Mathf.Approximately(currentTile.transform.position.z, selectedTile.transform.position.z))
                {
                    selectedTiles[tileIndex] = currentTile;
                    tileIndex++;
                }
            }
        }

        ContainToTransform(swipeContainer);
        StartCoroutine(CMoveSwipeContainer(direction));
    }

    Tile SelectTile(Vector3 position)
    {
        Tile returnTile;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if (hit.collider != null)
            {
                returnTile = hit.transform.gameObject.GetComponent<Tile>();
                return returnTile;
            }
        }
        return null;
    }

    void ContainToTransform(Transform parent)
    {
        foreach (var currentTile in selectedTiles)
        {
            currentTile.transform.SetParent(parent);
        }
    }

    Vector3 ParseDirectionToVector(Direction direction)
    {
        if (direction == Direction.DOWN)
            return Vector3.back * 0.1f;
        if (direction == Direction.UP)
            return Vector3.forward * 0.1f;
        if (direction == Direction.RIGHT)
            return Vector3.right * 0.1f;
        if (direction == Direction.LEFT)
            return Vector3.left * 0.1f;
        return Vector3.zero;
    }

    IEnumerator CMoveSwipeContainer(Direction direction)
    {
        Vector3 vector = ParseDirectionToVector(direction);
        for (int i = 0; i < 10; i++)
        {
            swipeContainer.transform.Translate(vector);
            yield return new WaitForSeconds(0.01f);
        }
        ContainToTransform(transform);
    }
}
