using UnityEngine;
using System.Collections.Generic;

public class Trigger : MonoBehaviour
{
    [SerializeField] List<Gear> gears;
    [SerializeField] List<Fan> fans;
    public void Triggered()
    {
        Debug.Log("Triggered");
        foreach (Gear gear in gears)
        {
            if (gear == null) continue; 
            gear.Activate();
        }
        foreach (Fan fan in fans)
        {
            if (fan == null) continue;
            fan.TurnOff();
        }
        Destroy(gameObject);
    }
}
