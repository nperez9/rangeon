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
    private bool roomsInstantiated;

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
        Random.InitState(9);
        Generate();
    }

    public void Generate()
    {
        map = new bool[mapWidth, mapHeight];
        CheckRoom(3, 3, 0, Vector2.zero, true);
        InstanciateRooms();
        // translate to global pos
        FindObjectOfType<PlayerController>().transform.position = firstRoomPos * 12;
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

        // it´s reversed, so, if it is 0.2 its 80%
        bool nort = Random.value > (generalDirection == Vector2.up ? 0.2f : 0.8f);
        bool south = Random.value > (generalDirection == Vector2.down ? 0.2f : 0.8f);
        bool east = Random.value > (generalDirection == Vector2.right ? 0.2f : 0.8f);
        bool west = Random.value > (generalDirection == Vector2.left ? 0.2f : 0.8f);

        // calculate the 4 posibble directions
        int maxRemaining = roomsToGenerate / 4;

        if (nort || firstRoom) {
            CheckRoom(x, y + 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.up : generalDirection);  
        }
        if (south || firstRoom)
        {
            CheckRoom(x, y - 1, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.down : generalDirection);
        }
        if (east || firstRoom)
        {
            CheckRoom(x + 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.right : generalDirection);
        }
        if (west || firstRoom)
        {
            CheckRoom(x - 1, y, firstRoom ? maxRemaining : remaining - 1, firstRoom ? Vector2.left : generalDirection);
        }

    }

    private void InstanciateRooms()
    {
        if (roomsInstantiated)
            return;
        roomsInstantiated = true;

        for (int x = 0; x < mapWidth; ++x)
        {
            for (int y = 0; y < mapHeight; ++y)
            {
                if (map[x, y] == false)
                    continue;

                GameObject roomObj = Instantiate(roomPrefab, new Vector3(x, y, 0) * 12, Quaternion.identity);
                // get a reference to the Room script of the new room object
                RoomsManager room = roomObj.GetComponent<RoomsManager>();
                
                // if we're within the boundary of the map, AND if there is room above us
                if (y < mapHeight - 1 && map[x, y + 1] == true)
                {
                    // enable the north door and disable the north wall.
                    room.nortDoor.gameObject.SetActive(true);
                    room.nortWall.gameObject.SetActive(false);
                } else
                {
                    room.nortDoor.gameObject.SetActive(false);
                    room.nortWall.gameObject.SetActive(true);
                }

                // South
                if (y > 0 && map[x, y - 1] == true)
                {
                    room.southDoor.gameObject.SetActive(true);
                    room.southWall.gameObject.SetActive(false);
                }

                // West 
                if (x > 0 && map[x -1, y] == true)
                {
                    room.westDoor.gameObject.SetActive(true);
                    room.westWall.gameObject.SetActive(false);
                }

                // East
                if (x < mapWidth - 1  && map[x + 1, y] == true)
                {
                    room.eastDoor.gameObject.SetActive(true);
                    room.eastWall.gameObject.SetActive(false);
                }

                // if this is not the first room, call GenerateInterior().
                if (firstRoomPos != new Vector2(x, y))
                    room.GenerateInterior();
                // add the room to the roomObjects list 
                roomObjects.Add(room);
            }
        }

        CalculateKeyAndExit();
    }

    private void CalculateKeyAndExit()
    {

    }
}
