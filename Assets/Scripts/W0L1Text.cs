using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class W0L1Text : TextSystem
{
    public RectTransform imgBc;
    public float imgLength1;
    public float imgLength2;
    public float imgLength3;
    public float imgLength4;
    public float imgLength5;



    public Image imgText;
    public Sprite textSprite1;
    public Sprite textSprite2;
    public Sprite textSprite3;
    public Sprite textSprite4;
    public Sprite textSprite5;



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
    private void Start()
    {
        initDone = false;
        initAlpha(imgText);
        initLength(imgBc);
        //pressDone = false;
        initDone = true;
        StartCoroutine(textPlay());
    }

    IEnumerator textPlay()
    {


        yield return new WaitUntil(() => initDone);//初始化

        yield return new WaitUntil(() => STM.ifSceneFadein);//等待淡入场景

        StartCoroutine(fadeInLength(imgBc, imgLength1));//淡入背景

        yield return new WaitUntil(() => fadeInLengthDone);//等待淡入结束

        //textID = 1;

        SetSprite(imgText, textSprite1);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本1

        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(8f);//等待4秒


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

        yield return new WaitForSecondsRealtime(9f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本2

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕







        //textID = 3;

        //StartCoroutine(changeLength(imgBc, imgLength1, imgLength2));//调整背景长度
        setLength(imgBc, imgLength3);

        Debug.Log("准备出第二条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束



        Debug.Log("出第二条台词");

        SetSprite(imgText, textSprite3);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本3

        Debug.Log("第二条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(6f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本3

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕







        //textID = 4;

        //StartCoroutine(changeLength(imgBc, imgLength1, imgLength2));//调整背景长度
        setLength(imgBc, imgLength4);

        Debug.Log("准备出第4条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束



        Debug.Log("出第4条台词");

        SetSprite(imgText, textSprite4);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本4

        Debug.Log("第4条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(8f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本4

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕





        //textID = 5;

        //StartCoroutine(changeLength(imgBc, imgLength1, imgLength2));//调整背景长度
        setLength(imgBc, imgLength5);

        Debug.Log("准备出第4条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束



        Debug.Log("出第4条台词");

        SetSprite(imgText, textSprite5);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本4

        Debug.Log("第4条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(10f);//等待6秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本4

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕











        StartCoroutine(fadeOutLength(imgBc, imgLength2));//淡出背景

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕

        yield return new WaitForSecondsRealtime(2f);//等待2秒

        //STM.goesto(nextMapName);






    }
}
