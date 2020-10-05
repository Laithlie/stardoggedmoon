using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Command{
    ARRIVED, //fades in
    LEFT,   // fades out
    TRAVERSED_TO // changes scene

}

public class ThingToDo{
    
    GameObject actor;
    Command command;
}

public class TagParser : MonoBehaviour
{

    
    List<string> currentTags;
    void ParseCurrentTag(string tag){
        string[] unparsedText = tag.Split(' ');
        if (unparsedText.Length > 2){
            throw new System.Exception("Invalid tag! Your tag " + tag + " has more than two separate words. this is currently illegal");
        }
        Command newCommand;
        if (!Command.TryParse(unparsedText[1], true, out newCommand)){
            throw new System.Exception("Invalid command! Your command " + unparsedText[1] + " has not got any code attached to it");
        }

        ParseCommand(newCommand, unparsedText[0]);

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
        }

    }

    void ToggleVisibility(GameObject thing, bool weWantYouVisibleToday){
        FadeImage fader = thing.GetComponent<FadeImage>();
        if (fader == null){
            throw new System.Exception("Queried gameobject " + thing + " does not have a fade script attached to it! please fix this");
        }
        if (weWantYouVisibleToday){
            fader.FadeIn();
        }
        else{
            fader.FadeOut();
        }
    }



}

