using Unity.VisualScripting;
using UnityEngine;


public class fan : MonoBehaviour
{
    direction direction;
    Vector2Int position;
    public int GetPriority()
    {
        return 2;
    }
    public void Blow(direction[,] windMap)
    {
        if (windMap == null) return;
        if (direction == direction.UP)
        {
            for (int y = position.y; y < windMap.GetLength(1); y++) windMap[position.x, y] = direction.UP;
        }
        else if (direction == direction.DOWN)
        {
            for (int y = position.y; y >= 0; y--) windMap[position.x, y] = direction.DOWN;
        }
        else if (direction == direction.RIGHT)
        {
            for (int x = position.x; x < windMap.GetLength(0); x++) windMap[x, position.y] = direction.RIGHT;
        }
        else if (direction == direction.LEFT)
        {
            for (int x = position.x; x >= windMap.GetLength(0); x--) windMap[x, position.y] = direction.LEFT;
        }
        else
        {
            Debug.Log("fan direction error");
        }
    }
}
