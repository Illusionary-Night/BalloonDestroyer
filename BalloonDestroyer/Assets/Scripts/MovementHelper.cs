using UnityEngine;

public static class MovementHelper
{
    public static Vector2 directionToVector(direction d)
    {
        switch (d)
        {
            case direction.UP: return Vector2.up;
            case direction.DOWN: return Vector2.down;
            case direction.LEFT: return Vector2.left;
            case direction.RIGHT: return Vector2.right;
            default: return Vector2.zero;
        }
    }
}