using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int mapWidth = 7;
    public int mapHeight = 7;
    public int roomsToGenerate = 12;
    public GameObject roomPrefab;

    private int roomCount;
    private int roomsInstantiated;

    private Vector2 firstRoomPos;
    private bool[,] map;
    private List<RoomsManager> roomObjects = new List<RoomsManager>();

    public static Generator instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Random.InitState(392910);
        Generate();
    }

    public void Generate()
    {
        map = new bool[mapWidth, mapHeight];
        CheckRoom(3, 3, 0, Vector2.zero, true);
    }

    private void CheckRoom(int x, int y, int remaining, Vector2 generalDirection, bool firstRoom = false)
    {
        // Check if all rooms are generated
        if (roomCount >= roomsToGenerate)
        {
            return;
        }

        // check if is bettween the defined limits of the map (7 by 7 by default)
        if (x < 0 || x > mapWidth - 1 || y < 0 || y > mapHeight - 1)
        {
            return;
        }

        // if the room that we create is already generated
        if (map[x, y] == true)
        {
            return;
        }

        // Sets the init of the map
        if (firstRoom == true)
        {
            firstRoomPos = new Vector2(x, y);
        }

        roomCount++;
        map[x, y] = true;
    }

    private void InstanciateRooms()
    {

    }

    private void CalculateKeyAndExit()
    {

    }
}
