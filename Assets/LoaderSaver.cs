using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[System.Serializable]
public class SaveData{
   public string savedState;
   public List<string> visibleObjects;

   public SaveData(StoryState state, List<string> objects){
       savedState = state.ToJson();
       visibleObjects = objects;
   }
}

public class LoaderSaver : MonoBehaviour
{
    [HideInInspector]
    public bool hasSaved = false;

    List<string> visibleObjects = new List<string>();
    // Start is called before the first frame update
    void Awake(){
        hasSaved = PlayerPrefs.HasKey("lastScene");
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
        SaveData toSave = new SaveData(state, visibleObjects);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        binaryFormatter.Serialize(file, toSave);
        file.Close();
        
        PlayerPrefs.SetString("lastScene", SceneManager.GetActiveScene().name);

    }

    

    public void LoadLastScene(){
        if (PlayerPrefs.HasKey("lastScene")){
            SceneManager.LoadScene(PlayerPrefs.GetString("lastScene"));
            SetIsLoading(true);
        }
    }

    public SaveData LoadAllData(){
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
        SaveData save = (SaveData) binaryFormatter.Deserialize(file);
        return save;
    }

    public void RegisterVisibleObject(GameObject visibleObject){
        visibleObjects.Add(visibleObject.name);
    }

    public void ResetVisibleObjects(){
        visibleObjects = new List<string>();
    }
}
