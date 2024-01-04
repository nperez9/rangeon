using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int mapWidth = 7;
    public int mapHeight = 7;
    public int roomsToGenerate = 12;
    public GameObject roomPrefab;

    [Header("Spawn Chances")]
    public float enemySpawnChance = 0.7f;
    public float coinSpawnChance = 0.8f;
    public float healthSpawnChance = 0.3f;

    [Header("Max Spawn per Room")]
    public int maxEnemiesPerRoom = 2;
    public int maxCoinsPerRoom = 4;
    public int maxHealthPerRoom = 1;
    public int maxDetailsPerRoom = 3;

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
        int seed = PlayerPrefs.GetInt("Seed");
        seed = seed != 0 ? seed : 9;
        Random.InitState(seed);
        Generate();
    }

    public void OnPlayerMove()
    {
        // get the position of the player
        Vector2 playerPos = FindObjectOfType<PlayerController>().transform.position;
        // get the position of the room that the player is in, in terms of map scale.
        Vector2 roomPos = new Vector2(((int)playerPos.x + 6) / 12, ((int)playerPos.y + 6) / 12);
        // generate a newer version of the map
        UI.instance.map.texture = MapTextureGenerator.Generate(map, roomPos);
    }

    public void Generate()
    {
        map = new bool[mapWidth, mapHeight];
        CheckRoom(3, 3, 0, Vector2.zero, true);
        InstanciateRooms();
        // translate to global pos
        FindObjectOfType<PlayerController>().transform.position = firstRoomPos * 12;
        UI.instance.UpdateMiniMap(MapTextureGenerator.Generate(map, firstRoomPos));
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

        // its reversed, so, if it is 0.2 its 80%
        bool nort = Random.value > (generalDirection == Vector2.up ? 0.2f : 0.8f);
        bool south = Random.value > (generalDirection == Vector2.down ? 0.2f : 0.8f);
        bool east = Random.value > (generalDirection == Vector2.right ? 0.2f : 0.8f);
        bool west = Random.value > (generalDirection == Vector2.left ? 0.2f : 0.8f);

        // calculate the 4 posibble directions
        int maxRemaining = roomsToGenerate / 4;

        if (nort || firstRoom)
        {
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
                }
                else
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
                if (x > 0 && map[x - 1, y] == true)
                {
                    room.westDoor.gameObject.SetActive(true);
                    room.westWall.gameObject.SetActive(false);
                }

                // East
                if (x < mapWidth - 1 && map[x + 1, y] == true)
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

    /** 
     *  this method will calculate the room that is the furthest away from all other rooms, 
     *  and spawn the key and exit door in those rooms.
     */
    private void CalculateKeyAndExit()
    {
        float maxDist = 0;

        RoomsManager keyRoom = null;
        RoomsManager exitDoorRoom = null;

        foreach (RoomsManager aRoom in roomObjects)
        {
            foreach (RoomsManager bRoom in roomObjects)
            {
                if (aRoom == bRoom)
                    continue;

                float dist = Vector2.Distance(aRoom.transform.position, bRoom.transform.position);

                if (dist > maxDist)
                {
                    maxDist = dist;
                    keyRoom = aRoom;
                    exitDoorRoom = bRoom;
                }
            }
        }

        keyRoom.SpawnOnePrefab(keyRoom.keyPrefab);
        exitDoorRoom.SpawnOnePrefab(exitDoorRoom.exitDoorPrefab);
    }
}
