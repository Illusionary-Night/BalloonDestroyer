using UnityEngine;

interface IBlower
{
    void SetWindStrength(int strength);
    int GetWindStrength();
    Vector2Int GetPosition();
    bool PositionIsInfluenced(Vector2Int pos);
    direction GetWindDirection();
}