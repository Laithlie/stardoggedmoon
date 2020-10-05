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
        this.image = this.GetComponent<Image>();
        if (this.image == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }
        this.targetAlpha = this.image.color.a;
        loaderSaver = GameObject.Find("GameController").GetComponent<LoaderSaver>();
        Debug.Log(loaderSaver);
        FadeOut(100f);
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = this.image.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.image.color = curColor;
        }
    }

    public void FadeOut(float fadeRate)
    {
        FadeRate = fadeRate;
        this.targetAlpha = 0.0f;
    }

    public void FadeIn(float fadeRate)
    {
        loaderSaver.RegisterVisibleObject(gameObject);
        FadeRate = fadeRate;
        this.targetAlpha = 1.0f;
    }
}
