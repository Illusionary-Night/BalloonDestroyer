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
    private GameObject spawnedLevelRoot;
    /// <summary>
    /// GameManager 呼叫這個函式來執行關卡生成
    /// </summary>
    public bool LoadAndGenerateLevel()
    {
        GameObject levelPrefabToLoad = GetLevelPrefabFromLevelManager();
        if (levelPrefabToLoad == null)
        {
            Debug.LogError("沒有指定要載入的關卡 Prefab！");
            return false;
        }

        // 清除舊關卡 (可選)
        if (spawnedLevelRoot != null)
            Destroy(spawnedLevelRoot);

        spawnedLevelRoot = Instantiate(levelPrefabToLoad, Vector3.zero, Quaternion.identity);
        Debug.Log("關卡 Prefab 已生成。");
        return true;
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
            //DontDestroyOnLoad(this.gameObject); // 切換場景時不銷毀
        }
    }

    // --- 【這是您的接孔】 ---
    private GameObject GetLevelPrefabFromLevelManager()
    {
        // TODO 嘗試尋找您寫的 LevelManager
        if (LevelManager.Instance != null)
        {
            GameObject prefab = LevelManager.Instance.GetSelectedLevelPrefab();
            if (prefab != null) return prefab;
        }

        // 無選擇關卡時 fallback 使用測試用 prefab
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