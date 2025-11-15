using UnityEngine;

public static class MovementHelper
{
    public static Vector3 directionToVector(direction d)
    {
        switch (d)
        {
            case direction.UP: return Vector3.up;
            case direction.DOWN: return Vector3.down;
            case direction.LEFT: return Vector3.left;
            case direction.RIGHT: return Vector3.right;
            default: return Vector3.zero;
        }
    }
}