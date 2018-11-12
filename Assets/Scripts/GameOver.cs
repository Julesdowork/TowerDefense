﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

    public Text roundsText;
    public SceneFader fader;
    public string mainMenuSceneName = "MainMenu";

    void OnEnable()
    {
        roundsText.text = PlayerStats.Rounds.ToString();
    }

    public void Retry()
    {
        fader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        fader.FadeTo(mainMenuSceneName);
    }
}
