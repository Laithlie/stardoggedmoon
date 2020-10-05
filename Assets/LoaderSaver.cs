using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class LoaderSaver : MonoBehaviour
{
    [HideInInspector]
    public bool hasSaved = false;

    List<string> visibleObjects = new List<string>();
    // Start is called before the first frame update
    void Awake(){
        hasSaved = PlayerPrefs.HasKey("inkSaveState");
    }

    public void SetIsLoading(bool isLoading){
        PlayerPrefs.SetInt("isLoading", isLoading ? 1 : 0);
    }

    public bool IsLoading(){
        if (PlayerPrefs.HasKey("isLoading"))
        {
            return PlayerPrefs.GetInt("isLoading") == 1;
        }
        return false;
    }

    public void Save(StoryState state){
        string savedState = state.ToJson();
        PlayerPrefs.SetString("inkSaveState", savedState);
        PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);

    }

    public void LoadLastScene(){
        if (PlayerPrefs.HasKey("lastScene")){
            SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
            SetIsLoading(true);
        }
    }

    public string LoadInkState(){
        if (PlayerPrefs.HasKey("inkSaveState")){
            return PlayerPrefs.GetString("inkSaveState");
        }
        return null;
    }

    public void RegisterVisibleObject(GameObject visibleObject){
        visibleObjects.Add(visibleObject.name);
    }

    public void ResetVisibleObjects(){
        visibleObjects = new List<string>();
    }
}
