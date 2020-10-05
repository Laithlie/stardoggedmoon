using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Command{
    ARRIVED, //fades in
    LEFT,   // fades out
    TRAVERSED_TO, // changes scene,
    FADE_TO_BLACK // fades out everything

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

    void Start(){
        allObjects = FindObjectsOfType(typeof(FadeImage)) as FadeImage[];
    }


    Command GetCommand(string text){
        Command newCommand;
        if (!Command.TryParse(text, true, out newCommand)){
            throw new System.Exception("Invalid command! Your command " + text + " has not got any code attached to it");
        }
        return newCommand;
    }
    void ParseCurrentTag(string tag){
        string[] unparsedText = tag.Split(' ');
        if (unparsedText.Length > 2){
            throw new System.Exception("Invalid tag! Your tag " + tag + " has more than two separate words. this is currently illegal");
        }
        Command newCommand;
        string actorName;
        if (unparsedText.Length == 1){
            newCommand = GetCommand(unparsedText[0]);
            actorName = "";
        }
        else{
            newCommand = GetCommand(unparsedText[1]);
            actorName = unparsedText[0];
        }


        ParseCommand(newCommand, actorName);

    } 

    public void ParseAllTags(List<string> currentTags){
        foreach (string tag in currentTags){
            ParseCurrentTag(tag);
        }

    }


    void ParseCommand(Command command, string actorName) {
        GameObject actor = GameObject.Find(actorName);
        switch (command){
            case(Command.ARRIVED):
                ToggleVisibility(actor, true);
                break;
            case (Command.LEFT):
                ToggleVisibility(actor, false);
                break;
            case (Command.TRAVERSED_TO):
                SceneManager.LoadScene(actorName);
                break;
            case (Command.FADE_TO_BLACK):
            Debug.Log("HI IM FADING");
                FadeAllOut();
                break;


        }

    }

    void ToggleVisibility(GameObject thing, bool weWantYouVisibleToday){
        FadeImage fader = thing.GetComponent<FadeImage>();
        if (fader == null){
            throw new System.Exception("Queried gameobject " + thing + " does not have a fade script attached to it! please fix this");
        }
        if (weWantYouVisibleToday){
            fader.FadeIn(fadeRate);
        }
        else{
            fader.FadeOut(fadeRate);
        }
    }

    void FadeAllOut(){
        foreach (FadeImage obj in allObjects){
            obj.FadeOut(fadeToBlackRate);
        }
    }



}

