using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamera : MonoBehaviour
{

    public float CameraPush = 5;
    GlobalCache global;
    private GameObject app;
    private CameraMove CM;
    public float speed = 1;
    private float scare = 5;
    //public GameObject oppo;//互斥对象
     

    private void Start()
    {
        global = GlobalCache.instance;
        app = global.APP;
        CM = app.GetComponent<CameraMove>();
        scare = global.cameraAttribute.orthographicSize; 


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //StopCoroutine("ScareDownTo");
        //CM.StopCoroutine("ScareDownTo");
        //CM.StopCoroutine("ScareUpTo");
        //StopAllCoroutines();
        //StopCoroutine(CM.ScareDownTo(CameraPush, speed, 0));
        //StopCoroutine(CM.ScareUpTo(CameraPush, speed, 0));

        if (other.CompareTag("Player"))
        {
            CM.ScareTo(CameraPush, speed, 0);
        }
        

    }





}
