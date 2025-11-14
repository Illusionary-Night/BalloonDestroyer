using UnityEngine;
using System.Collections.Generic; // 【新增】為了使用 List
using System.Linq; // 【新增】為了使用 .All() 和 .OrderBy()
using System.Collections; // (我們暫時不用協程，來貼合您的架構)

public enum Terrain
{
    Grass,
    Water
}

public enum direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE // 無風
}

// 遊戲狀態機，來管理「玩家回合」和「演算回合」
public enum GameState
{
    PlayerTurn,     // 等待玩家輸入
    ObjectsMoving,  // 物件移動中 (此時玩家不能輸入)
    GameEnd         // 遊戲結束
}

public class GM : MonoBehaviour
{
    public static GM Instance { get; private set; }
    public Terrain[,] TerrainMap = new Terrain[10, 10];

    public GameState currentState;
    private List<IBlowable> movableObjects = new List<IBlowable>(); // 儲存所有可動物件
    private direction currentWind = direction.NONE; // 玩家選擇的風向

    // --- 填入您的函式 ---

    void Awake() // (Start 執行前會先執行 Awake)
    {
        Instance = this; // 設定單例
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        // 根據不同的遊戲狀態，執行不同的任務
        switch (currentState)
        {
            case GameState.PlayerTurn:
                GetKey(); // 玩家回合：偵測按鍵
                break;

            case GameState.ObjectsMoving:
                GameLoop(); // 物件移動回合：檢查是否所有物件都停止了
                break;

            case GameState.GameEnd:
                // 遊戲結束，不做任何事
                break;
        }
    }

    void Initialize() // 您的 Initialize
    {
        // TODO 載入關卡編輯器的 Tilemap

        // 找場上所有實作了 Iblow 介面的物件儲存起來
        movableObjects = FindObjectsOfType<MonoBehaviour>().OfType<IBlowable>().ToList();

        // 根據優先級排序 (帆船 > 氣球)
        movableObjects = movableObjects.OrderByDescending(obj => obj.GetPriority()).ToList();

        // 遊戲開始 玩家開始回合
        currentState = GameState.PlayerTurn;
    }

    void GetKey()
    {
        direction selectedDirection = direction.NONE;

        if (Input.GetKeyDown(KeyCode.UpArrow)) selectedDirection = direction.UP;
        else if (Input.GetKeyDown(KeyCode.DownArrow)) selectedDirection = direction.DOWN;
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) selectedDirection = direction.LEFT;
        else if (Input.GetKeyDown(KeyCode.RightArrow)) selectedDirection = direction.RIGHT;

        // 如果玩家按下了有效按鍵
        if (selectedDirection != direction.NONE)
        {
            currentWind = selectedDirection; // 記錄風向
            currentState = GameState.ObjectsMoving; // 切換到「物件移動」狀態
        }
    }

    void Move()
    {
        // 演算階段
        // 迴圈遍歷所有可動物件 (因為已排序，帆船會先動)
        foreach (var obj in movableObjects)
        {
            // TODO 加入「風扇覆蓋」的檢查邏輯
            // 檢查這個物件的位置是否在風扇影響範圍內
            // True 則使用風扇的風向覆蓋 finalWind = CalculateWindFor(obj.position, currentWind)
            // False 則使用全域風向


            direction finalWind = currentWind; // 暫時先用全域風向

            // 命令物件開始移動
            obj.StartMove(finalWind);
        }
    }

    void GameLoop()
    {
        // 這是「等待」階段
        // 每一幀都檢查，是否「所有」物件的 IsMoving() 都回傳 false
        Move();

        bool isAllStopped = movableObjects.All(obj => !obj.IsMoving());
        if (isAllStopped)
        {
            Debug.Log("All objects were stoped, turn to player");
            currentState = GameState.PlayerTurn; // 切換回玩家回合
        }

    }

    public void EndGame(bool didWin)
    {
        if (currentState == GameState.GameEnd) return; // 防止重複呼叫

        currentState = GameState.GameEnd; // 凍結遊戲

        if (didWin)
        {
            Debug.Log("Win！");
        }
        else
        {
            Debug.Log("Loser！");
        }
    }
}