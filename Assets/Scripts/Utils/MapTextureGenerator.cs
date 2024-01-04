using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is responsible for generating a texture that represents the map.
 * 
 */
public class MapTextureGenerator : MonoBehaviour
{
    /**
     * This method generates a texture that represents the map.
     * 
     * @param map The map to generate a texture for.
     * @param playerRoom The room the player is currently in.
     * @return The generated texture.
     */
    public static Texture2D Generate(bool[,] map, Vector2 playerRoom)
    {
        Texture2D texture = new Texture2D(map.GetLength(0), map.GetLength(1));
        texture.filterMode = FilterMode.Point;

        Color[] pixels = new Color[map.GetLength(0) * map.GetLength(1)];

        for (int i = 0; i < pixels.Length; i++)
        {
            int x = i % map.GetLength(0);
            int y = Mathf.FloorToInt(i / map.GetLength(1));

            if (playerRoom == new Vector2(x, y))
            {
                pixels[i] = Color.green;
            }
            else
            {
                pixels[i] = map[x, y] == true ? Color.white : Color.clear;
            }

            texture.SetPixels(pixels);
            texture.Apply();
        }

        return texture;
    }
}
