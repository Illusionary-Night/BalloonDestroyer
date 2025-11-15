using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;


public class fan : MonoBehaviour, IBlower
{
    [SerializeField]direction direction;
    [SerializeField]int windStrength;
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
    public bool PositionIsInfluenced(Vector2Int targetpos)
    {
        Debug.Log("PositionIsInfluenced");
        Vector2Int delta = MovementHelper.directionToVector(direction);
        Tilemap tilemap = LevelGenerator.Instance.GetTilemap(TilemapType.ObjectMap);
        Vector2Int fanpos = (Vector2Int)tilemap.WorldToCell(transform.position);
        Vector2Int tmppos = new Vector2Int();
        tmppos = fanpos;
        for(int i =1; i <= windStrength; i++)
        {
            tmppos += delta;
            print("tmp: "+tmppos+"tar: "+targetpos);
            if (tmppos==targetpos)
            {
                return true;
            }
        }
        return false;
    }
    public direction GetWindDirection()
    {
        return direction;
    }
    public Vector2Int GetPosition()
    {
        Tilemap tilemap = LevelGenerator.Instance.GetTilemap(TilemapType.ObjectMap);
        Vector2Int pos = (Vector2Int)tilemap.WorldToCell(transform.position);
        return pos;
    }
}
