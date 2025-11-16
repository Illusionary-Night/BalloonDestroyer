using UnityEngine;

public class Gear : MonoBehaviour
{
    public void Activate()
    {
        Debug.Log("Activate");
        Destroy(gameObject);
    }
}
