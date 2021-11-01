using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

 


public class CameraMove : MonoBehaviour
{
    /*
    public delegate void HandleMoves();
    public event EventHandler HandleMovesEvent;
    */
    //事件系统备用，用于发出屏幕移动通知，以便于中断玩家操作权

    public GameObject MainCamera;
    float CameraSize;
    //public bool ifTest=false;
    private GlobalCache global;
    private GameObject CameraHolder;
    private PlayerMovement PM;
    //private bool isMovingCamera = false;

    private Vector3 originalPosition;

    private void Start()
    {
        init();
    }

     


    public void init()
    {
        global = GlobalCache.instance;

        //MainCamera = GameObject.FindGameObjectWithTag("Camera");
        MainCamera = global.Camera;
        //MainCamera.GetComponent<Camera>().orthographicSize = 15;//演出测试用
        MainCamera.GetComponent<Camera>().orthographicSize = 5;
        //if (ifTest) { MainCamera.GetComponent<Camera>().orthographicSize = 10; }
        CameraHolder = global.CameraHolder;
        /**/
        //CameraSize = MainCamera.GetComponent<Camera>().orthographicSize;
        CameraSize = global.cameraAttribute.orthographicSize;
        //StartCoroutine(ScareDownTo(5,1));//演出测试用
        /**/
        //以上调用待封装

        //还需要禁用playerMovement

        originalPosition = CameraHolder.transform.position;
        PM = global.playerMove;
        //isMovingCamera = false;
    }


    /*
    public void ScareToLook(float type)
    {
        if (type == 1)//缩小到6
        {
            StopCoroutine("ScareDownTo");
            StopCoroutine("ScareUpTo");
            
            StartCoroutine(ScareUpTo(6f, 2, 0,CameraMoveType.playerControl));

        }
        else//放大到5
        {
            StopCoroutine("ScareDownTo");
            StopCoroutine("ScareUpTo");
            StartCoroutine(ScareDownTo(5f, 4, 0, CameraMoveType.playerControl));
        }
    }
    
    */

    public void ScareTo(float target, float speed, float delay)
    {
        StopAllCoroutines();
        if (target > global.cameraAttribute.orthographicSize)
        {
            StartCoroutine(ScareUpTo(target, speed, delay));


        }
        else
        {
            StartCoroutine(ScareDownTo(target, speed, delay));

        }

    }


    public IEnumerator ScareDownTo(float target,float speed,float delay)
    {
        Debug.Log("视野缩小中");

        StopCoroutine("ScareUpTo");
        

        PM.IFInLooking = true;
        //isMovingCamera = true;

        /*
        float wait = 0;
        while(wait < delay)
        {
            //wait += Time.deltaTime * 1f;
            wait += global.unscaledDeltaTime * 1f;
            yield return null;
        }*/
        if (delay > 0)
        {
            yield return new WaitForSecondsRealtime(delay);

        }

        while (CameraSize > target)
        {
            //CameraSize -= Time.deltaTime * 0.5f;
            CameraSize -= Time.fixedUnscaledDeltaTime * 2f * speed;
            //MainCamera.GetComponent<Camera>().orthographicSize = CameraSize;
            global.cameraAttribute.orthographicSize = CameraSize;

            yield return new WaitForSecondsRealtime(0.1f);
        }
        global.cameraAttribute.orthographicSize = target;
        PM.IFInLooking = false;
        //isMovingCamera = false;
    }

    public IEnumerator ScareUpTo(float target, float speed,float delay )
    {
        Debug.Log("视野放大中");
        StopCoroutine("ScareDownTo");
        PM.IFInLooking = true;
        //isMovingCamera = true;
        /*
        float wait = 0;
        while (wait < delay)
        {
            //wait += Time.deltaTime * 1f;
            wait += global.deltaTime * 1f;
            yield return null;
        }
        */
        if (delay > 0)
        {
            yield return new WaitForSecondsRealtime(delay);

        }

        while (CameraSize < target)
        {
            //CameraSize += Time.deltaTime * 0.5f;
            CameraSize += Time.fixedUnscaledDeltaTime * 2f * speed;
            //MainCamera.GetComponent<Camera>().orthographicSize = CameraSize;
            global.cameraAttribute.orthographicSize = CameraSize;

            yield return null;
        }
        global.cameraAttribute.orthographicSize = target;
        PM.IFInLooking = false;
        //isMovingCamera = false;
    }


}
