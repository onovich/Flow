using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用事件所需




public class Player : Character
{

    //Color glowColor;
    Transform playerTrans;
    public GameObject Trail;
    private GlobalCache global;

    public bool SpeedupOn=false;
    public bool MovementOn=true;
    public bool WaveShockOn = true;

    [HideInInspector]
    public float currentMoveSpeed;
    [HideInInspector]
    public float angleSpeed;
    [HideInInspector]
    public float initialMoveSpeed;
    [HideInInspector]
    public float moveSpeedUp;

    public SpriteRenderer render;

    public ParticleSystem GoToTheDoor;

    public SpriteRenderer circleRender;

    public ParticleSystem shockWave;

    private PlayerMovement PM;

    [HideInInspector]
    public bool ifDead;

    private void Start()
    {
        global = GlobalCache.instance;
        playerTrans = transform;
        ifDead = false;
        //PM = global.playerMove;
        GetComponent<PlayerMovement>().OnWaveShockEvent += ShockWave;
    }


    private void Awake()
    {
        //初始化速度
        currentMoveSpeed = 80f;//rg
        angleSpeed = 2f;
        initialMoveSpeed = currentMoveSpeed;
        moveSpeedUp = 170f;//rg

        //注册天敌
        killers.Add("Enemy"); killers.Add("Rock");

    }

    public void ShockWave()
    {
        Debug.Log("ShockWavedOnce");
        shockWave.Play();
    }




}
