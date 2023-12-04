using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [Header("Door Objects")]
    [SerializeField] private Transform nortDoor;
    [SerializeField] private Transform southDoor;
    [SerializeField] private Transform eastDoor;
    [SerializeField] private Transform westDoor;

    [Header("Walls")]
    [SerializeField] private Transform nortWall;
    [SerializeField] private Transform southWall;
    [SerializeField] private Transform eastWall;
    [SerializeField] private Transform westWall;

    [Header("Values")]
    public int insideWidth;
    public int insideHeight;

    [Header("Prebas & Elements")]
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public GameObject keyPrefab;
    public GameObject exitDoorPrefab;

    private List<Vector3> usedPositions = new List<Vector3>();

    public void GenerateInterior()
    {

    }

    public void SpawnPrefab(GameObject prefab, int min = 0, int max = 0)
    {

    }
}
