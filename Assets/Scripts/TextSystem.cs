using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSystem : MonoBehaviour
{
    float backShowsSpeed = 0.3f;
    [HideInInspector]
    
    float textShowsSpeed = 0.04f;

    //public bool IEgoingDone = true;

    public bool fadeInLengthDone = true;
    public bool fadeInAlphaDone = true;

    public bool fadeOutLengthDone = true;
    public bool fadeOutAlphaDone = true;

    public bool changeLengthDone = true;

    public void initLength(RectTransform img)
    {
        float length = 0;
        img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
    }

    
    public IEnumerator fadeInLength(RectTransform img, float L)
    {
        fadeInLengthDone = false;
        yield return null;
        float length = 0;
        while (Mathf.Abs(length - L) > 0.1f)
        {
            length = Mathf.Lerp(length, L, backShowsSpeed);
            img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
            yield return null;
        }
        length = L;
        img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
        fadeInLengthDone = true;
        yield return null;

    }

    public IEnumerator fadeOutLength(RectTransform img, float L)
    {
        fadeOutLengthDone = false;
        yield return null;
        float length = L;
        while (Mathf.Abs(length) > 0.1f)
        {
            length = Mathf.Lerp(length, 0, backShowsSpeed);
            img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
            yield return null;
        }
        length = 0;
        img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
        fadeOutLengthDone = true;
        yield return null;

    }

    public void setLength(RectTransform img, float L)
    {
        img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, L);
    }
    /*
    public IEnumerator changeLength(RectTransform img, float L1, float L2)
    {
        changeLengthDone = false;
        yield return null;
        float length1 = L1;
        float length2 = L2;
        while (Mathf.Abs(length1 - length2) > 1f)
        {
            L1 = Mathf.Lerp(L1, L2, backShowsSpeed);
            img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, L1);
            yield return null;
        }
        L1 = L2;
        img.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, L1);
        changeLengthDone = true;
        yield return null;


    }
    */

    ///

    public void initAlpha(Image img)
    {
        Color color = img.color;
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float a = 0;
        img.color = new Color(r, g, b, a);

    }

    public void SetSprite(Image img,Sprite sprite)
    {
        img.sprite = sprite;

    }

    public IEnumerator fadeInAlpha(Image img)
    {
        fadeInAlphaDone = false;
        yield return null;
        Color color = img.color;
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float a = 1;
        float alpha = 0;
        while (alpha < a)
        {
            alpha += textShowsSpeed;
            img.color = new Color(r, g, b, alpha);
            yield return null;

        }
        alpha = 1;
        img.color = new Color(r, g, b, alpha);
        fadeInAlphaDone = true;
        yield return null;

    }

    public IEnumerator fadeOutAlpha(Image img)
    {
        fadeOutAlphaDone = false;
        yield return null;
        //img.gameObject.SetActive(false);
        Color color = img.color;
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float a = 0;
        float alpha = 1;
        while (alpha > a)
        {
            alpha -= textShowsSpeed;
            img.color = new Color(r, g, b, alpha);
            yield return null;

        }
        alpha = 0;
        img.color = new Color(r, g, b, alpha);
        fadeOutAlphaDone = true;
        yield return null;

    }
}
