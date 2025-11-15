using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Balloon : MonoBehaviour, IBlowable
{
    [SerializeField] float speed = 3f;
    [SerializeField] LayerMask obstacleMask; // 指定 Rocks, Fans, Boats 所在 Layer
    bool isMoving = false;

    public void StartMove(direction finalWinddirection)
    {
        if (isMoving) return;
        Vector2 dir = MovementHelper.directionToVector(finalWinddirection);
        if (dir == Vector2.zero) return;
        StartCoroutine(MoveAlongWind(dir));
    }

    IEnumerator MoveAlongWind(Vector2 dir)
    {
        isMoving = true;
        while (true)
        {
            // 檢查前方一格是否被阻擋
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, obstacleMask);
            if (hit.collider != null) break;

            Vector3 target = transform.position + (Vector3)dir;
            while ((transform.position - target).sqrMagnitude > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
            transform.position = target;
            // 抵達該格後檢查是否碰到 Goal
            Collider2D[] cols = Physics2D.OverlapPointAll(transform.position);
            foreach (var c in cols)
            {
                if (c.GetComponent<Goal>() != null) { GameManager.Instance.EndGame(true); isMoving = false; yield break; }
            }

            yield return null;
        }
        isMoving = false;
    }

    public bool IsMoving() => isMoving;
    public int GetPriority() => 0; // 氣球優先度 0
    public Vector2Int GetPosition()
    {
        Debug.Log("BalloonV3: " + transform.position);
        Tilemap tilemap = LevelGenerator.Instance.GetTilemap(TilemapType.ObjectMap);
        Vector2Int pos = (Vector2Int)tilemap.WorldToCell(transform.position);
        Debug.Log("BalloonV2INT: " + pos);
        return pos;
    }
}