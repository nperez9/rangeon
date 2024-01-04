using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField seedInput;

    private void Start()
    {
        seedInput.text = PlayerPrefs.GetInt("Seed").ToString();
    }

    public void OnUpdateSeed()
    {
        PlayerPrefs.SetInt("Seed", int.Parse(seedInput.text));
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
