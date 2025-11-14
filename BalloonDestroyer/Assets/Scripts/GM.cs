using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Terrain
{
    Grass,
    Water
}
public enum Direction
{
    NULL,
    UP, 
    DOWN,
    LEFT, 
    RIGHT
}
public class GM : MonoBehaviour
{
    //[SerializeField]
    public Terrain[,] TerrainMap = new Terrain[10, 10];
    public Direction NowDirection= Direction.NULL;
    public List<Iblow> ObjectList = new List<Iblow>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        GetKey();
    }
    void Initialize()
    {
        
    }
    void GameLoop()
    {
        Initialize();
        while (true)
        {
            GetKey();
            Move();
        }
    }
    void Move()
    {
        bool isDone = false;
        while (true)
        {
            isDone = true;
            foreach (var eachObject in ObjectList)
            {
                isDone = isDone && eachObject.move();
            }
            if (isDone) break;
        }
    }
    void EndGame()
    {

    }
    void GetKey()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NowDirection = Direction.UP;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            NowDirection = Direction.LEFT;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            NowDirection = Direction.DOWN;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            NowDirection = Direction.RIGHT;
        }
    }
}
