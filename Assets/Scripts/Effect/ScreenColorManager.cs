using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;//使用Volume所需
using UnityEngine.Rendering.Universal;//使用ColorAdjustments、ColorCurves所需
using System;//使用事件所需

public class ScreenColorManager : MonoBehaviour
{

    private GameObject blackEffect;
    private Volume v;
    private ColorAdjustments colorAdjust;
    private float contrast = 100f;
    private float saturation = -100f;
    private ColorCurves curve;
    private TextureCurve textCurve;
    private  AnimationCurve baseCurve;
    private GlobalCache global;
 

    //void BlackAndWhite()//test
    void BlackAndWhite(object sender, EventArgs e)//event
    {
        

        StartCoroutine(SetContrast());
        //StartCoroutine(SetSaturation());
        StartCoroutine(SetCurse());


    }

    public void GameOverDisplay()
    {
        StartCoroutine(SetContrast());
        StartCoroutine(SetSaturation());
    }
    public void GameReset()
    {
        Init();
    }

    public IEnumerator SetContrast()
    {
        while (colorAdjust.contrast.value < contrast)
        {
            //colorAdjust.contrast.value += Time.deltaTime * 40;
            colorAdjust.contrast.value += global.deltaTime * 40;
            yield return null;
        }
    }

    public IEnumerator SetSaturation()
    {
        while(colorAdjust.saturation.value > saturation)
        {
            //colorAdjust.saturation.value -= Time.deltaTime * 40;
            colorAdjust.saturation.value -= global.deltaTime * 40;
            yield return null;
        }
    }

    public IEnumerator SetCurse()
    {
        float index = 0;
        //0,0 1,1 -> 0,1 1,0

        /*
        while (index<1)
        {
            textCurve = new TextureCurve(baseCurve, 0f, false, new Vector2(1, 1));
            index += Time.deltaTime * 0.5f;
            textCurve.AddKey(0f, index);
            textCurve.AddKey(1f, 1-index);
            curve.master.Override(textCurve);
            //curve.master.value = textCurve;
            

            yield return null;
        }
        */
        //Debug.Log("反色完成,index="+index);
        //Debug.Log("最后一次刷新的点1=(0," + index+"),点2=(1,"+(1-index)+")");
        while (index < 1)
        {
            textCurve = new TextureCurve(baseCurve, 0f, false, new Vector2(1, 1));
            //index += Time.deltaTime * 2f;
            index += global.deltaTime * 2f;
            textCurve.AddKey(0f, 0f);
            textCurve.AddKey(1f, 1 - index);
            curve.master.Override(textCurve);
            //curve.master.value = textCurve;


            yield return null;
        }
        index = 0;
        while (index < 1)
        {
            textCurve = new TextureCurve(baseCurve, 0f, false, new Vector2(1, 1));
            //index += Time.deltaTime * 2f;
            index += global.deltaTime * 2f;
            textCurve.AddKey(0f, index);
            textCurve.AddKey(1f, 0);
            curve.master.Override(textCurve);
            //curve.master.value = textCurve;


            yield return null;
        }

    }
    

    private void Start()
    {
        global = GlobalCache.instance;
        blackEffect = global.ColorManager;
        //blackEffect = GameObject.FindGameObjectWithTag("ColorManager");
        v = blackEffect.GetComponent<Volume>();
        ColorAdjustments tmp1;
        ColorCurves tmp2;
        if (v.profile.TryGet<ColorAdjustments>(out tmp1))
        {
            colorAdjust = tmp1;
        }

        if (v.profile.TryGet<ColorCurves>(out tmp2))
        {
            curve = tmp2;
        }
    

        baseCurve = new AnimationCurve();

        textCurve = new TextureCurve(baseCurve,0f,false,new Vector2(1,1));


        Init();
    }


    private void Init()//重生的时候可以调用这个，要做的时候可以使其订阅玩家重生事件
    {


        colorAdjust.contrast.value = 0f;
        colorAdjust.saturation.value = 0f;
        //colorAdjust.contrast.value = 100f;
        //colorAdjust.saturation.value = -100f;




        textCurve.AddKey(0f, 0f);
        textCurve.AddKey(1f, 1f);
        //textCurve.AddKey(0f, 1f);
        //textCurve.AddKey(1f, 0f);
 
        curve.master.Override(textCurve);


        //BlackAndWhite();//测试用，正式版需要注释掉，改为事件订阅

    }



}
