using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TouchController touchController;

    [SerializeField]
    TileManager tileManager;

    [SerializeField]
    UIManager uiManager;

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
        touchController.EndedTouching += tileManager.TouchTile;
        touchController.SwipedTouching += tileManager.SwipeTile;
        tileManager.CompletedMove += touchController.SetTouchable;
        tileManager.CreatedUnit += uiManager.UpdateGold;
        tileManager.SwipedTile += uiManager.UpdateStep;
        tileManager.stageManager.ClickStartButton += uiManager.SetCameraBattleMode;
    }

    void UnBindEvents()
    {
        touchController.BeganTouching -= tileManager.SelectTile;
        touchController.EndedTouching -= tileManager.TouchTile;
        touchController.SwipedTouching -= tileManager.SwipeTile;
        tileManager.CompletedMove -= touchController.SetTouchable;
        tileManager.CreatedUnit -= uiManager.UpdateGold;
        tileManager.SwipedTile -= uiManager.UpdateStep;
        tileManager.stageManager.ClickStartButton -= uiManager.SetCameraBattleMode;
    }
}
