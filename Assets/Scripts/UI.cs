using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject[] hearts;
    public TextMeshProUGUI coinsText;
    public GameObject keyIcon;
    public TextMeshProUGUI levelText;
    public RawImage map;

    public static UI instance;

    void Awake()
    {
        instance = this;
    }

    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].SetActive(true);
            }
            else
            {
                hearts[i].SetActive(false);
            }
        }
    }

    public void UpdateCoins(int coins)
    {
        coinsText.text = coins.ToString();
    }

    public void ToggleKeyIcon(bool active)
    {
        keyIcon.SetActive(active);
    }

    public void UpdateLevelText(int level)
    {
        levelText.text = "Level " + level.ToString();
    }

    public void UpdateMiniMap(Texture2D map)
    {
        this.map.texture = map;
    }
}
