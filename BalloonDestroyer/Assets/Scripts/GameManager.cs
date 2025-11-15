using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.InputSystem;


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

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private LevelGenerator levelGenerator;

    public GameState currentState;
    private List<IBlowable> movableObjects = new List<IBlowable>(); // 儲存所有可動物件
    private direction currentWind = direction.NONE; // 玩家選擇的風向



    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // 確保只有一個 GameManager 存在
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // 切換場景時不銷毀
        }
        levelGenerator = GetComponent<LevelGenerator>();
        if (levelGenerator == null)
        {
            Debug.LogError("GameManager can't find \"LevelGenerator\" Component！Please mount LevelGenerator.cs onto the same object.");
        }
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
        // 載入關卡編輯器的 Tilemap
        if (levelGenerator == null)
        {
            currentState = GameState.GameEnd; // 缺少 LevelGenerator，凍結遊戲
            return;
        }

        // 委託 LevelGenerator 載入並生成關卡
        bool didGenerate = levelGenerator.LoadAndGenerateLevel();

        if (!didGenerate)
        {
            Debug.LogError("Level generate Fail!");
            currentState = GameState.GameEnd; // 生成失敗，凍結遊戲
            return;
        }


        // 找場上所有實作了 Iblow 介面的物件儲存起來
        movableObjects = Object.FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None).OfType<IBlowable>().ToList();

        // 根據優先級排序 (帆船 > 氣球)
        movableObjects = movableObjects.OrderByDescending(obj => obj.GetPriority()).ToList();

        // 遊戲開始 玩家開始回合
        currentState = GameState.PlayerTurn;
    }

    void GetKey()
    {
        direction selectedDirection = direction.NONE;
        var keyboard = Keyboard.current;

        if (keyboard.upArrowKey.wasPressedThisFrame) selectedDirection = direction.UP;
        else if (keyboard.downArrowKey.wasPressedThisFrame) selectedDirection = direction.DOWN;
        else if (keyboard.leftArrowKey.wasPressedThisFrame) selectedDirection = direction.LEFT;
        else if (keyboard.rightArrowKey.wasPressedThisFrame) selectedDirection = direction.RIGHT;

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