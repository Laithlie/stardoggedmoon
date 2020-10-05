using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum CommandType {
    ON_ONE,
    ON_ALL
}
public enum Command{
    ARRIVED, //fades in
    LEFT,   // fades out
    TRAVERSED_TO, // changes scene,
    FADE_TO_BLACK, // fades out everything
    FADE_TO_WHITE,
    NOT_A_VALID_COMMAND // fades also

}

public class ThingToDo{
    
    GameObject actor;
    Command command;
}

public class TagParser : MonoBehaviour
{
    public float fadeRate;

    public float fadeToBlackRate;
    List<string> currentTags;
    FadeImage[] allObjects;
    Camera mainCamera;
    CanvasGroup inkCanvas;


    void Awake(){
        inkCanvas = GameObject.Find("InkCanvas").GetComponent<CanvasGroup>();
    }
    void Start(){
        allObjects = FindObjectsOfType(typeof(FadeImage)) as FadeImage[];
        mainCamera = Camera.main;
        
    }

    void ToggleCanvasVisibility(bool isVisible){
        if (isVisible){
            inkCanvas.alpha = 1;
            inkCanvas.interactable = true;
        }
        else{
            inkCanvas.alpha = 0;
            inkCanvas.interactable = false;
        }
    }


    Command GetCommand(string text){
        Command newCommand;
        if (!Command.TryParse(text, true, out newCommand)){
            return Command.NOT_A_VALID_COMMAND;
        }
        return newCommand;
    }

    float StringToFloat(string text){
        float param;
        if (float.TryParse(text, out param)){
                return param;
            }
            else {
                throw new System.Exception("Parameter " + text + " cannot be parsed as float. Either leave parameter blank or enter a valid number");
            }
    }
    float GetParam(string[] unparsedText, CommandType commandType){
        if (commandType == CommandType.ON_ALL && unparsedText.Length == 2){
            return StringToFloat(unparsedText[1]);
        }
        else if (commandType == CommandType.ON_ONE && unparsedText.Length == 3){
            return StringToFloat(unparsedText[2]);
        }
        else {
            return -1;
        }
    }

    void ParseCurrentTag(string tag){
        string[] unparsedText = tag.Split(' ');
        Command newCommand;
        string actorName;
        float param;

        if (unparsedText[0] == "INCLUDE"){
            Debug.Log("sup");
            return;
        }

        newCommand = GetCommand(unparsedText[0]);
        if (newCommand != Command.NOT_A_VALID_COMMAND){
            param = GetParam(unparsedText, CommandType.ON_ALL);
            actorName = "";
        }
        else{
            newCommand = GetCommand(unparsedText[1]);
            if (newCommand == Command.NOT_A_VALID_COMMAND){
                throw new System.Exception("Command " + unparsedText[1] + " is not a valid command. Enter a valid one pls ");
            }
            actorName = unparsedText[0];
            param = GetParam(unparsedText, CommandType.ON_ONE);
        }


        ParseCommand(newCommand, actorName, param);

    } 

    public void ParseAllTags(List<string> currentTags){
        foreach (string tag in currentTags){
            ParseCurrentTag(tag);
        }

    }


    void ParseCommand(Command command, string actorName, float param) {
        GameObject actor = GameObject.Find(actorName);
        switch (command){
            case (Command.ARRIVED):
                ToggleCanvasVisibility(true);
                ToggleVisibility(actor, true, param);
                break;
            case (Command.LEFT):
                ToggleVisibility(actor, false, param);
                break;
            case (Command.TRAVERSED_TO):
                SceneManager.LoadScene(actorName);
                break;
            case (Command.FADE_TO_BLACK):
                ToggleCanvasVisibility(false);
                ChangeBackGroundColour(Color.black);
                FadeAllOut(param);
                break;
            case (Command.FADE_TO_WHITE):
                ToggleCanvasVisibility(false);
                ChangeBackGroundColour(Color.white);
                FadeAllOut(param);
                break;


        }

    }

    void ToggleVisibility(GameObject thing, bool weWantYouVisibleToday, float rate){
        if (rate == -1){
            rate = fadeRate;
        }
        FadeImage fader = thing.GetComponent<FadeImage>();
        if (fader == null){
            throw new System.Exception("Queried gameobject " + thing + " does not have a fade script attached to it! please fix this");
        }
        if (weWantYouVisibleToday){
            fader.FadeIn(rate);
        }
        else{
            fader.FadeOut(rate);
        }
    }


    void ChangeBackGroundColour(Color color){
        mainCamera.backgroundColor = color;
    }
    void FadeAllOut(float rate){
        if (rate == -1){
            rate = fadeToBlackRate;
        }
        foreach (FadeImage obj in allObjects){
            obj.FadeOut(rate);
        }
    }



}

