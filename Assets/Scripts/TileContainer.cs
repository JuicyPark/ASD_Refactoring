﻿using System.Collections;
using UnityEngine;
using System;

public class TileContainer : MonoBehaviour
{
    public Action CompletedMove;


    [SerializeField]
    Tile tilePrefab;
    Tile selectedTile;

    [SerializeField]
    int rowCount = 7;
    [SerializeField]
    int columCount = 7;
    Tile[] rowTiles;
    Tile[] columTiles;
    Tile[] tiles;

    [SerializeField]
    Transform swipeContainer;

    public void SelectTile(Vector3 position)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray.origin, ray.direction, out hit))
        {
            if (hit.collider != null)
                selectedTile = hit.transform.gameObject.GetComponent<Tile>();
        }
    }

    public void CreateAnimal()
    {
        CompletedMove?.Invoke();

    }

    public void SelectTiles(Direction direction)
    {
        if (selectedTile == null)
        {
            CompletedMove?.Invoke();
            return;
        }

        if (direction == Direction.UP || direction == Direction.DOWN)
        {
            int tileIndex = 0;
            foreach (var currentTile in tiles)
            {
                if (selectedTile.xIndex == currentTile.xIndex)
                {
                    columTiles[tileIndex] = currentTile;
                    tileIndex++;
                }
            }
            ContainToTransform(swipeContainer, columTiles);
        }

        else if (direction == Direction.RIGHT || direction == Direction.LEFT)
        {
            int tileIndex = 0;
            foreach (var currentTile in tiles)
            {
                if (selectedTile.zIndex == currentTile.zIndex)
                {
                    rowTiles[tileIndex] = currentTile;
                    tileIndex++;
                }
            }
            ContainToTransform(swipeContainer, rowTiles);
        }
        ChangeTileIndex(direction);
        StartCoroutine(CMoveSwipeContainer(direction));
    }

    void Start()
    {
        rowTiles = new Tile[rowCount];
        columTiles = new Tile[columCount];
        tiles = new Tile[rowCount * columCount];

        int tileIndex = 0;
        for(int i = 0; i< columCount;i++)
        {
            for(int j = 0; j< rowCount;j++)
            {
                Vector3 tilePosition = new Vector3(j, 0, i);
                Tile tile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(90f,0,0),transform);

                tile.xIndex = j;
                tile.zIndex = i;

                tiles[tileIndex] = tile;
                tileIndex++;
            }
        }
    }

    void ChangeTileIndex(Direction direction)
    {
        if (direction == Direction.UP)
        {
            foreach (var tile in columTiles)
            {
                tile.zIndex++;
                if (tile.zIndex >= columTiles.Length)
                {
                    tile.zIndex = 0;
                    tile.transform.position = Vector3.right * tile.transform.position.x + Vector3.forward * -1 + Vector3.forward * transform.position.z;
                }
            }
        }
        else if (direction == Direction.DOWN)
        {
            foreach (var tile in columTiles)
            {
                tile.zIndex--;
                if (tile.zIndex < 0)
                {
                    tile.zIndex = columTiles.Length - 1;
                    tile.transform.position = Vector3.right * tile.transform.position.x + Vector3.forward * columTiles.Length + Vector3.forward * transform.position.z;
                }
            }
        }
        else if (direction == Direction.LEFT)
        {
            foreach (var tile in rowTiles)
            {
                tile.xIndex--;
                if (tile.xIndex < 0)
                {
                    tile.xIndex = rowTiles.Length - 1;
                    tile.transform.position = Vector3.forward * tile.transform.position.z + Vector3.right * rowTiles.Length + Vector3.right * transform.position.x;
                }
            }
        }
        else if (direction == Direction.RIGHT)
        {
            foreach (var tile in rowTiles)
            {
                tile.xIndex++;
                if (tile.xIndex >= rowTiles.Length)
                {
                    tile.xIndex = 0;
                    tile.transform.position = Vector3.forward * tile.transform.position.z + Vector3.right * -1 + Vector3.right * transform.position.x;
                }
            }
        }
    }

    void InitTiles(Direction direction)
    {
        selectedTile = null;
        if(direction == Direction.DOWN || direction == Direction.UP)
        {
            for(int i = 0; i < columTiles.Length;i++)
                columTiles[i] = null;
        }
        else if(direction == Direction.LEFT || direction == Direction.RIGHT)
        {
            for (int i = 0; i < rowTiles.Length; i++)
                rowTiles[i] = null;
        }
    }

    void ContainToTransform(Transform parent, Tile[] selectedTiles)
    {
        foreach (var currentTile in selectedTiles)
            currentTile.transform.SetParent(parent);
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

        if (direction == Direction.UP || direction == Direction.DOWN)
            ContainToTransform(transform, columTiles);
        else if (direction == Direction.LEFT || direction == Direction.RIGHT)
            ContainToTransform(transform, rowTiles);
        InitTiles(direction);
        CompletedMove?.Invoke();
    }
}