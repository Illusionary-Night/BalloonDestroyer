using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Balloon : MonoBehaviour, IBlowable
{
    private Collider2D myCollider;
    [SerializeField] float speed = 3f;
    [SerializeField] LayerMask obstacleMask; // 指定 Rocks, Fans, Boats 所在 Layer
    bool isMoving = false;
    direction Direction;
    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();  // 取得自己的 Collider2D
    }
    public void StartMove(direction finalWinddirection)
    {
        Direction = finalWinddirection;
        if (isMoving)
        {
            Debug.Log("isMoving");
            return;
        }
        Vector2 dir = MovementHelper.directionToVector(finalWinddirection);
        if (dir == Vector2.zero)
        {
            isMoving = false; // 確保停下來
            return;
        }
        StartCoroutine(MoveOneWind(dir));
    }

    IEnumerator MoveOneWind(Vector2 dir)
    {
        isMoving = true;

        // 檢查前方一格是否被阻擋
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, obstacleMask);

        // 如果前方有東西，就立刻停止
        if (hit.collider != null)
        {
            if (dir != (Vector2)MovementHelper.directionToVector(Direction))
            {
                isMoving = false;
                yield break;
            }
            // 檢查前方一格是否被阻擋
            RaycastHit2D hit = Physics2D.Raycast(transform.position + (Vector3)dir, (Vector3)dir, 0.2f, obstacleMask);
            if (hit.collider != null && hit.collider != myCollider)
            {
                // 打到別的東西（不是自己），才當作被擋住
                // Debug.Log("Boat blocked by: " + hit.collider.name);
                break;
            }

            Vector3 target = transform.position + (Vector3)dir;
            //Debug.Log("while start");
            while ((transform.position - target).sqrMagnitude > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
            //Debug.Log("while end");
            transform.position = target;
            // 抵達該格後檢查是否碰到 Goal
            Collider2D[] cols = Physics2D.OverlapPointAll(transform.position);
            foreach (var c in cols)
            {
                if (c.GetComponent<Goal>() != null) { GameManager.Instance.EndGame(true); isMoving = false; yield break; }
                if (c.GetComponent<Needle>() != null) { GameManager.Instance.EndGame(false); isMoving = false; yield break; }
            }

            yield return null;
        }

        // 抵達該格後檢查是否碰到 Goal
        Collider2D[] cols = Physics2D.OverlapPointAll(transform.position);
        foreach (var c in cols)
        {
            if (c.GetComponent<Goal>() != null)
            {
                GameManager.Instance.EndGame(true);
                isMoving = false;
                yield break; // 結束協程
            }
        }

        //移動完一格，設定 isMoving = false
        //    這樣 GameManager 才能在下一幀重新計算風向
        isMoving = false;
    }

    public bool IsMoving() => isMoving;
    public Vector3 GetPosition() => transform.position;
    public int GetPriority() => 0; // 氣球優先度 0
}