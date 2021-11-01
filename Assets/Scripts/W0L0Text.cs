using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class W0L0Text : TextSystem
{
    public RectTransform imgBc;
    public float imgLength1;
    public float imgLength2;

    public float imgLength3;

    public float imgLength101;
    public float imgLength102;
    public float imgLength103;


    public Image imgText;
    public Sprite textSprite1;
    public Sprite textSprite2;

    public Sprite textSprite3;

    public Sprite textSprite101;
    public Sprite textSprite102;
    public Sprite textSprite103;

    private int textID;

    private bool initDone;

    public string nextMapName;
    public SceneTransManager STM;

    private bool pressDone = false;

    private bool beatRight = false;

    private bool nowCanCheckBeat = false;

    

    public void checkBeat()
    {
        Debug.Log("checkBeat");
        if (nowCanCheckBeat)
        {
            beatRight = true;
            Debug.Log("beatRight");

        }
    }
    public void checkBeatError()
    {
        if (nowCanCheckBeat)
        {
            beatRight = false;
            //
        }
    }


    private void Update()
    {
        if (textID == 1)
        {
            if (Input.anyKey)
            {
                pressDone = true;
            }
            else
            {
                pressDone = false;
            }
        }
        if(textID == 2)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                pressDone = true;
            }
            else
            {
                pressDone = false;
            }
        }
        if((textID == 3)||(textID == 102))
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
    }

    private void Start()
    {
        initDone = false;
        initAlpha(imgText);
        initLength(imgBc);
        pressDone = false;
        initDone = true;
        StartCoroutine(textPlay());
    }

    IEnumerator textPlay()
    {


        yield return new WaitUntil(() => initDone);//初始化

        yield return new WaitUntil(() => STM.ifSceneFadein);//等待淡入场景

        StartCoroutine(fadeInLength(imgBc, imgLength1));//淡入背景

        yield return new WaitUntil(() => fadeInLengthDone);//等待淡入结束



        textID = 1;



        //Debug.Log("准备出第二条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束

        setLength(imgBc, imgLength1);


        //Debug.Log("出第二条台词");

        SetSprite(imgText, textSprite1);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本2

        //Debug.Log("第二条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(4f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本2

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕



        textID = 2;



        //Debug.Log("准备出第二条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束

        setLength(imgBc, imgLength2);


        //Debug.Log("出第二条台词");

        SetSprite(imgText, textSprite2);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本2

        //Debug.Log("第二条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(4f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本2

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕



        
        textID = 3;


        //StartCoroutine(changeLength(imgBc, imgLength1, imgLength2));//调整背景长度
        setLength(imgBc, imgLength3);

        SetSprite(imgText, textSprite3);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本3

        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(7f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本3

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出结束




        nowCanCheckBeat = true;



        StartCoroutine(fadeOutLength(imgBc, imgLength3));//淡出背景

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕




        ///等待正确释放热能

        yield return new WaitUntil(() => beatRight);


        nowCanCheckBeat = false;
        beatRight = false;


        yield return new WaitUntil(() => initDone);//初始化

        yield return new WaitUntil(() => STM.ifSceneFadein);//等待淡入场景

        StartCoroutine(fadeInLength(imgBc, imgLength102));//淡入背景

        yield return new WaitUntil(() => fadeInLengthDone);//等待淡入结束


        textID = 102;



        //Debug.Log("准备出第二条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束
        setLength(imgBc, imgLength102);

        //Debug.Log("出第二条台词");

        SetSprite(imgText, textSprite102);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本2

        //Debug.Log("第二条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(6f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本2

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕


        StartCoroutine(fadeOutLength(imgBc, imgLength102));//淡出背景

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕


        nowCanCheckBeat = true;




        ///等待正确释放热能

        yield return new WaitUntil(() => beatRight);




        yield return new WaitUntil(() => initDone);//初始化

        yield return new WaitUntil(() => STM.ifSceneFadein);//等待淡入场景

        StartCoroutine(fadeInLength(imgBc, imgLength103));//淡入背景

        yield return new WaitUntil(() => fadeInLengthDone);//等待淡入结束


        textID = 103;



        //Debug.Log("准备出第二条台词");

        //yield return new WaitUntil(() => changeLengthDone);//等待调整结束
        setLength(imgBc, imgLength103);

        //Debug.Log("出第二条台词");

        SetSprite(imgText, textSprite103);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本2

        //Debug.Log("第二条台词出完");


        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

        //yield return new WaitUntil(() => pressDone);//等待按键

        yield return new WaitForSecondsRealtime(4f);//等待4秒

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本2

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出完毕


        StartCoroutine(fadeOutLength(imgBc, imgLength103));//淡出背景

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕



        STM.goesto(nextMapName);






    }
}
