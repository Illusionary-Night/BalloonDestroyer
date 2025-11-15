using UnityEngine;

public static class MovementHelper
{
    public static Vector2Int directionToVector(direction d)
    {
        switch (d)
        {
            case direction.UP: return Vector2Int.up;
            case direction.DOWN: return Vector2Int.down;
            case direction.LEFT: return Vector2Int.left;
            case direction.RIGHT: return Vector2Int.right;
            default: return Vector2Int.zero;
        }
    }
}