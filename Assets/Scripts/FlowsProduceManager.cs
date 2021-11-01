using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowsProduceManager : MonoBehaviour
{
    private GameObject[] birds;
    public GameObject birdBass;
    //private int birdsNum;
    GlobalCache global;
    Camera mainCamera;
    public BirdSetting setting;
    //public GameObject playerBird;

    private void Start()
    {
        float width = UnityEngine.Screen.width;
        float height = UnityEngine.Screen.height;
        birds = new GameObject[setting.birdsNum + 1];
        global = GlobalCache.instance;
        mainCamera = global.cameraAttribute;



        for (int i = 0; i < setting.birdsNum; i++)
        {
            float randX = Random.Range(0, width);
            float randY = Random.Range(0, height);
            Vector3 v = new Vector3(randX, randY);
            Vector3 pos = mainCamera.ScreenToWorldPoint(v);
            birds[i] = Instantiate(birdBass);
            birds[i].transform.position = pos;
            float angle = Random.Range(0, 12) * 30f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            birds[i].transform.rotation = q;
            //birds[i].GetComponent<Bird>().theOne = false;
            //birds[i].GetComponent<Bird>().birdID = i + 1;


        }






    }
    /*
    private void Update()
    {
        
    }
    */
}
