using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour {

    public SceneFader fader;
    public string menuSceneName = "Main Menu";
    public string nextLevel = "Level 2";
    public int levelToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", levelToUnlock);
        fader.FadeTo(nextLevel);
    }

	public void Menu()
    {
        fader.FadeTo(menuSceneName);
    }
}
