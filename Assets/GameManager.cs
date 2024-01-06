using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;
    public int baseSeed;

    private int prevRoomPlayerHealth;
    private int prevRoomPlayerCoins;

    private PlayerController player;

    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        level = 1;
        baseSeed = PlayerPrefs.GetInt("Seed");
        baseSeed = baseSeed != 0 ? baseSeed : 9;
        Random.InitState(baseSeed);
        Generator.instance.Generate();
        UI.instance.UpdateLevelText(level);

        player = FindObjectOfType<PlayerController>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if this is the main menu scene, destroy this gameObject to prevent a duplicate
        if (scene.name != "Game")
        {
            Destroy(gameObject);
            return;
        }
        player = FindObjectOfType<PlayerController>();
        level++;
        baseSeed++;
        // generate a new level with the updated baseSeed
        Generator.instance.Generate();
        // transfer the previous game data to the new level
        player.currentHP = prevRoomPlayerHealth;
        player.coins = prevRoomPlayerCoins;
        UI.instance.UpdateHearts(prevRoomPlayerHealth);
        UI.instance.UpdateCoins(prevRoomPlayerCoins);
        UI.instance.UpdateLevelText(level);
    }

    public void GoToNextLevel()
    {
        prevRoomPlayerHealth = player.currentHP;
        prevRoomPlayerCoins = player.coins;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
