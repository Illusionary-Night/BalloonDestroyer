using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;


public class Fan : MonoBehaviour, IBlower
{
    [SerializeField]direction direction;
    [SerializeField] int windStrength;
    private Animator[] allAnimators;

    bool ONOFF = true;

    private void Awake()
    {
        allAnimators = GetComponentsInChildren<Animator>();
        if (allAnimators == null || allAnimators.Length == 0)
        {
            Debug.LogWarning("在 " + gameObject.name + " 及其子物件上找不到任何 Animator 元件！");
        }
    }
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
        if(!ONOFF)return false;
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
    public void TurnOff()
    {
        ONOFF = false;
        foreach (var animator in allAnimators)
        {
            if (animator != null) animator.speed = 0;
        }
    }
}
