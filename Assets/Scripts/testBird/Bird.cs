using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class aBird
{
    public Vector2 flee = Vector2.zero; 
    public Vector2 gather = Vector2.zero;
    public Vector2 fixedDirector = Vector2.zero;
    public Vector2 playerGuider = Vector2.zero;
    public Vector2 final = Vector2.zero;
    public int hitNumber = 0;
}

public enum BirdType
{
    normal,
    dark,

}

public class Bird : Character
{
    public GameObject director;

    //[HideInInspector]
    //public static int deadNum =0;

    private Transform beetrans;
    [HideInInspector]
    public Vector2 beepos;
    private Transform directtrans;
    private Vector2 directpos;

    private float speedScale = 1f;

    public ParticleSystem GoToTheDoor;

    //BirdSetting setting;
    public BirdSetting setting;
    private float maxX = 9.2f;
    private float maxY = 5f;

    private float saveMaxX = 8.7f;
    private float saveMaxY = 4.8f;
    public GameObject view;
    [HideInInspector]
    public LayerMask seeObbMask;
    //[HideInInspector]
    //public int birdID = 0;

    private Material birdme;
    

    private ContactFilter2D filter;


    public GameObject[] RayPoints;


    public bool theOne = false;

    private GameObject player;
    private GlobalCache global;
    private Transform playerTrans;
    private Vector2 playerPos;
    [HideInInspector]
    public bool ifInPlayerView = false;

    public TrailRenderer trailRenderer;
    [HideInInspector]
    public Color trailColor;

    public bool ifColorCold = false;
    public bool ifColorHot = false;
    [HideInInspector]
    public float disFromPlayer;


    public float heat;//温度

    public BirdType FlowType = BirdType.normal;

    public BirdManager BM;

    [HideInInspector]
    public bool ifMoving;

    [HideInInspector]
    public bool ifDead;

    [HideInInspector]
    public bool canGoDoor;
    [HideInInspector]
    public bool canNotGoDoor;

    void refreshIfHotOrCold()
    {

        trailColor = trailRenderer.startColor;
        float r = trailColor.r;
        float g = trailColor.g;
        float b = trailColor.b;
        float a = trailColor.a;

        if ((Mathf.Abs(r - 1) <= 0.02) && (Mathf.Abs(g - 0) <= 0.02) && (Mathf.Abs(b - 0) <= 0.02))
        {
            //Debug.Log("变热止于此");
            ifColorHot = true;
        }
        else
        {
            //Debug.Log("可变热");
            ifColorHot = false;
        }
        
        
        if((Mathf.Abs(r-1)<=0.02)&& (Mathf.Abs(g - 1f) <= 0.02) && (Mathf.Abs(b - 1) <= 0.02))
        {
            //Debug.Log("变冷止于此");
            ifColorCold =  true;
        }
        else
        {
            //Debug.Log("可变冷");
            ifColorCold =  false;
        }

        heat = Mathf.Abs(1-b);
        if (heat >= setting.heatWhenDoorOpen)
        {
            canGoDoor = true;
        }
        else
        {
            canGoDoor = false;
        }

        if (heat <= setting.heatWhenDoorClose)
        {
            canNotGoDoor = true;
        }
        else
        {
            canNotGoDoor = false;
        }


    }


    public void refreshColorHot()
    {
        //float dis = Vector2.Distance(beepos, playerPos);
        trailColor = trailRenderer.startColor;
        float r = trailColor.r;
        float g = trailColor.g;
        float b = trailColor.b;
        float a = trailColor.a;
        float n = setting.warmSpeed;
        r = Mathf.Lerp(r,1,n);
        g = Mathf.Lerp(g, 0,n);
        b = Mathf.Lerp(b,0,n); ;
        //r = 1;
        //g = 0;
        //b = 0;


        trailRenderer.startColor = new Color(r, g, b, a);

        
        var main1 = explodeEffect.gameObject.GetComponent<ParticleSystem>().main;
        main1.startColor = new Color(r, g, b, a);

        //var main2 = GoToTheDoor.main;
        //main2.startColor = new Color(r, g, b, a);


        refreshIfHotOrCold();

    }




