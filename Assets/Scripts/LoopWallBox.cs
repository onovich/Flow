using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopWallBox : MonoBehaviour
{
    private float maxX;
    private float maxY;

    private float saveMaxX;
    private float saveMaxY;
    
    private GlobalCache global;
    private GameObject player;
    private Transform playerTrans;
    private Player playerAttribute;
    private Vector2 playerpos;
    public bool ifInTeachingLevel = false;
    private void Start()
    {
        Debug.Log("我在"+gameObject.name);
        global = GlobalCache.instance;
        player = global.player;
        playerTrans = player.transform;
        playerAttribute = global.playerAttribute;
        if (ifInTeachingLevel)
        {
            maxX = 100f;
            maxY = 100f;
            saveMaxX = 90f;
            saveMaxY = 90f;
        }
        else
        {
            maxX = 9.2f;
            maxY = 5f;
            saveMaxX = 8.7f;
            saveMaxY = 4.8f;
        }

    }

    private void FixedUpdate()
    {
        playerpos = playerTrans.position;
        comeInOppoSide();
    }

    void comeInOppoSide()
    {
        
        Vector2 v = Vector2.zero;
        float x = playerpos.x;
        float y = playerpos.y;
        if (y >= maxY)
        {
            Debug.Log("我在"+gameObject.name);
            //Debug.Log("准备转换：y="+y+",-maxY="+ -maxY);
            v.y = -saveMaxY;
            playerpos += 2 * v;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();
            //Debug.Log("越界转换完成");
            playerTrans.position = playerpos;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();


        }
        if (y <= -maxY)
        {
            Debug.Log("我在" + gameObject.name);

            //Debug.Log("准备转换：y=" + y + ",maxY=" + maxY);
            v.y = saveMaxY;
            playerpos += 2 * v;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();
            //Debug.Log("越界转换完成");
            playerTrans.position = playerpos;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();

        }
        if (x >= maxX)
        {
            Debug.Log("我在" + gameObject.name);

            //Debug.Log("准备转换：x=" + x + ",-maxX=" + -maxY);
            v.x = -saveMaxX;
            playerpos += 2 * v;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();
            //Debug.Log("越界转换完成");
            playerTrans.position = playerpos;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();

        }
        if (x <= -maxX)
        {
            Debug.Log("我在" + gameObject.name);

            //Debug.Log("准备转换：x=" + x + ",maxX=" + maxY);
            v.x = saveMaxX;
            playerpos += 2 * v;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();
            //Debug.Log("越界转换完成");
            playerTrans.position = playerpos;
            playerAttribute.Trail.GetComponent<TrailRenderer>().Clear();

        }


    }
}
