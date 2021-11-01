using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleText : TextSystem
{
    public RectTransform imgBc;
    public float imgLength;


    public Image imgText;
    public Sprite textSprite;

    private bool initDone;

    public string nextMapName;
    public SceneTransManager STM;

    private bool pressDone = false;
    public WaveTimeController WTC;

    private void Awake()
    {
        WTC.mainLoop = false;

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressDone = true;
        }
        else
        {
            pressDone = false;
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


        yield return new WaitUntil(() => initDone);//等待初始化

        yield return new WaitUntil(() => STM.ifSceneFadein);//等待淡入场景

        StartCoroutine(fadeInLength(imgBc,imgLength));//淡入背景

        yield return new WaitUntil(() => fadeInLengthDone);//等待淡入结束

        SetSprite(imgText, textSprite);

        StartCoroutine(fadeInAlpha(imgText));//淡入文本

        yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入结束

        yield return new WaitUntil(() => pressDone);//等待按键

        StartCoroutine(fadeOutAlpha(imgText));//淡出文本

        yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出结束

        StartCoroutine(fadeOutLength(imgBc, imgLength));//淡出背景

        yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出结束

        STM.goesto(nextMapName);//场景切换






    }



}
