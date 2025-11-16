using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// <summary>
// 這是一個跨場景的 Singleton (單例)，
// 負責儲存玩家在「主選單」選擇的關卡 Prefab，
// 並在「遊戲場景」中將其提供給 LevelGenerator。
// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }
    [SerializeField] List<GameObject> AllLevel;
    private GameObject selectedLevelPrefab;
    [SerializeField] string selectedLevelName;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // <summary>
    // 這個函式將被「主選單」中的 UI 按鈕呼叫。
    // </summary>
    // <param name="levelPrefab">要載入的關卡 Prefab (例如 Level_01.prefab)</param>
    public void SelectLevelAndLoadGame(GameObject levelPrefab)
    {
        Debug.Log("LoadLevel");
        selectedLevelPrefab = levelPrefab;
        selectedLevelName = selectedLevelPrefab.name;
        SceneManager.LoadScene("GameScene");
    }

    // <summary>
    // 這個函式將被 LevelGenerator 呼叫，
    // 用來取得要生成的關卡。
    // </summary>
    public GameObject GetSelectedLevelPrefab()
    {
        return selectedLevelPrefab;
    }
//    public GameObject nextLevel(GameObject nowlevelPrefab)
//    {
//        Debug.Log("nexrLevel");
//        for (int i=0;i<AllLevel.Count;i++)
//        {
//            GameObject tmpLevelPrefab = AllLevel[i];
//            if (tmpLevelPrefab != nowlevelPrefab)continue;
//            Debug.Log("NowLevel: " + i);
//            if (tmpLevelPrefab == null) continue;
//            if (i + 1 >= AllLevel.Count) continue;
//            if (AllLevel[i + 1] == null) continue;
//            Debug.Log("SelectAllLevel: "+ (i+1));
//;           return AllLevel[i+1];
//        }
//        return AllLevel[0];
//    }
}