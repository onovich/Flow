using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class textSprite
{
    //public int mapID;
    //public int id;
    public Sprite sprite;
    public bool ifWaitForPress;
    public KeyCode pressKeyA;
    public KeyCode pressKeyB;
    public bool ifStartsWhenAwake;
    public float length;
    public bool ifAuto;
    
}

public class EasyTextSystem : MonoBehaviour
{

    public RectTransform textBc;
    [HideInInspector]
    public int getTextID;
    [HideInInspector]
    public int currentMapID;
    public EasyTextSetting setting;

    public textSprite[] texts;

    public Image textBox;

    public RectTransform rectPointer;

    //private bool textIsPlaying = false;

    private bool ifCheckButton = false;
    [SerializeReference]
    private KeyCode Key1;
    [SerializeReference]
    private KeyCode Key2;

    public SceneTransManager STM;

    private bool initDone = false;

    private void Start()
    {
        startWhenAwake();
    }


    private void startWhenAwake()
    {
        if (setting.ifStartWhenAwake)
        {
            if (setting.texts[setting.AwakeTextId].ifWaitForPress)
            {
                ifCheckButton = true;
                Key1 = setting.texts[setting.AwakeTextId].pressKeyA;
                Key2 = setting.texts[setting.AwakeTextId].pressKeyB;
                getTextID = setting.AwakeTextId;
            }
            initDone = true;
            textPlays(setting.AwakeTextId);
        }
    }

    private void textPlays(int textId)
    {
        Debug.Log("开启文本协程");
        StopAllCoroutines();
        StartCoroutine(textPlay(textId));


    }


    bool ifWaitEnd = false;

