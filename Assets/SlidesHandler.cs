using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SlidesHandler : MonoBehaviour
{
    public RawImage rawImage;
    public string basePath;
    public string[] slideNames;

    private int currentSlideIndex = 0;


    // Start is called before the first frame update
    private void Start()
    {
        currentSlideIndex = 0;
        StartCoroutine("FetchNextSlide");
    }


    IEnumerator FetchNextSlide()
    {
        Texture2D tex;
        tex = new Texture2D(4, 4, TextureFormat.DXT1, false);

        WWW www = new WWW(basePath + "/" + slideNames[currentSlideIndex]);

        while (!www.isDone)
            yield return null;


        www.LoadImageIntoTexture(tex);
        rawImage.texture = tex;
    }

    internal void changeSlideTo(int direction)
    {
        if (direction == 0)
        {
            // Reset to 0th slide
            currentSlideIndex = 0;
            StartCoroutine("FetchNextSlide");
            return;
        }

        // Find next or prev slide
        currentSlideIndex += direction;
        if (currentSlideIndex < 0)
        {
            // Went beyond 0 while moving to previous. Snap back to 0
            currentSlideIndex = 0;
        }
        else if (currentSlideIndex >= slideNames.Length - 1)
        {
            currentSlideIndex = slideNames.Length - 1;
        }


        StartCoroutine("FetchNextSlide");
    }
}
