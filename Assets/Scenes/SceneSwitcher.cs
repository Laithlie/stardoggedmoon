using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void GotoDay1Scene()
    {
        SceneManager.LoadScene("Day 1");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void GotoCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GotoDay2Scene()
    {
        SceneManager.LoadScene("Day 2");
    }
    public void GotoDay3Scene()
    {
        SceneManager.LoadScene("Day 3");
    }
    public void GotoDay4Scene()
    {
        SceneManager.LoadScene("Day 4");
    }
}