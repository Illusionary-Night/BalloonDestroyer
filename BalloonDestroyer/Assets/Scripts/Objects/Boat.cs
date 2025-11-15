using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boat : MonoBehaviour, IBlowable
{
    private Collider2D myCollider;
    private Tilemap terrainMap;
    [SerializeField] float speed = 3f;
    [SerializeField] LayerMask obstacleMask; // 指定 Rocks, Fans, Boats 所在 Layer
    bool isMoving = false;

    public bool IsMoving() => isMoving;
    public int GetPriority() => 1;
    private void Awake()
    {
        myCollider = GetComponent<Collider2D>();  // 取得自己的 Collider2D
    }
    public void StartMove(direction finalWinddirection)
    {
        if (terrainMap == null)
        {
            //terrainMap = GameObject.Find("Water_Map").GetComponent<Tilemap>();
        }
        if (IsMoving()) return;
        Vector2 dir = MovementHelper.directionToVector(finalWinddirection);
        if (dir == Vector2.zero) return;
        StartCoroutine(MoveAlongWind(dir));
    }

    IEnumerator MoveAlongWind(Vector2 dir)
    {
        isMoving = true;
        while (true)
        {
            if (!ConditionMet(dir)) break;
            
            
            Vector3 target = transform.position + (Vector3)dir;
            while ((transform.position - target).sqrMagnitude > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
            transform.position = target;

            yield return null;
        }
        isMoving = false;
    }
    private bool ConditionMet(Vector2 dir)
    {
        Vector3Int tarPos = Vector3Int.RoundToInt(transform.position + (Vector3)dir - new Vector3(0.5f, 0.5f, 0));
        //Debug.Log("tarPos: " + tarPos);
        TileBase[,] tileBase = LevelGenerator.Instance.GetMapData(TilemapType.WaterMap);
        //Debug.Log("tileBase: " + tileBase);
        TileBase tile = LevelGenerator.Instance.GetTilemap(TilemapType.WaterMap).GetTile(tarPos);
        if (tile == null)
        {
            //Debug.Log("land / no water here");
            return false;
        }
        // 檢查前方一格是否被阻擋
        RaycastHit2D hit = Physics2D.Raycast(transform.position+ (Vector3)dir, (Vector3)dir, 0.2f, obstacleMask);
        if (hit.collider != null && hit.collider != myCollider)
        {
            // 打到別的東西（不是自己），才當作被擋住
            // Debug.Log("Boat blocked by: " + hit.collider.name);
            return false;
        }

        return true;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}

