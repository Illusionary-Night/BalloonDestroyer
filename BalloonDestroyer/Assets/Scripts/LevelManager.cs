using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 這是一個跨場景的 Singleton (單例)，
/// 負責儲存玩家在「主選單」選擇的關卡 Prefab，
/// 並在「遊戲場景」中將其提供給 LevelGenerator。
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private GameObject selectedLevelPrefab;

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

    /// <summary>
    /// 這個函式將被「主選單」中的 UI 按鈕呼叫。
    /// </summary>
    /// <param name="levelPrefab">要載入的關卡 Prefab (例如 Level_01.prefab)</param>
    public void SelectLevelAndLoadGame(GameObject levelPrefab)
    {
        this.selectedLevelPrefab = levelPrefab;
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// 這個函式將被 LevelGenerator 呼叫，
    /// 用來取得要生成的關卡。
    /// </summary>
    public GameObject GetSelectedLevelPrefab()
    {
        return selectedLevelPrefab;
    }
}