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
   public HashSet<string> visibleObjects;
  

   public SaveData(StoryState state, HashSet<string> objects){
       savedState = state.ToJson();
       visibleObjects = objects;
   }
}

public class LoaderSaver : MonoBehaviour
{
    [HideInInspector]
    public bool hasSaved = false;

    HashSet<string> visibleObjects = new HashSet<string>();
    string fileName; 
    // Start is called before the first frame update
    void Awake(){
        fileName = Application.persistentDataPath + "/gamesave.save";
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
        FileStream file;
        if (File.Exists(fileName)){
            file = File.OpenWrite(fileName);
        }
        else{
            file = File.Create(fileName);
        }
         
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
        FileStream file = File.Open(fileName, FileMode.Open);
        SaveData save = (SaveData) binaryFormatter.Deserialize(file);
        return save;
    }

    public void RegisterVisibleObject(GameObject visibleObject){
        visibleObjects.Add(visibleObject.name);
    }

    public void ClearVisibleObject(GameObject visibleObject){
        if (visibleObjects.Contains(visibleObject.name)){
            visibleObjects.Remove(visibleObject.name);
        }
        
    }

    public void ResetVisibleObjects(){
        visibleObjects = new HashSet<string>();
    }

    private bool IsFileLocked()
{
    FileInfo file = new FileInfo(Application.persistentDataPath + "/gamesave.save");
    FileStream stream = null;

    try
    {
        stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
    }
    catch (IOException)
    {
        //the file is unavailable because it is:
        //still being written to
        //or being processed by another thread
        //or does not exist (has already been processed)
        return true;
    }
    finally
    {
        if (stream != null)
            stream.Close();
    }

    //file is not locked
    return false;
}
}
