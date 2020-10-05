using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HackToHideContinueButtonIIfNotRelevant : MonoBehaviour
{
    LoaderSaver loaderSaver;
    // Start is called before the first frame update
    void Start()
    {
        loaderSaver = GameObject.Find("GameController").GetComponent<LoaderSaver>();
        if (!loaderSaver.hasSaved){
            GetComponent<Button>().enabled = false;
            transform.GetChild(0).GetComponent<TMP_Text>().color = Color.gray;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
