using UnityEngine;


interface IBlowable
{
    /// <summary>
    /// GameManager 呼叫這個來命令物件根據風向開始移動
    /// </summary>
    void StartMove(direction finalWinddirection);

    /// <summary>
    /// GameManager 呼叫這個來檢查物件是否還在移動
    /// </summary>
    bool IsMoving();

    /// <summary>
    /// GameManager 呼叫這個來排序 (帆船回傳 1, 氣球回傳 0)
    /// </summary>
    int GetPriority();
    Vector3 GetPosition();
}