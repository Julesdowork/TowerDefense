using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static bool gameEnded;

    public GameObject gameOverUI;
    public string nextLevel = "Level 2";
    public int levelToUnlock = 2;
    public SceneFader fader;

    void Start()
    {
        gameEnded = false;
    }

    // Update is called once per frame
    void Update () {
        if (gameEnded)
            return;

		if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
	}

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        Debug.Log("LEVEL WON!!!");
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        fader.FadeTo(nextLevel);
    }
}
