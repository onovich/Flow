using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour
{
    private Transform target;
    public Vector3 Move;
    private float maxX;
    private float maxY;
    //private float saveMaxX;
    //private float saveMaxY;
    GlobalCache global;
    public bool ifInTeachingLevel = false;

    [HideInInspector]
    public bool Follow = false;


    private void Start()
    {
        Follow = true;
        global = GlobalCache.instance;
        target = global.player.GetComponent<Transform>();
        //target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Move = transform.position - target.position;
        if (ifInTeachingLevel)
        {
            maxX = 100f;
            maxY = 100f;
            //saveMaxX = 90f;
            //saveMaxY = 90f;
        }
        else
        {
            maxX = 9.2f;
            maxY = 5f;
            //saveMaxX = 8.7f;
            //saveMaxY = 4.8f;
        }
    }


    void comeInOppoSide()
    {
        Vector2 v = Vector2.zero;
        Vector2 playerpos = target.position;
        float x = playerpos.x;
        float y = playerpos.y;
        
        if ((y >= maxY)||(y <= -maxY) ||(x >= maxX) ||(x <= -maxX))
        {
            //Debug.Log("x="+x+",y="+y);
            v.x = 0;
            v.y = 0;
            playerpos = v;
            target.position = playerpos;
            clearTrail();
            //Debug.Log("越界重置");

        }
        
        
    }

    void clearTrail()
    {
        if (target != null)
        {
            TrailRenderer trail = target.gameObject.GetComponent<Player>().Trail.GetComponent<TrailRenderer>();
            trail.Clear();
            //Debug.Log("清除尾迹");

        }
    }

    void Update()
    {
        comeInOppoSide();
        if ((target != null)&&(Follow))
        {
            transform.position = target.position + Move;
            //this.GetComponent<Transform>().rotation = Quaternion.Euler((target.rotation.eulerAngles ));
        }
    }
}
