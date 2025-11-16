using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public GameObject levelPrefab;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LevelManager.Instance.SelectLevelAndLoadGame(levelPrefab);
        });
    }
}
