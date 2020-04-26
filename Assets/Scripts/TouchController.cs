using System;
using UnityEngine;

public enum Direction
{
    NONE, UP, DOWN, LEFT, RIGHT
}

public class TouchController : MonoBehaviour
{
    public Action<Vector3> BeganTouching;
    public Action EndedTouching;
    public Action<Direction> SwipedTouching;

    Vector2 startTouch;
    Vector2 endTouch;

    [SerializeField]
    float touchSensitive = 400f;

    bool touchable = true;

    public void SetTouchable() => touchable = true;

    void Update()
    {
        if (!touchable)
            return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouch = Input.GetTouch(0).position;
            BeganTouching?.Invoke(Input.mousePosition);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchable = false;
            endTouch = Input.GetTouch(0).position;
            var currentDirection = GetDirection(endTouch - startTouch);

            if (currentDirection == Direction.NONE)
                EndedTouching?.Invoke();
            else
                SwipedTouching?.Invoke(GetDirection(endTouch - startTouch));
        }
    }

    Direction GetDirection(Vector3 directionVector)
    {
        if (Mathf.Abs(directionVector.x) < touchSensitive && Mathf.Abs(directionVector.y) < touchSensitive)
            return Direction.NONE;

        if (Mathf.Abs(directionVector.x) > Mathf.Abs(directionVector.y))
        {
            if (directionVector.x > 0)
                return Direction.RIGHT;
            else
                return Direction.LEFT;
        }
        else
        {
            if (directionVector.y > 0)
                return Direction.UP;
            else
                return Direction.DOWN;
        }
    }
}
