using Unity.VisualScripting;
using UnityEngine;

public class Fan : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
//using UnityEngine;

//public class fan : MonoBehaviour, Iblow
//{
//    Direction direction;
//    Vector2Int position;
//    public int GetPriority()
//    {
//        return 2;
//    }
//    public void Blow(Direction[,] windMap)
//    {
//        if (windMap == null) return;
//        if (direction == Direction.UP)
//        {
//            for (int y = position.y; y < windMap.GetLength(1); y++) windMap[position.x, y] = Direction.UP;
//        }
//        else if (direction == Direction.DOWN)
//        {
//            for (int y = position.y; y >= 0; y--) windMap[position.x, y] = Direction.DOWN;
//        }
//        else if (direction == Direction.RIGHT)
//        {
//            for (int x = position.x; x < windMap.GetLength(0); x++) windMap[x, position.y] = Direction.RIGHT;
//        }
//        else if (direction == Direction.LEFT)
//        {
//            for (int x = position.x; x >= windMap.GetLength(0); x--) windMap[x, position.y] = Direction.LEFT;
//        }
//        else
//        {
//            Debug.Log("fan direction error");
//        }
//    }
//}
