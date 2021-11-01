using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class EndText : TextSystem
{
    public RectTransform imgBc;
    public float imgLength1;

    public Image imgText;
    public Sprite textSprite1;

    private int textID;

    private bool initDone;

    public RectTransform Thanks;


    public string nextMapName;
    public SceneTransManager STM;

    //private bool pressDone = false;

    public WaveTimeController WTC;

    private bool hasShowExit = false;

    private void Awake()
    {
        WTC.mainLoop = false;

    }
    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Transform trans = Thanks.transform;
        float y = trans.localPosition.y;
        float x = trans.localPosition.x;

        yield return new WaitForSecondsRealtime(4f);//等待4秒

        while (y<1600)
        {
            y += 0.2f;
            trans.localPosition = new Vector3(x,y,0);
            yield return null;


            Debug.Log("y="+y+"trans.LocalY="+ trans.localPosition.y);

            if ((y > 30) && (!hasShowExit))
            {
                yield return new WaitUntil(() => initDone);//初始化

                yield return new WaitUntil(() => STM.ifSceneFadein);//等待淡入场景

                StartCoroutine(fadeInLength(imgBc, imgLength1));//淡入背景

                yield return new WaitUntil(() => fadeInLengthDone);//等待淡入结束



                SetSprite(imgText, textSprite1);

                StartCoroutine(fadeInAlpha(imgText));//淡入文本1

                yield return new WaitUntil(() => fadeInAlphaDone);//等待淡入完毕

                hasShowExit = true;

            }


            //yield return new WaitUntil(() => pressDone);//等待按键

            if ((hasShowExit) && (Input.GetKeyDown(KeyCode.Escape)))
            {
                StartCoroutine(fadeOutAlpha(imgText));//淡出文本1

                yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出结束

                StartCoroutine(fadeOutLength(imgBc, imgLength1));//淡出背景

                yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕

                yield return new WaitForSecondsRealtime(2f);//等待2秒

#if UNITY_EDITOR //在编辑器模式下

                EditorApplication.isPlaying = false;

#else //正式环境下

        Application.Quit();

#endif
            }
            

            

        }



        while (true)
        {
            yield return null;

            if ((hasShowExit) && (Input.GetKeyDown(KeyCode.Escape)))
            {
                StartCoroutine(fadeOutAlpha(imgText));//淡出文本1

                yield return new WaitUntil(() => fadeOutAlphaDone);//等待淡出结束

                StartCoroutine(fadeOutLength(imgBc, imgLength1));//淡出背景

                yield return new WaitUntil(() => fadeOutLengthDone);//等待淡出完毕

                yield return new WaitForSecondsRealtime(2f);//等待2秒

#if UNITY_EDITOR //在编辑器模式下

                EditorApplication.isPlaying = false;

#else //正式环境下

        Application.Quit();

#endif
            }
        }






    }
}
