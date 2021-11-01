using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBird : Bird
{
    //public static float darkDeadNum=0;

    public W0L2Text W0L2T;

    public MusicPlayer MP;

    private void Awake()
    {
        FlowType = BirdType.dark;
        trailColor = trailRenderer.startColor;
        float r = 0;
        float g = 1;
        float b = 1;
        float a = 1;
        trailColor = new Color(r, g, b, a);
        trailRenderer.startColor = trailColor;

        OnLoseEvent += W0L2T.Lose;
        OnKillAllEvent += W0L2T.Win;

        var main1 = explodeEffect.gameObject.GetComponent<ParticleSystem>().main;
        main1.startColor = new Color(r, g, b, a);

        OnDeadHappenEvent += MP.DeadPlay;

        //var main2 = GoToTheDoor.main;
        //main2.startColor = new Color(r, g, b, a);



    }
    //dark的区别
    //1. 不向着player飞:已实现
    //2. 长相不同:已实现
    //3. 和bird没有相互作用力:已实现
    //4. 热量不会增加也不会减少:已实现
    //5. 与bird碰撞时，若bird的热量不足，则bird毁灭，否则自己毁灭:研究中

    public delegate void OnKillAllEventHandler();
    public event OnKillAllEventHandler OnKillAllEvent;

    public delegate void OnLoseEventHandler();
    public event OnLoseEventHandler OnLoseEvent;

    public delegate void OnDeadHappenEventHandler();
    public event OnDeadHappenEventHandler OnDeadHappenEvent;

    
    public override void initOBBMask()
    {

        seeObbMask = LayerMask.GetMask("DarkTrigger");


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Flow")) && (other.gameObject.GetComponent<Bird>().FlowType == BirdType.normal)&&(!other.gameObject.GetComponent<Bird>().ifDead)&&(!ifDead))
        {
            if (other.gameObject.GetComponent<Bird>().heat >= 0.6)
            {
                //MP.DeadPlay();
                OnDeadHappenEvent?.Invoke();

                GetComponent<Bird>().ifMoving = false;
                GetComponent<Bird>().explodeEffect.GetComponent<ParticleSystem>().Play();
                GetComponent<Bird>().trailRenderer.enabled = false;
                BM.DarkFlowDeadNum += 1;
                Debug.Log("热量足够，杀敌+1。至今杀敌人数：" + BM.DarkFlowDeadNum);
                if (BM.DarkFlowDeadNum == BM.DarkFlows.Length)
                {
                    OnKillAllEvent?.Invoke();//广播「杀光了所有敌人」
                }
                GetComponent<Bird>().ifDead = true;
            }
            else
            if (other.gameObject.GetComponent<Bird>().heat <= 0.4)
            {
                //MP.DeadPlay();
                OnDeadHappenEvent?.Invoke();

                other.gameObject.GetComponent<Bird>().ifMoving = false;
                other.gameObject.GetComponent<Bird>().explodeEffect.GetComponent<ParticleSystem>().Play();
                other.gameObject.GetComponent<Bird>().trailRenderer.enabled = false;
                BM.FlowDeadNum += 1;
                Debug.Log("热量不足，阵亡+1。至今阵亡人数：" + BM.FlowDeadNum);
                other.gameObject.GetComponent<Bird>().ifDead = true;
                if(BM.FlowDeadNum == BM.Flows.Length)
                {
                    OnLoseEvent?.Invoke();//广播「全部阵亡」
                }
            }
            

        }
    }

     



}
