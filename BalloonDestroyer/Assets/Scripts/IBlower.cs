using UnityEngine;

interface IBlower
{
    void SetWindStrength(int strength);
    int GetWindStrength();
    Vector3 GetPosition();
    bool PositionIsInfluenced(Vector3 pos);
    direction GetWindDirection();
}