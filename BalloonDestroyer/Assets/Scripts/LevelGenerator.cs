using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TerrainUtils;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

/// <summary>
/// 專門負責載入關卡 Prefab。
/// (現在使用 Prefab Tile，這個腳本不再需要手動生成物件)
/// </summary>
public enum TilemapType
{
    ObjectMap,
    ObstacleMap,
    TerrainMap,
    WaterMap
}
public class LevelGenerator : MonoBehaviour
{
    [Header("Level Manager Hook (Debug)")]
    [Tooltip("【僅供測試用】請拖曳一個「關卡 Prefab」(Level_01.prefab)。")]
    public GameObject debug_TestLevelPrefab; // (這就是接孔)
    public static LevelGenerator Instance { get; private set; }
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
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); 
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // 切換場景時不銷毀
        }
    }

    // --- 【這是您的接孔】 ---
    private GameObject GetLevelPrefabFromLevelManager()
    {
        // TODO 嘗試尋找您寫的 LevelManager

        // 如果找不到 LevelManager，則使用我們設定的「測試用 Prefab」
        return debug_TestLevelPrefab;
    }
    public Tilemap GetTilemap(TilemapType tileType)
    {
        var level = GetLevelPrefabFromLevelManager();
        Tilemap tilemap = null;
        if (level == null)
        {
            Debug.LogError("Level prefab not found!");
            return null;
        }
        switch (tileType)
        {
            case TilemapType.ObjectMap:
                tilemap = level.transform.Find("Object_Map")?.GetComponent<Tilemap>();
                break;
            case TilemapType.ObstacleMap:
                tilemap = level.transform.Find("Obstacle_Map")?.GetComponent<Tilemap>();
                break;
            case TilemapType.TerrainMap:
                tilemap = level.transform.Find("Terrain_Map")?.GetComponent<Tilemap>();
                break;
            case TilemapType.WaterMap:
                tilemap = level.transform.Find("Water_Map")?.GetComponent<Tilemap>();
                break;

        }
        if (tilemap == null)
        {
            Debug.LogError("tilemap not found!");
        }
        return tilemap;
    }
    public TileBase[,] GetMapData(TilemapType tileType)
    {
        Tilemap tilemap = GetTilemap(tileType);
        BoundsInt bounds = tilemap.cellBounds;

        int w = bounds.size.x;
        int h = bounds.size.y;

        TileBase[,] map = new TileBase[w, h];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Vector3Int pos = new Vector3Int(bounds.x + x, bounds.y + y, 0);
                map[x, y] = tilemap.GetTile(pos);
            }
        }
        return map;
    }
}