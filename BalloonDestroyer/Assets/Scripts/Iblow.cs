using UnityEngine;

public interface Iblow
{
    public void Blow(Direction[,] windMap);
    public int GetPriority(); // 先吹後吹的差別 後吹的會把先吹的蓋過去 所以後吹的優先度較高 WorldWind是1
}

