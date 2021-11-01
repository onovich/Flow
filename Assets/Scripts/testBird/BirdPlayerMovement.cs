using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//使用事件所需
using UnityEngine.SceneManagement;


public class BirdPlayerMovement : MonoBehaviour
{

    public GameObject view; 

    private float Angle;
    private float Move;

    public GameObject director;

    Rigidbody2D rg;//rg

    private GlobalCache global;


    private Transform playerTrans;
    //Vector2 playerpos; 

    [HideInInspector]
    bool IFMove = false;

    [HideInInspector]
    public bool IFInLooking = false;

    //private GameObject app;
     

    public BirdSetting setting;

    Scene scene;

    //public delegate void OnPlayerMove();//玩家有移动或旋转输入，会更新该广播，以用于关联的数据更新，比如Compass，以代替Update
    //public event EventHandler OnPlayerMoveEvent;

    //private Player player;
    //private float localR;
    //private GlobalEventManager GEM;
    float initialMoveSpeed;
    private float maxX = 7f;
    private float maxY = 4.5f;
    void comeInOppoSide()
    {
        Vector3 v = Vector2.zero;
        float x = transform.position.x;
        float y = transform.position.y;
        if (y >= maxY)
        {
            v.y = -maxY;

        }
        if (y <= -maxY)
        {
            v.y = maxY;
        }
        if (x >= maxX)
        {
            v.x = -maxX;
        }
        if (x <= -maxX)
        {
            v.x = maxX;
        }

        playerTrans.position += 2 * v; 

        //beepos += 2 * v;
        //beetrans.position = beepos;
    }

    public void Start()
    {
        IFMove = true;
        IFInLooking = false;

        playerTrans = transform;
        //playerpos = transform.position;

        //player = GetComponent<Player>();
        //transform = transform;

        //GEM = GlobalEventManager.instance;

        rg = GetComponent<Rigidbody2D>();//rg
        //localR = 3.6f;//进入屏内所距离，取自Compass类。如果希望安全考虑，可以把值调大一点，这样CompassManager可以提前刷新
        //localR = 3.8f;

        global = GlobalCache.instance;
        //app = global.APP;
        //CM = global.CM;
        //CR = global.CR;

        SceneManager.GetActiveScene();

        initialMoveSpeed = setting.playerMoveSpeed;

    }

    /*
    public PlayerMovement() 
    {
        transform = player.transform;
    }
    */

    public void FixedMovement()
    {
        if (IFMove)
        {
            playerTrans.Rotate(Vector3.forward * Angle * setting.playerAngleSpeed * -1f);//左右旋转
            rg.AddForce(playerTrans.up * Move * setting.playerMoveSpeed);
        }

        comeInOppoSide();

    }





    public void Movement()
    {
        Angle = Input.GetAxis("Horizontal");//按左右键时返回[-1，1]的值，即旋转的角度
        Move = Input.GetAxis("Vertical");//按上下键时返回一个[-1，1]的值，即移动的位置


        if (scene.name != "textBoard")
        {

        }
        else
        {
            if (Angle != 0)
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        
        if (Input.GetKey(KeyCode.Space))
        {
            if (Move != 0)//当角色在移动中时，空格=加速
            {
                setting.playerMoveSpeed = setting.playerMoveSpeedUp;
                //OnSpeedUpEvent?.Invoke(this, EventArgs.Empty);//广播「当前处于加速状态」
            }
            else
            {
                setting.playerMoveSpeed = initialMoveSpeed;
            }
           

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
