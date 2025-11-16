using UnityEngine;

public class Follow : MonoBehaviour
{
    void Update()
    {
        transform.position = GameManager.Instance.CurrentBalloon.transform.position;
    }
}