    public void refreshColorCold()
    {
        //float dis = Vector2.Distance(beepos, playerPos);
        float r = trailColor.r;
        float g = trailColor.g;
        float b = trailColor.b;
        float a = trailColor.a;
        float n = setting.coolSpeed;

        //r =0;
        //g = 0.5f;
        //b = 1; ;

        r = Mathf.Lerp(r, 1, n);
        g = Mathf.Lerp(g, 1f,n);
        b = Mathf.Lerp(b, 1, n); ;

        trailRenderer.startColor = new Color(r, g, b, a);

        var main1 = explodeEffect.gameObject.GetComponent<ParticleSystem>().main;
        main1.startColor = new Color(r, g, b, a);

        //var main2 = GoToTheDoor.main;
        //main2.startColor = new Color(r, g, b, a);

        refreshIfHotOrCold();


    }










    Vector2 AvOfAll(Vector2[] L)
    {
        Vector2 V = Vector2.zero;
        for(int i = 0; i < L.Length; i ++)
        {
            V += L[i];
        }
        V /= (L.Length);
        return V;
    }

    bool Ranbool()
    {
        float i = Random.Range(0, 1);
        if (i > 0.5) { return true; } else { return false; }


    }

     

    Vector2 rightWay(bool[] wrongWay)
    {
        Vector2 addWrong = Vector2.zero;

        for (int i = 0; i < RayPoints.Length; i++)
        {
            if (wrongWay[i] == true)
            {
                Vector2 rayp = RayPoints[i].transform.position;
                Vector2 trans = beepos;
                addWrong += (rayp - trans);
            }

        }



        return (Vector2.zero - addWrong);
    }

    


    aBird FixMoving()
    {

        


        int hitNumber = 99;
        
        aBird ab = new aBird();
        float dis;

        

        Vector2[] birds = new Vector2[200];//用于存储所有射线贯穿的鸟类坐标
        Vector2[] directors = new Vector2[200];//用于存储所有射线贯穿的鸟类航向的相对位置(director相对于其鸟
        //Vector2[] flees = new Vector2[300];//用于存储所有射线贯穿的鸟类相对位置(鸟类相对于本体
        bool[] wrongWay = new bool[36];

        int index = 0;
        int hitAllNum = 0;

        for (int i = 0; i < RayPoints.Length; i++)
        {
            RaycastHit2D[] hits = new RaycastHit2D[36];//用于存储每根射线贯穿的碰撞体
            dis = Vector2.Distance(beepos, RayPoints[i].transform.position);
            hitNumber = Physics2D.Raycast(beepos, RayPoints[i].transform.position, filter, hits, dis);

            if (hitNumber >0 )
            {
                for(int j=0;j < hitNumber; j++)
                {
                    if (index < birds.Length-1)
                    {
                        birds[index] = hits[j].transform.position;

                        if(hits[j].collider.gameObject.GetComponent<Bird>() == null)
                        {
                            Vector2 dircPos = hits[j].collider.gameObject.GetComponent<BirdPlayerMovement>().director.transform.position;
                            Vector2 hitJpos = hits[j].transform.position;
                            directors[index] = dircPos - hitJpos;
                        }
                        else
                        {
                            Vector2 dircPos = hits[j].collider.gameObject.GetComponent<Bird>().director.transform.position;
                            Vector2 hitJpos = hits[j].transform.position;
                            directors[index] = dircPos - hitJpos;
                        }
                        

                        index += 1;
                        hitAllNum += 1;
                    }
                    

                }
                wrongWay[i] = true;

            }
            else
            {
                wrongWay[i] = false;
            }

            

        }

        if (hitAllNum == 0)
        {
            ab.flee = (directpos- beepos)* setting.fleeWeight;
        }
        else
        {
            ab.flee = (SteerTowards(rightWay(wrongWay)))*setting.fleeWeight;

        }

        ///***///
        ab.playerGuider = (playerPos - beepos)*setting.playerGuideWeight * (heat+ setting.PlayerGuidMin) * setting.PlayerGuideScaleFromeHeat;
        ab.gather = (SteerTowards(AvOfAll(birds))- beepos)* setting.gatherWeight;
        ab.fixedDirector = (SteerTowards(AvOfAll(directors)))* setting.fixedWeight;
        ///***///

        if (setting.ifHasPlayer)
        {
            ab.final = ab.gather + ab.fixedDirector + ab.flee + ab.playerGuider;

        }
        else
        {
            ab.final = ab.gather + ab.fixedDirector + ab.flee;

        }

        ab.hitNumber = hitNumber;
        return ab;

    }

    
    public virtual void initOBBMask()
    {
        seeObbMask = LayerMask.GetMask("Trigger");
    }

