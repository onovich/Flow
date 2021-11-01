using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioSource BGM;
    public AudioSource Beat;
    public AudioSource BeatError;
    public AudioSource Wave;
    public AudioSource WaveError;



    public AudioSource FlowsGoes;
    public AudioSource PlayerGoes;

    public AudioSource Dead1;
    public AudioSource Dead2;
    public AudioSource Dead3;

    public AudioSource EndBGS;

    public AudioSource DoorOpen;
    public AudioSource DoorChange;
 

    public WaveTimeController WTC;
    public PlayerMovement playerMove;

    static MusicPlayer S;

    void Awake()
    {

        if (S == null) { S = this; }

        else if (S == this) { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);


    }


    void Start()
    {
        if (BGM != null)
        {
            BGM.Play();

        }
        else
        {
            Debug.Log("未注册BGM");
        }

        //Mu.Stop();//声音停止
        //Mu.loop = true;//设置声音为循环播放 ;
        //Mu.Play();//声音播放

        playerMove.OnWaveShockEvent += WavePlay;
        playerMove.OnWaveShockErrorEvent += BeatErrorPlay;
        WTC.OnHeartBeatEvent += BeatPlay;
        



    }


    public void DoorOpenPlay()
    {
        DoorOpen.Play();
    }

    public void DoorChangePlay()
    {
        DoorChange.Play();
    }

    void BeatPlay()
    {
        Beat.Play();
         
    }
    void BeatErrorPlay()
    {

    }
    void WavePlay()
    {
        Wave.Play();
    }

    public void FlowGoesPlay()
    {
        FlowsGoes.Play();
    }
    public void PlayerGoesPlay()
    {
        PlayerGoes.Play();
    }

     


    public void DeadPlay()
    {
        float n = Random.Range(1f, 10f);
         
        if ((n >= 1) && (n < 3.3))
        {
            if ((!Dead1.isPlaying)&& (!Dead2.isPlaying)&& (!Dead3.isPlaying))
            {
 
            }
            Dead1.Play();

        }
        if ((n >= 3.3) && (n < 6.6))
        {
            if ((!Dead1.isPlaying) && (!Dead2.isPlaying) && (!Dead3.isPlaying))
            {
 
            }
            Dead2.Play();

        }
        if ((n >= 6.6) && (n <= 10))
        {
            if ((!Dead1.isPlaying) && (!Dead2.isPlaying) && (!Dead3.isPlaying))
            {
 
            }
            Dead3.Play();

        }




    }


}