    IEnumerator textPlay(int textId)
    {

        yield return new WaitUntil(()=>initDone);

        //textIsPlaying = true;
        float length = 0;
        while (Mathf.Abs(length - setting.texts[textId].length)>0.1f)
        {
            length = Mathf.Lerp(length, setting.texts[textId].length,setting.backShowsSpeed);
            //textBc.transform.localScale = Mathf.Lerp();
            textBc.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
            yield return new WaitForSecondsRealtime(setting.backShowsTimeWait);
        }
        length = setting.texts[textId].length;
        textBc.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
        yield return new WaitForSecondsRealtime(setting.backShowsTimeWait);

        Debug.Log("已淡入文本背景");
        textBox.gameObject.SetActive(true);
        Sprite text = setting.texts[textId].sprite;
        textBox.sprite = text;
        Color color = textBox.color;
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float a = 1;
        float alpha = 0;
        while (alpha < a)
        {
            alpha += Time.fixedUnscaledDeltaTime * setting.textShowsSpeed;
            textBox.color = new Color(r, g, b, alpha);
            yield return new WaitForSecondsRealtime(setting.textShowsTimeWait);

        }
        alpha = a;
        textBox.color = new Color(r, g, b, alpha);
        yield return new WaitForSecondsRealtime(setting.textShowsTimeWait);

        Debug.Log("已淡入文本图片");

        
        float pointLength = rectPointer.rect.width;
        rectPointer.gameObject.SetActive(true);
        float originalPointerLength = rectPointer.rect.width;

        if (setting.texts[textId].ifWaitForPress)
        {
            /*
            ifCheckButton = true;
            Key1 = setting.texts[textId].pressKeyA;
            Key2 = setting.texts[textId].pressKeyB;
            */


            while (!ifWaitEnd)
            {
                Debug.Log("文本光标旋转中");

                while (Mathf.Abs(pointLength)>0.1f)
                {
                    /*
                    if (checkPress(textId))
                    {
                        ifWaitEnd = true;
                        Debug.Log("跳出循环体");
                        break;
                    }*/
                    pointLength = Mathf.Lerp(pointLength, 0, setting.pointAngleSpeed);
                    rectPointer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pointLength);
                    yield return new WaitForSecondsRealtime(setting.pointAngleTimeWait);
                }
                /*
                if (checkPress(textId))
                {
                    ifWaitEnd = true;
                    Debug.Log("跳出循环体");
                    break;
                }
                if (ifWaitEnd)
                {
                    Debug.Log("跳出循环体");
                    break;
                }
                */

                rectPointer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
                //yield return new WaitForSecondsRealtime(setting.pointAngleTimeWait);


                while (Mathf.Abs(pointLength - originalPointerLength) > 0.1f)
                {
                    /*
                    if (checkPress(textId))
                    {
                        ifWaitEnd = true;
                        Debug.Log("跳出循环体");
                        break;
                    }
                    */
                    pointLength = Mathf.Lerp(pointLength, originalPointerLength, setting.pointAngleSpeed);
                    rectPointer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, pointLength);
                    yield return new WaitForSecondsRealtime(setting.pointAngleTimeWait);
                }
                /*
                if (checkPress(textId))
                {
                    ifWaitEnd = true;
                    Debug.Log("跳出循环体");
                    break;
                }
                if (ifWaitEnd)
                {
                    Debug.Log("跳出循环体");
                    break;
                }
                */

                rectPointer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalPointerLength);
                //yield return new WaitForSecondsRealtime(setting.pointAngleTimeWait);

                
            }
            rectPointer.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0f);
            yield return new WaitForSecondsRealtime(setting.pointAngleTimeWait);

            rectPointer.gameObject.SetActive(false);
            //光标消失

            

        }
        else
        {
            while (setting.disappearTime>0)
            {
                yield return new WaitForSecondsRealtime(setting.disappearTime);
            }
            Debug.Log("等待，文本准备消失");

        }





        color = textBox.color;
        r = color.r;
        g = color.g;
        b = color.b;
        a = 1;
        alpha = a;
        while (alpha > 0)
        {
            alpha -= Time.fixedUnscaledDeltaTime * setting.textShowsSpeed;
            textBox.color = new Color(r, g, b, alpha);
            yield return new WaitForSecondsRealtime(setting.textShowsTimeWait);

        }
        alpha = 0;
        textBox.color = new Color(r, g, b, alpha);
        yield return new WaitForSecondsRealtime(setting.textShowsTimeWait);

        Debug.Log("已淡出文本图片");
        textBox.gameObject.SetActive(false);



        length = textBc.rect.width;
        while (Mathf.Abs(length) > 0.1)
        {
            length = Mathf.Lerp(length, 0, setting.backShowsSpeed);
            //textBc.transform.localScale = Mathf.Lerp();
            textBc.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
            yield return new WaitForSecondsRealtime(setting.backShowsTimeWait);
        }
        length = 0;
        textBc.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
        yield return new WaitForSecondsRealtime(setting.backShowsTimeWait);

        Debug.Log("已淡出文本背景");




        if (setting.ifGoNextMapAfterPress)
        {
            Debug.Log("按键切换场景");

            STM.goesto(setting.nextMapNameIfGo);
        }

        ifWaitEnd = false;

        int prepareID = 0;

        for (int i = getTextID + 1; i < setting.texts.Length; i++)
        {
            if (setting.texts[i].ifAuto)
            {
                prepareID = i;
                break;
            }
        }
        
        if (prepareID != 0)
        {
            if (setting.texts[prepareID].ifWaitForPress)
            {
                ifCheckButton = true;
                Key1 = setting.texts[prepareID].pressKeyA;
                Key2 = setting.texts[prepareID].pressKeyB;
                getTextID = prepareID;
            }
            textPlays(prepareID);

        }
        else
        {
            rectPointer.gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        if (ifCheckButton)
        {
            if (Input.anyKeyDown)
            {
                ifWaitEnd = true;
                Debug.Log("按键判定成功");
                ifCheckButton = false;
                StopAllCoroutines();
                StartCoroutine(textDis());
            }

        }
    }



    IEnumerator textDis()
    {

        Color color = textBox.color;
        float r = color.r;
        float g = color.g;
        float b = color.b;
        float a = color.a;
        float alpha = a;
        while (alpha > 0)
        {
            alpha -= Time.fixedUnscaledDeltaTime * setting.textShowsSpeed;
            textBox.color = new Color(r, g, b, alpha);
            yield return new WaitForSecondsRealtime(setting.textShowsTimeWait);

        }
        alpha = 0;
        textBox.color = new Color(r, g, b, alpha);
        yield return new WaitForSecondsRealtime(setting.textShowsTimeWait);

        Debug.Log("已淡出文本图片");
        textBox.gameObject.SetActive(false);



        float length = textBc.rect.width;
        while (Mathf.Abs(length) > 0.1)
        {
            length = Mathf.Lerp(length, 0, setting.backShowsSpeed);
            //textBc.transform.localScale = Mathf.Lerp();
            textBc.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
            yield return new WaitForSecondsRealtime(setting.backShowsTimeWait);
        }
        length = 0;
        textBc.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
        yield return new WaitForSecondsRealtime(setting.backShowsTimeWait);

        Debug.Log("已淡出文本背景");




        if (setting.ifGoNextMapAfterPress)
        {
            Debug.Log("按键切换场景");

            STM.goesto(setting.nextMapNameIfGo);
        }

        ifWaitEnd = false;
        int prepareID = 0;

        for(int i = getTextID+1; i < setting.texts.Length; i++)
        {
            if (setting.texts[i].ifAuto)
            {
                prepareID = i;
                break;
            }
        }
        

        if (prepareID != 0)
        {
            if (setting.texts[prepareID].ifWaitForPress)
            {
                ifCheckButton = true;
                Key1 = setting.texts[prepareID].pressKeyA;
                Key2 = setting.texts[prepareID].pressKeyB;
                getTextID = prepareID;
            }
            textPlays(prepareID);

        }
        else
        {
            rectPointer.gameObject.SetActive(false);
        }

    }

    
     

}
