using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
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
    public const int MAXMAPXLENGTH = 64;
    public const int MAXMAPYLENGTH = 36;
    //[SerializeField]
    public Terrain[,] TerrainMap = new Terrain[MAXMAPXLENGTH, MAXMAPYLENGTH];
    [SerializeField]public List<Iblown> BlownList = new List<Iblown>();
    [SerializeField]public List<Iblow>BlowList = new List<Iblow>();
    public Direction[,] WindMap = new Direction[MAXMAPXLENGTH, MAXMAPYLENGTH];
    public bool CanChangeWind;
    public WorldWind worldWind;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Initialize();
        GameLoop();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Initialize()
    {
        CanChangeWind = true;
        worldWind = new WorldWind();
        
    }
    void GameLoop()
    {
        Initialize();
        while (true)
        {
            GetKey();
            Blow();
            Move();
        }
    }
    void Move()
    {
        bool isDone = false;
        while (true)
        {
            isDone = true;
            foreach (var eachObject in BlownList)
            {
                isDone = isDone && eachObject.move();
            }
            if (isDone) break;
        }
        CanChangeWind = true;
    }
    void EndGame()
    {

    }
    void GetKey()
    {
        Direction direction = worldWind.GetDirection();
        if (!CanChangeWind) return;
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = Direction.UP;
            CanChangeWind = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Direction.LEFT;
            CanChangeWind = false;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = Direction.DOWN;
            CanChangeWind = false;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = Direction.RIGHT;
            CanChangeWind = false;
        }
        worldWind.SetDirection(direction);
    }
    void Blow()
    {
        foreach(var eachObject in BlowList)
        {
            eachObject.Blow(WindMap);
        }
    }
    void InitializeBlower() //blower的優先度排序初始化 由低排到高
    {
        BlowList.Sort((a, b) => a.GetPriority().CompareTo(b.GetPriority()));
    }
}
