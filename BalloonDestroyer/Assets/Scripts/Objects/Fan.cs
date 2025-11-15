using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;


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
    public bool PositionIsInfluenced(Vector3 targetpos)
    {
        //Debug.Log("PositionIsInfluenced");
        Vector3 delta = MovementHelper.directionToVector(direction);
        Tilemap tilemap = LevelGenerator.Instance.GetTilemap(TilemapType.ObjectMap);
        Vector3 fanpos = transform.position;
        Vector3 tmppos = new Vector3();
        tmppos = fanpos;
        for(int i =1; i <= windStrength; i++)
        {
            tmppos += delta;
            //print("tmp: "+tmppos+"tar: "+targetpos);
            if (Vector3.Distance(tmppos, targetpos) <= 0.3)
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
    public Vector3 GetPosition()
    {
        return this.transform.position;
    }
}
