using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTimeController : MonoBehaviour
{
    void Start()
    {
        trans = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        thisTurnPass = false;

        


        //主循环，所有初始化应该在此之前完成。
        StartCoroutine(Loop());
    }

    Transform trans;
    //public delegate void OnEnergyGatheredHandler();
    //public event OnEnergyGatheredHandler OnEnergyGathered;
    //public delegate void OnEnergyDissipateHandler();
    //public event OnEnergyDissipateHandler OnEnergyDissipate;

    public Player player;
    private SpriteRenderer sprite;
    public Transform playerTrans;

    public bool thisTurnhasBeat;//为真时，说明本轮已经发出beat，不需要重复发出
    

    public bool thisTurnPass;//为真时，这一轮即便时机正确，按键也无效。用于过早按键的情况。
    public bool ifIgnore;//为真时，说明此时处于周期间歇，不作检测（否则会把间歇时的误触带到下一轮里，反玩家认知）


    public delegate void OnHeartBeatHandler();
    public event OnHeartBeatHandler OnHeartBeatEvent;

    private bool HeartBeatThisTurn;//为真时，本轮不再执行心跳
    public bool mainLoop = true;


    public void WaveCorrect()
    {
        thisTurnhasBeat = true;
    }
    public void WaveError()
    {
        thisTurnPass = true;
    }


    IEnumerator Loop()
    {
        Vector3 transScaleOriginal = trans.localScale;
        Color color = sprite.color;
        Vector3 playerScaleOriginal = playerTrans.localScale;

        while (true)
        {
            yield return null;
            if (mainLoop)
            {
                color.a = 0.1f;
                sprite.color = color;
                Vector3 transScale = new Vector3(2, 2, 1);
                trans.localScale = transScale;
                ifIgnore = false;
                thisTurnhasBeat = false;
                Vector3 playerScale = new Vector3(1f, 1f, 1);
                playerTrans.localScale = playerScale;
                HeartBeatThisTurn = false;
                thisTurnPass = false;

                Debug.Log("刷新Circle");

                while (transScale.x > 0.11)
                {
                    Debug.Log("Looping");
                    transScale = Vector3.Slerp(transScale, new Vector3(0.1f, 0.1f, 1f), 0.1f);
                    trans.localScale = transScale;
                    if (transScale.x < 2)
                    {
                        color.a = Mathf.Lerp(color.a, 0.08f, 0.08f);
                        sprite.color = color;
                        if (thisTurnPass)
                        {
                            Debug.Log("Pass");
                            break;
                        }
                    }
                    yield return null;


                    if ((!HeartBeatThisTurn) && (transScale.x <= 0.35f) && (transScale.x > 0.18f))
                    {
                        player.WaveShockOn = true;
                        playerScale = new Vector3(1.5f, 1.5f, 1);
                        playerTrans.localScale = playerScale;
                        OnHeartBeatEvent?.Invoke();
                        HeartBeatThisTurn = true;

                    }
                    if (transScale.x <= 0.12f)
                    {
                        player.WaveShockOn = false;

                    }


                    //跳动
                    if (playerScale.x > 1)
                    {
                        playerScale = Vector3.Slerp(playerScale, new Vector3(0.8f, 0.8f, 1), 0.1f);
                        playerTrans.localScale = playerScale;

                    }
                    else
                    {
                        playerScale = new Vector3(1f, 1f, 1);
                        playerTrans.localScale = playerScale;
                    }
                    yield return null;

                    sprite.color = color;
                    yield return null;




                }


                //本轮没有按错时
                if (!thisTurnPass)
                {
                    ifIgnore = true;
                    yield return new WaitForSecondsRealtime(0.1f);
                    ifIgnore = false;
                }

                //本轮按错时

                else
                {
                    player.WaveShockOn = false;

                    while (transScale.x < 4.1)
                    {
                        Debug.Log("按错Loop");
                        transScale = Vector3.Lerp(transScale, new Vector3(4.2f, 4.2f, 1f), 0.02f);
                        color.a = Mathf.Lerp(color.a, 0f, 0.05f);
                        trans.localScale = transScale;
                        sprite.color = color;
                        yield return null;

                    }
                    ifIgnore = true;
                    yield return new WaitForSecondsRealtime(0.1f);
                    ifIgnore = false;
                    thisTurnPass = false;
                    Debug.Log("本轮跳过");




                }
            }

            

        }
    }




}
