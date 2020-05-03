using System.Collections;
using UnityEngine;
using System;
using UnityEditor.Rendering;

public class TileManager : MonoBehaviour
{
    public Action CompletedMove;

    [SerializeField]
    Tile tilePrefab;
    Tile selectedTile;

    [SerializeField]
    int rowSize = 7;
    [SerializeField]
    int columSize = 7;

    Tile[] rowTiles;
    Tile[] columTiles;
    Tile[] tiles;

    [SerializeField]
    Transform swipeContainer;

    [SerializeField]
    LayerMask blockLayerMask;

    [SerializeField]
    UnitData unitData;

    [SerializeField]
    ArrowSpawnsor arrowSpawnsor;

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

    public void TouchTile()
    {
        CompletedMove?.Invoke();

        if (selectedTile != null)
            ActivateTile();
    }

    public void SelectTiles(Direction direction)
    {
        if (selectedTile == null)
        {
            CompletedMove?.Invoke();
            return;
        }

        if (!ResourceManager.Instance.TileMoveable())
            return;

        ResourceManager.Instance.UseStep();

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
        rowTiles = new Tile[rowSize];
        columTiles = new Tile[columSize];
        tiles = new Tile[rowSize * columSize];

        int tileIndex = 0;
        for (int i = 0; i < columSize; i++)
        {
            for (int j = 0; j < rowSize; j++)
            {
                Vector3 tilePosition = new Vector3(j, 0, i);
                Tile tile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(90f, 0, 0), transform);

                tile.xIndex = j;
                tile.zIndex = i;

                tiles[tileIndex] = tile;
                tileIndex++;
            }
        }

        arrowSpawnsor.GetTileSize(rowSize, columSize);
        arrowSpawnsor.SetArrowContainerPosition();
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
                    tile.transform.position = Vector3.right * tile.transform.position.x
                        + Vector3.forward * -1 + Vector3.forward * transform.position.z;
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
                    tile.transform.position = Vector3.right * tile.transform.position.x
                        + Vector3.forward * columTiles.Length + Vector3.forward * transform.position.z;
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
                    tile.transform.position = Vector3.forward * tile.transform.position.z
                        + Vector3.right * rowTiles.Length + Vector3.right * transform.position.x;
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
                    tile.transform.position = Vector3.forward * tile.transform.position.z
                        + Vector3.right * -1 + Vector3.right * transform.position.x;
                }
            }
        }
    }

    void InitTiles(Direction direction)
    {
        selectedTile = null;
        if (direction == Direction.DOWN || direction == Direction.UP)
        {
            for (int i = 0; i < columTiles.Length; i++)
                columTiles[i] = null;
        }
        else if (direction == Direction.LEFT || direction == Direction.RIGHT)
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

    Tile FindNearSameTile()
    {
        Collider[] hitColliders = Physics.OverlapSphere(selectedTile.transform.position, 0.5f, blockLayerMask);
        foreach (var nearTileCollider in hitColliders)
        {
            Tile nearTile = nearTileCollider.GetComponent<Tile>();
            if (nearTile == selectedTile)
                continue;

            if (nearTile.level == selectedTile.level && nearTile.unitIndex == selectedTile.unitIndex)
            {
                return nearTile;
            }
        }
        return null;
    }

    void SetUnit(int unitIndex)
    {
        if (selectedTile.unit != null)
            Destroy(selectedTile.unit.gameObject);
        selectedTile.spriteRenderer.sprite = unitData.LevelUnitDatas[selectedTile.level].unitSprites[unitIndex];

        selectedTile.unit = Instantiate(unitData.LevelUnitDatas[selectedTile.level].unitPrefabs[unitIndex]
            , selectedTile.transform.position, Quaternion.identity, selectedTile.transform);
        selectedTile.unit.gameObject.SetActive(false);
    }

    void InitTile(Tile tile)
    {
        if (tile.unit != null)
            Destroy(tile.unit.gameObject);
        tile.spriteRenderer.sprite = unitData.noneSelectTileSprite;
        tile.level = 0;
    }

    void CreateUnit()
    {
        selectedTile.level++;
        selectedTile.unitIndex = UnityEngine.Random.Range(0, unitData.LevelUnitDatas[selectedTile.level].unitSprites.Length);
        SetUnit(selectedTile.unitIndex);
    }

    void MergeUnit(Tile nearTile)
    {
        InitTile(nearTile);
        CreateUnit();
    }

    void ActivateTile()
    {
        if (selectedTile.level >= unitData.LevelUnitDatas.Length - 1)
            return;

        if (selectedTile.level == 0)
        {
            if (!ResourceManager.Instance.UnitCreatable())
                return;

            ResourceManager.Instance.UseGold();
            CreateUnit();
        }
        else
        {
            Tile nearTile = FindNearSameTile();
            if (nearTile != null)
            {
                MergeUnit(nearTile);
            }
        }
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
