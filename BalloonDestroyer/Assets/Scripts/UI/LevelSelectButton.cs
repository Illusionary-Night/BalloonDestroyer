using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public GameObject levelPrefab;

    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LevelManager.Instance.SelectLevelAndLoadGame(levelPrefab);
        });
    }
}
