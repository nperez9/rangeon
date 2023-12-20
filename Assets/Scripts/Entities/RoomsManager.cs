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

    [Header("Prefabs & Elements")]
    public GameObject enemyPrefab;
    public GameObject coinPrefab;
    public GameObject healthPrefab;
    public GameObject keyPrefab;
    public GameObject exitDoorPrefab;

    [Header("Details")]
    public List<Sprite> detailSprites = new List<Sprite>();
    public List<Color> colorList = new List<Color>();

    private List<Vector3> usedPositions = new List<Vector3>();

    public void GenerateInterior()
    {
        PaintItBlack();

        if (Random.value < Generator.instance.enemySpawnChance)
        {
            SpawnPrefab(enemyPrefab, 1, Generator.instance.maxEnemiesPerRoom + 1);
        }

        if (Random.value < Generator.instance.coinSpawnChance)
        {
            SpawnPrefab(coinPrefab, 1, Generator.instance.maxCoinsPerRoom + 1);
        }

        if (Random.value < Generator.instance.healthSpawnChance)
        {
            SpawnPrefab(healthPrefab, 1, Generator.instance.maxHealthPerRoom + 1);
        }

        int detailCount = Random.Range(0, Generator.instance.maxDetailsPerRoom + 1);
        for (int i = 0; i < detailCount; i++)
        {
            SpawnDetailSprite();
        }
    }

    public void SpawnPrefab(GameObject prefab, int min = 0, int max = 0)
    {
        int spawnCount = Random.Range(min, max);
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPos = GetRandomPositionInsideRoom();
            Instantiate(prefab, spawnPos, Quaternion.identity, transform);
        }
    }

    /**
     * This method will paint the room with random colors.
     */
    private void PaintItBlack()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        int selectedColor = Random.Range(0, colorList.Count);

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.color = colorList[selectedColor];
        }
    }

    private Vector3 GetRandomPositionInsideRoom()
    {
        int x;
        int y;
        Vector3 newPos;

        do
        {
            x = Random.Range(-(insideWidth / 2), insideWidth / 2);
            y = Random.Range(-(insideHeight / 2), insideHeight / 2);
            newPos = new Vector3(x, y, 0) + transform.position;
        } while (usedPositions.Contains(newPos));

        usedPositions.Add(newPos);
        return newPos;
    }

    private void SpawnDetailSprite()
    {
        // Get a random position inside the room
        Vector3 spawnPos = GetRandomPositionInsideRoom();

        // Get a random detail sprite
        int spriteIndex = Random.Range(0, detailSprites.Count);
        Sprite detailSprite = detailSprites[spriteIndex];

        // Create a new GameObject to hold the sprite
        GameObject spriteObject = new GameObject("DetailSprite");
        spriteObject.transform.position = spawnPos;
        spriteObject.transform.parent = transform;

        // Add a SpriteRenderer and set the sprite
        SpriteRenderer spriteRenderer = spriteObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = detailSprite;

        // Get a random color
        int colorIndex = Random.Range(0, colorList.Count);
        spriteRenderer.color = colorList[colorIndex];
    }
}
