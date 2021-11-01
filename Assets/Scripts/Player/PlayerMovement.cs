using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用事件所需
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;


public class PlayerMovement : MonoBehaviour
{

    new Transform transform;
   
    private float Angle;
    private float Move;

    Rigidbody2D rg;//rg

    private GlobalCache global;
     
    public delegate void OnWaveShockHandler();
    public event OnWaveShockHandler OnWaveShockEvent;

    public delegate void OnWaveShockErrorHandler();
    public event OnWaveShockErrorHandler OnWaveShockErrorEvent;

    public WaveTimeController WTC;

    [HideInInspector]
    bool IFMove = false;

    [HideInInspector]
    public bool IFInLooking = false;

    //private GameObject app;
    private CameraMove CM;
    private CameraRotate CR;

    public SceneTransManager STM;

    public W0L0Text w0l0;


    //Scene scene;

    //public delegate void OnPlayerMove();//玩家有移动或旋转输入，会更新该广播，以用于关联的数据更新，比如Compass，以代替Update
    //public event EventHandler OnPlayerMoveEvent;

    private Player player;
    //private float localR;
    //private GlobalEventManager GEM;

    public void Start()
    {
        IFMove = true;
        IFInLooking = false;
        player = GetComponent<Player>();
        transform = player.transform;

        //GEM = GlobalEventManager.instance;

        rg = GetComponent<Rigidbody2D>();//rg
        //localR = 3.6f;//进入屏内所距离，取自Compass类。如果希望安全考虑，可以把值调大一点，这样CompassManager可以提前刷新
        //localR = 3.8f;

        global = GlobalCache.instance;
        //app = global.APP;
        CM = global.CM;
        CR = global.CR;

        OnWaveShockErrorEvent += WTC.WaveError;
        OnWaveShockEvent += WTC.WaveCorrect;

        if (w0l0 != null)
        {
            OnWaveShockEvent += w0l0.checkBeat;
            OnWaveShockErrorEvent += w0l0.checkBeatError;
        }


        SceneManager.GetActiveScene();


        

    }


     


    //运动控制
    public void FixedMovement()
    {
        if ((IFMove)&&(player.MovementOn))
        {
            transform.Rotate(Vector3.forward * Angle * player.angleSpeed * -1f);//左右旋转
            rg.AddForce(transform.up * Move * player.currentMoveSpeed);
            //Debug.Log("Move="+Move+ ",player.Speed="+ player.currentMoveSpeed);
        }

        

    }

    //运动及所有技能计算
    public void Movement()
    {
        Angle = Input.GetAxis("Horizontal");//按左右键时返回[-1，1]的值，即旋转的角度
        //Move = Input.GetAxis("Vertical");//按上下键时返回一个[-1，1]的值，即移动的位置

        if (player.MovementOn)
        {
            Move = 1;
        }
        else
        {
            Move = 0;
        }
        

        //float targetScale = 0.35f;
        //float originalScale = 1f;


        ///---------------------------------------------------------------------
        //Wave控制
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.WaveShockOn)
            {
                if (!WTC.thisTurnhasBeat)
                {
                    OnWaveShockEvent?.Invoke();//广播「当前处于WaveShock状态」
                    Debug.Log("广播：「当前处于WaveShock状态」");
                }
            }
            else
            {
                if (!WTC.ifIgnore)
                {
                    OnWaveShockErrorEvent?.Invoke();//广播「当前处于WaveShock失败状态」
                    Debug.Log("广播：「当前处于WaveShock失败状态」");

                }

            }
        }

        ///---------------------------------------------------------------------
        //R重启
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            STM.goesto(scene.name);
        }

        ///---------------------------------------------------------------------
        //ESC退出
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR //在编辑器模式下

            EditorApplication.isPlaying = false;

#else //正式环境下

        Application.Quit();

#endif
        }




    }
    void FixedUpdate()
    {
        
        FixedMovement();
    }
    void Update()
    {
        Movement();
    }

}
