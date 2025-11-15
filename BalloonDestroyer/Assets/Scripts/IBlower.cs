using UnityEngine;


interface IBlower
{
    void SetWindStrength(int strength);
    int GetWindStrength();
    bool PositionIsInfluenced(Vector2 pos);
    direction GetWindDirection();
}