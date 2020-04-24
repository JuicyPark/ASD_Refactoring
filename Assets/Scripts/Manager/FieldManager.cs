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
        touchController.SwipedScreen += tileManager.SelectTiles;
    }

    void UnBindEvents()
    {
        touchController.SwipedScreen -= tileManager.SelectTiles;
    }
}
