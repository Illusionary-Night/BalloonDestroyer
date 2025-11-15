using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] Gear gear;
    public void Triggered()
    {
        Debug.Log("Triggered");
        if (gear == null) return;
        gear.Activate();
        Destroy(gameObject);
    }
}
