using System;
using UnityEngine;

public enum Direction
{
    NONE, UP, DOWN, LEFT, RIGHT
}

public class TouchController : MonoBehaviour
{
    public Action<Vector3, Direction> SwipedScreen;

    Vector2 startTouch;
    Vector2 endTouch;

    [SerializeField]
    float touchSensitive = 400f;

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouch = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouch = Input.GetTouch(0).position;
            SwipedScreen?.Invoke(Input.mousePosition, GetDirection(endTouch - startTouch));
        }
    }

    Vector3 GetInputPosition(Vector3 mousePosition)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(mousePosition);
        point.y = 1;
        return point;
    }

    Direction GetDirection(Vector3 directionVector)
    {
        if (SMath.ABS(directionVector.x) < touchSensitive && SMath.ABS(directionVector.y) < touchSensitive)
        {
            return Direction.NONE;
        }

        if (SMath.ABS(directionVector.x) > SMath.ABS(directionVector.y))
        {
            if (directionVector.x > 0)
            {
                return Direction.RIGHT;
            }
            else
            {
                return Direction.LEFT;
            }
        }
        else
        {
            if (directionVector.y > 0)
            {
                return Direction.UP;
            }
            else
            {
                return Direction.DOWN;
            }
        }
    }
}
