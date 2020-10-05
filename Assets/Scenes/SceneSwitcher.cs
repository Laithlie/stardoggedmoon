using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    LoaderSaver loaderSaver;
    void Awake(){
        loaderSaver = GetComponent<LoaderSaver>();
    }

    public void GotoDay1Scene()
    {
        loaderSaver.SetIsLoading(false);
        SceneManager.LoadScene("Day1");
    }

    public void GotoMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GotoCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GotoDay2Scene()
    {
        SceneManager.LoadScene("Day2");
    }
    public void GotoDay3Scene()
    {
        SceneManager.LoadScene("Day3");
    }
    public void GotoDay4Scene()
    {
        SceneManager.LoadScene("Day4");
    }
}