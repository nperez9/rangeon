using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviour
{
    [Header("Door Objects")]
    [SerializeField] public Transform nortDoor;
    [SerializeField] public Transform southDoor;
    [SerializeField] public Transform eastDoor;
    [SerializeField] public Transform westDoor;

    [Header("Walls")]
    [SerializeField] public Transform nortWall;
    [SerializeField] public Transform southWall;
    [SerializeField] public Transform eastWall;
    [SerializeField] public Transform westWall;

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
