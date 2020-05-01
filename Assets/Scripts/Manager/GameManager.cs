using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TouchController touchController;

    [SerializeField]
    TileContainer tileContainer;

    void Start()
    {
        BindEvents();
    }

    void OnDestroy()
    {
        UnBindEvents();
    }

    void BindEvents()
    {
        touchController.BeganTouching += tileContainer.SelectTile;
        touchController.EndedTouching += tileContainer.TouchTile;
        touchController.SwipedTouching += tileContainer.SelectTiles;
        tileContainer.CompletedMove += touchController.SetTouchable;
    }

    void UnBindEvents()
    {
        touchController.BeganTouching -= tileContainer.SelectTile;
        touchController.EndedTouching -= tileContainer.TouchTile;
        touchController.SwipedTouching -= tileContainer.SelectTiles;
        tileContainer.CompletedMove -= touchController.SetTouchable;
    }
}
