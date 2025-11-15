using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 專門負責載入關卡 Prefab。
/// (現在使用 Prefab Tile，這個腳本不再需要手動生成物件)
/// </summary>
public class LevelGenerator : MonoBehaviour
{
    [Header("Level Manager Hook (Debug)")]
    [Tooltip("【僅供測試用】請拖曳一個「關卡 Prefab」(Level_01.prefab)。")]
    public GameObject debug_TestLevelPrefab; // (這就是接孔)

    /// <summary>
    /// GameManager 呼叫這個函式來執行關卡生成
    /// </summary>
    public bool LoadAndGenerateLevel()
    {
        // 取得要載入的關卡 Prefab (未來由 LevelManager 提供)
        GameObject levelPrefabToLoad = GetLevelPrefabFromLevelManager();

        if (levelPrefabToLoad == null)
        {
            Debug.LogError("沒有指定要載入的關卡 Prefab！(請檢查 LevelManager 或 debug_TestLevelPrefab 欄位)");
            return false; // 生成失敗
        }

        // 生成關卡物件 (它會包含 Grid 和所有 Tilemaps)
        // 【重要】當這個 Prefab 被生成時 (Instantiate)，
        // 所有被繪製在 Tilemap 上的 "Prefab Tile" 都會自動執行它們的生成邏輯。
        Instantiate(levelPrefabToLoad, Vector3.zero, Quaternion.identity);

        Debug.Log("關卡 Prefab 已生成。");
        return true; // 生成成功
    }


    // --- 【這是您的接孔】 ---
    private GameObject GetLevelPrefabFromLevelManager()
    {
        // TODO 嘗試尋找您寫的 LevelManager

        // 如果找不到 LevelManager，則使用我們設定的「測試用 Prefab」
        return debug_TestLevelPrefab;
    }
}