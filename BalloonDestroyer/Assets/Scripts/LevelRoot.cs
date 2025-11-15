using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// 這是「關卡 Prefab」的根腳本。
/// LevelGenerator 會尋找這個腳本，來取得此關卡對應的 Tilemaps。
/// </summary>
public class LevelRoot : MonoBehaviour
{
    [Tooltip("用於標記物件 (氣球、終點) 生成位置的 Tilemap")]
    public Tilemap objectMap;

    [Tooltip("用於繪製靜態障礙物 (石頭、牆壁) 的 Tilemap")]
    public Tilemap obstacleMap;

    [Tooltip("用於繪製地形 (草地、水) 的 Tilemap")]
    public Tilemap terrainMap;

}