using UnityEngine;

public class fan : MonoBehaviour, Iblow
{
    Direction direction;
    public int GetPriority()
    {
        return 2;
    }
    public void Blow(Direction[,] windMap)
    {
        if (windMap == null)return;

    }
}
