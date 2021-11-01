using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class W0L2Text : TextSystem
{
    public RectTransform imgBc;
    public float imgLength1;
    public float imgLength2;
    public float imgLength3;
    public float imgLength4;





    public Image imgText;
    public Sprite textSprite1;
    public Sprite textSprite2;
    public Sprite textSprite3;
    public Sprite textSprite4;


    public WaveTimeController WTC;


    //private int textID;

    private bool initDone;

    //public string nextMapName;
    public SceneTransManager STM;

    //private bool pressDone = false;

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            pressDone = true;
        }
        else
        {
            pressDone = false;
        }
    }
    */
    private void Awake()
    {
        WTC.mainLoop = false;

    }

    private void Start()
    {
        initDone = false;
        initAlpha(imgText);
        initLength(imgBc);
       // pressDone = false;
        initDone = true;
        StartCoroutine(textPlay());
    }

    IEnumerator textPlay()
    {

        Time.timeScale = 0f;


        yield return new WaitUntil(() => initDone);//初始化

        yield return new WaitUntil(() => STM.ifSceneFadein);//等待淡入场景

        StartCoroutine(fadeInLength(imgBc, imgLength1));//淡入背景

        yield return new WaitUntil(() => fadeInLengthDone);//等待淡入结束

        //textID = 1;

        SetSprite(imgText, textSprite1);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本1

        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(8f);//等待3秒


        StartCoroutine(fadeOutAlpha(imgText));//淡出文本1

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出结束

        //textID = 2;

        //StartCoroutine(changeLength(imgBc, imgLength1, imgLength2));//调整背景长度
        setLength(imgBc, imgLength2);

        Debug.Log("准备出第二条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束



        Debug.Log("出第二条台词");

        SetSprite(imgText, textSprite2);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本2

        Debug.Log("第二条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(6f);//等待3秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本2

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕











        StartCoroutine(fadeOutLength(imgBc, imgLength2));//淡出背景

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕

        yield return new WaitForSecondsRealtime(1f);//等待2秒

        //STM.goesto(nextMapName);

        WTC.mainLoop = true;

        Time.timeScale = 1f;


        yield return null;


    }


    public void Lose()
    {
        StartCoroutine(whenLose());
    }

    public void Win()
    {
        StartCoroutine(whenKillAll());
    }



    IEnumerator whenLose()
    {




        //textID = 99;

        //StartCoroutine(changeLength(imgBc, imgLength1, imgLength2));//调整背景长度
        setLength(imgBc, imgLength4);

        Debug.Log("准备出失败台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束



        Debug.Log("出失败台词");

        SetSprite(imgText, textSprite4);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本99

        Debug.Log("失败台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(8f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本99

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕


        StartCoroutine(fadeOutLength(imgBc, imgLength4));//淡出背景

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕

        yield return new WaitForSecondsRealtime(2f);//等待2秒


        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕

        STM.goesto("W0L2");



    }

    IEnumerator whenKillAll()
    {


        //textID = 999;

        //StartCoroutine(changeLength(imgBc, imgLength1, imgLength2));//调整背景长度
        setLength(imgBc, imgLength3);

        Debug.Log("准备出成功台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束



        Debug.Log("出成功台词");

        SetSprite(imgText, textSprite3);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本999

        Debug.Log("成功台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(6f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本999

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕



        StartCoroutine(fadeOutLength(imgBc, imgLength3));//淡出背景

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕

        yield return new WaitForSecondsRealtime(2f);//等待2秒


        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕




    }











}
