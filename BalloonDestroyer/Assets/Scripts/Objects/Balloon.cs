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
            //Debug.Log("isMoving");
            return;
        }
        Vector2 dir = MovementHelper.directionToVector(finalWinddirection);
        if (dir == Vector2.zero) return;
        if (IsHeadWind(transform.position+(Vector3)dir,dir)) return;
        StartCoroutine(MoveAlongWind(dir));
    }

    IEnumerator MoveAlongWind(Vector2 dir)
    {
        //Debug.Log("dir"+dir);
        isMoving = true;
        while (true)
        {
            if (dir != (Vector2)MovementHelper.directionToVector(Direction))
            {
                isMoving =  false;
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
                if (c.GetComponent<Trigger>() != null) { c.GetComponent<Trigger>().Triggered();  isMoving = false;yield break; }
            }

            yield return null;
        }
        isMoving = false;
    }

    public bool IsMoving() => isMoving;
    public int GetPriority() => 0; // 氣球優先度 0
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public bool IsHeadWind(Vector3 position, Vector2 itsDir)
    {
        Vector2 fanDir = MovementHelper.directionToVector(GameManager.Instance.CheckLocalWind(position));
        if(Vector2.Distance(fanDir+itsDir,Vector2.zero)<0.1)return true;//對沖
        return false;
    }
}