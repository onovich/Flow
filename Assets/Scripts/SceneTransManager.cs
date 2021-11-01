using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransManager : MonoBehaviour
{
    public Image black;

    public bool ifSceneFadein = false;

    public void goesto(string nextMapName)
    {
        StopAllCoroutines();
        StartCoroutine(beBlack(nextMapName));
    }

    
    IEnumerator beBlack(string nextMapName)
    {

        Color color = black.color;
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float a = 1;
        float alpha = 0;
        while (alpha < a)
        {
            alpha += 0.04f;
            black.color = new Color(r, g, b, alpha);
            yield return null;
            yield return null;
            yield return null;
            yield return null;


        }
        alpha = a;
        black.color = new Color(r, g, b, alpha);
        yield return null;
        AsyncOperation op = SceneManager.LoadSceneAsync(nextMapName);


    }


    private void Start()
    {
        black.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(beUnBlack());
    }

    
    IEnumerator beUnBlack()
    {
        Color color = black.color;
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float a = 0;
        float alpha = 1;
        while (alpha >a)
        {
            alpha -= 0.04f;
            black.color = new Color(r, g, b, alpha);
            yield return null;
            yield return null;
            yield return null;
            yield return null;

        }
        alpha = a;
        black.color = new Color(r, g, b, alpha);
        Debug.Log("画面淡入完成");
        ifSceneFadein = true;
        yield return null;
    }
    
}
