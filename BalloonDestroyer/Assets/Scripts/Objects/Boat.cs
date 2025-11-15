using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Boat : MonoBehaviour, IBlowable
{
    private Tilemap terrainMap;
    [SerializeField] public Sprite Water_0; // 指向 Water_0 圖片資源 
    [SerializeField] float speed = 3f;
    [SerializeField] LayerMask obstacleMask; // 指定 Rocks, Fans, Boats 所在 Layer
    bool isMoving = false;

    public bool IsMoving() => isMoving;
    public int GetPriority() => 1;

    public void StartMove(direction finalWinddirection)
    {
        if (terrainMap == null)
        {
            terrainMap = GameObject.Find("Terrain_Map").GetComponent<Tilemap>();
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
            if (!CondiconditionMet(dir)) break;
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
    private bool CondiconditionMet(Vector2 dir)
    {
        Sprite WhatIsAtForward = terrainMap.GetSprite(terrainMap.WorldToCell(transform.position + new Vector3(dir.x, dir.y, 0)));
        if (WhatIsAtForward != Water_0) return false;
        // 檢查前方一格是否被阻擋
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1f, obstacleMask);
        if (hit.collider != null) return false;
        return true;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
