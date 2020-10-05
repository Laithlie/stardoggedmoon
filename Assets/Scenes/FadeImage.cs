using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public float FadeRate;
    
    private Image image;
    private float targetAlpha;
    LoaderSaver loaderSaver;
    // Use this for initialization
    void Awake()
    {
        loaderSaver = GameObject.Find("GameController").GetComponent<LoaderSaver>();
        this.image = this.GetComponent<Image>();
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }
        this.targetAlpha = this.image.color.a;
        
        FadeOut(100f);
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            //Debug.Log("At " +  gameObject.name + " lerpin from " + curColor.a + " to " + targetAlpha);
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
        }
    }

    public void FadeOut(float fadeRate)
    {
        loaderSaver.ClearVisibleObject(gameObject);
        FadeRate = fadeRate;
        this.targetAlpha = 0.0f;
    }

    public void FadeIn(float fadeRate)
    {
        
        loaderSaver.RegisterVisibleObject(gameObject);
        FadeRate = fadeRate;
        this.targetAlpha = 1.0f;
        //Debug.Log("Fading in " +  gameObject.name + " with curColour " + image.color.a + " and targetAlpha " + this.targetAlpha);
    }
}
