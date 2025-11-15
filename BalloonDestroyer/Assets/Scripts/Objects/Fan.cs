using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class fan : MonoBehaviour, IBlower
{
    direction direction;
    int windStrength;
    public void SetWindStrength(int strength)
    {
        if(windStrength < 0)
        {
            Debug.Log("windStrengthError");
            return;
        }
        windStrength = strength;
    }
    public int GetWindStrength()
    {
        return windStrength;
    }
    public bool PositionIsInfluenced(Vector2 targetpos)
    {
        //Vector2Int delta = MovementHelper.directionToVector(direction);
        //Vector2 fanpos = LevelGenerator.Instance.GetMapData(TilemapType.ObjectMap)[0,0].WorldToCell(transform.position);
        //Vector2 tmppos = new Vector2();
        //tmppos = fanpos;
        //for(int i =1; i < windStrength; i++)
        //{
        //    tmppos += delta;
        //    if (tmppos==targetpos)
        //    {
        //        return true;
        //    }
        //}
        return false;
    }
    public direction GetWindDirection()
    {
        return direction;
    }

}