    void Start()
    {
        /*
        if (theOne)
        {
            birdme = GetComponent<SpriteRenderer>().material;
            birdme.color = Color.yellow;
        }
        */


        global = GlobalCache.instance;
        player = global.player;
        playerTrans = player.transform;
        playerPos = playerTrans.position;
        rg = player.GetComponent<Rigidbody2D>();
        ifMoving = true;
        ifDead = false;
        beetrans = transform;
        beepos = beetrans.position;
        directtrans = director.transform;
        directpos = directtrans.position;
        canGoDoor = false;
        canNotGoDoor = true;
        trailColor = trailRenderer.startColor;

        BM = global.BirdManager;

        speedScale = (heat + setting.speedScaleFromHeat) / 2;

        //filter = new ContactFilter2D().NoFilter();

        initOBBMask();

        trailRenderer.Clear();

        filter = new ContactFilter2D
        {
            useLayerMask = true,
            //useTriggers = false,
            useTriggers = true,
            layerMask = seeObbMask,

        };
        float startSpeed = (setting.minSpeed + setting.maxSpeed) / 2 * speedScale;
        velocity = transform.up * startSpeed;
    }

    
    public bool ifTest = false;

    Vector2 velocity;

    Vector2 SteerTowards(Vector2 ve)
    {
        Vector2 v = ve.normalized * setting.maxSpeed  - velocity;
        Vector2 n = Vector2.ClampMagnitude(v, setting.maxSteerForce);
        return (n);
    }

    
    private Rigidbody2D rg;
    private float playerAngle;
    private float playerMove;


  

     

    private void FixedUpdate()
    {
        

        if ((!ifDead)&&(ifMoving))
        {
            comeInOppoSide();

            beepos = beetrans.position;
            playerPos = playerTrans.position;
            directpos = directtrans.position;

            disFromPlayer = Vector2.Distance(beepos, playerPos);


            velocity += FixMoving().final * Time.deltaTime;
            velocity = new Vector2(velocity.x, velocity.y);
            float speed = velocity.magnitude;
            Vector2 dir = velocity / speed;
            speed = Mathf.Clamp(speed, setting.minSpeed, setting.maxSpeed) * speedScale;

            velocity = dir * speed;
            beepos += velocity * Time.deltaTime;
            beetrans.position = beepos;
            beetrans.up = dir;
            speedScale = (heat + setting.speedScaleFromHeat) / 2;
        }
    }

    void comeInOppoSide()
    {
        Vector2 v = Vector2.zero;
        float x = transform.position.x;
        float y = transform.position.y;
        if (y >= maxY)
        {
            v.y = -saveMaxY;
            beepos += 2 * v;
            trailRenderer.Clear();
            beetrans.position = beepos;
            trailRenderer.Clear();


        }
        if (y <= -maxY)
        {
            v.y = saveMaxY;
            beepos += 2 * v;
            trailRenderer.Clear();
            beetrans.position = beepos;
            trailRenderer.Clear();

        }
        if (x >= maxX)
        {
            v.x = -saveMaxX;
            beepos += 2 * v;
            trailRenderer.Clear();
            beetrans.position = beepos;
            trailRenderer.Clear();

        }
        if (x <= -maxX)
        {
            v.x = saveMaxX;
            beepos += 2 * v;
            trailRenderer.Clear();
            beetrans.position = beepos;
            trailRenderer.Clear();

        }

    }
    


     
}
