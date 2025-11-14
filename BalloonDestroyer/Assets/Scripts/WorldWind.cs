using UnityEngine;

public class WorldWind : Iblow
{
    private Direction direction;
    public void Blow(Direction[,] windMap)
    {
        if (windMap == null)return;
        for (int x = 0; x < windMap.GetLength(0); x++)
        {
            for (int y = 0; y < windMap.GetLength(1); y++)
            {
                windMap[x, y] = direction;
            }
        }
    }
    public void SetDirection(Direction direction)
    {
        this.direction = direction;
    }
    public Direction GetDirection()
    {
        return this.direction;
    }
    public WorldWind()
    {
        direction = Direction.NULL;
    }
    public int GetPriority()
    {
        return 1;
    }
}