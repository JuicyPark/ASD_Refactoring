using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    [SerializeField]
    TouchController touchController;

    [SerializeField]
    TileManager tileManager;

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
        touchController.BeganTouching += tileManager.SelectTile;
        touchController.EndedTouching += tileManager.CreateAnimal;
        touchController.SwipedTouching += tileManager.SelectTiles;
        tileManager.CompletedMove += touchController.SetTouchable;
    }

    void UnBindEvents()
    {
        touchController.BeganTouching -= tileManager.SelectTile;
        touchController.EndedTouching -= tileManager.CreateAnimal;
        touchController.SwipedTouching -= tileManager.SelectTiles;
        tileManager.CompletedMove -= touchController.SetTouchable;
    }
}
